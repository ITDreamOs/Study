using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Features.Scanning;
using Castle.DynamicProxy;
using Castle.DynamicProxy.Internal;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{
    /// <summary>
    /// Adds registration syntax to the <see cref="ContainerBuilder"/> type.
    /// </summary>
    [SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
    public static class RegistrationExtensions
    {    /// <summary>
         /// Configure the component so that any properties whose types are registered in the
         /// container will be wired to instances of the appropriate service.
         /// </summary>
         /// <param name="registration">Registration to auto-wire properties.</param>
         /// <param name="wiringFlags">Set wiring options such as circular dependency wiring support.</param>
         /// <returns>A registration builder allowing further configuration of the component.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle>
        MyPropertiesAutowired<TLimit, TActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registration, PropertyWiringOptions wiringFlags = PropertyWiringOptions.None, IPropertySelector propertySelector = null)
        {
            var preserveSetValues = (int)(wiringFlags & PropertyWiringOptions.PreserveSetValues) != 0;
            var allowCircularDependencies = (int)(wiringFlags & PropertyWiringOptions.AllowCircularDependencies) != 0;
            if (propertySelector == null)
                propertySelector = new DefaultPropertySelector(preserveSetValues);
            if (allowCircularDependencies)
                registration.RegistrationData.ActivatedHandlers.Add((s, e) => AutowiringPropertyInjector.InjectProperties(e.Context, e.Instance, propertySelector, e.Parameters));
            else
                registration.RegistrationData.ActivatingHandlers.Add((s, e) => AutowiringPropertyInjector.InjectProperties(e.Context, e.Instance, propertySelector, e.Parameters));

            return registration;
            //return registration.PropertiesAutowired(new DefaultPropertySelector(preserveSetValues), allowCircularDependencies);
        }
        ///// <summary>
        ///// Set the policy used to find candidate properties on the implementation type.
        ///// </summary>
        ///// <typeparam name="TLimit">Registration limit type.</typeparam>
        ///// <typeparam name="TActivatorData">Activator data type.</typeparam>
        ///// <typeparam name="TStyle">Registration style.</typeparam>
        ///// <param name="registration">Registration to set policy on.</param>
        ///// <param name="propertySelector">Policy to be used when searching for properties to inject.</param>
        ///// <returns>A registration builder allowing further configuration of the component.</returns>
        //public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> MyPropertiesAutowired<TLimit, TActivatorData, TStyle>(
        //    this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration,
        //    Func<PropertyInfo, object, bool> propertySelector)
        //{
        //    if (registration == null) throw new ArgumentNullException(nameof(registration));

        //    return registration.MyPropertiesAutowired(PropertyWiringOptions.None,new DelegatePropertySelector(propertySelector));
        //}
    }
    internal class AutowiringPropertyInjector
    {
        public const string InstanceTypeNamedParameter = "Autofac.AutowiringPropertyInjector.InstanceType";

        public static void InjectProperties(IComponentContext context, object instance, IPropertySelector propertySelector, IEnumerable<Parameter> parameters)
        {
            //判断instance是否是代理类型，如果是代理类型，则取Target
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (propertySelector == null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var unproxyedInstance = UnwrapProxy(instance);
            var instanceType = instance.GetType();
            var unproxyedInstanceType = unproxyedInstance.GetType();
            foreach (var property in unproxyedInstanceType
                .GetRuntimeProperties()
                .Where(pi => pi.CanWrite))
            {
                var propertyType = property.PropertyType;

                if (propertyType.GetTypeInfo().IsValueType && !propertyType.GetTypeInfo().IsEnum)
                {
                    continue;
                }

                if (propertyType.IsArray && propertyType.GetElementType().GetTypeInfo().IsValueType)
                {
                    continue;
                }

                if (propertyType.IsGenericEnumerableInterfaceType() && propertyType.GetTypeInfo().GenericTypeArguments[0].GetTypeInfo().IsValueType)
                {
                    continue;
                }

                if (property.GetIndexParameters().Length != 0)
                {
                    continue;
                }

                if (!propertySelector.InjectProperty(property, instance))
                {
                    continue;
                }

                var setParameter = property.SetMethod.GetParameters().First();
                var valueProvider = (Func<object>)null;
                var parameter = parameters.FirstOrDefault(p => p.CanSupplyValue(setParameter, context, out valueProvider));
                if (parameter != null)
                {
                    property.SetValue(unproxyedInstance, valueProvider(), null);
                    continue;
                }

                object propertyValue;
                var propertyService = new TypedService(propertyType);
                var instanceTypeParameter = new NamedParameter(InstanceTypeNamedParameter, unproxyedInstanceType);
                if (context.TryResolveService(propertyService, new Parameter[] { instanceTypeParameter }, out propertyValue))
                {
                    property.SetValue(unproxyedInstance, propertyValue, null);
                }
            }
        }
        internal static TType UnwrapProxy<TType>(TType proxy)
        {
            if (!ProxyUtil.IsProxy(proxy))
                return proxy;

            try
            {
                dynamic dynamicProxy = proxy;
                return dynamicProxy.__target;
            }
            catch (RuntimeBinderException)
            {
                return proxy;
            }
        }

    }
    internal static class TypeExtensions
    {
        private static readonly ConcurrentDictionary<Type, bool> IsGenericEnumerableInterfaceCache = new ConcurrentDictionary<Type, bool>();
        private static readonly ConcurrentDictionary<Type, bool> IsGenericListOrCollectionInterfaceTypeCache = new ConcurrentDictionary<Type, bool>();
        private static readonly ConcurrentDictionary<Tuple<Type, Type>, bool> IsGenericTypeDefinedByCache = new ConcurrentDictionary<Tuple<Type, Type>, bool>();




        public static bool IsGenericTypeDefinedBy(this Type @this, Type openGeneric)
        {
            return IsGenericTypeDefinedByCache.GetOrAdd(
                Tuple.Create(@this, openGeneric),
                key => !key.Item1.GetTypeInfo().ContainsGenericParameters
                    && key.Item1.GetTypeInfo().IsGenericType
                    && key.Item1.GetGenericTypeDefinition() == key.Item2);
        }


        public static bool IsGenericEnumerableInterfaceType(this Type type)
        {
            return IsGenericEnumerableInterfaceCache.GetOrAdd(
                type, t => type.IsGenericTypeDefinedBy(typeof(IEnumerable<>))
                           || type.IsGenericListOrCollectionInterfaceType());
        }

        public static bool IsGenericListOrCollectionInterfaceType(this Type type)
        {
            return IsGenericListOrCollectionInterfaceTypeCache.GetOrAdd(
                type, t => t.IsGenericTypeDefinedBy(typeof(IList<>))
                           || t.IsGenericTypeDefinedBy(typeof(ICollection<>))
                           || t.IsGenericTypeDefinedBy(typeof(IReadOnlyCollection<>))
                           || t.IsGenericTypeDefinedBy(typeof(IReadOnlyList<>)));
        }

    }
    public static class DynamicProxyRegistrationExtensions
    {
        private const string InterceptorsPropertyName = "Autofac.Extras.DynamicProxy.RegistrationExtensions.InterceptorsPropertyName";

        private static readonly IEnumerable<Autofac.Core.Service> EmptyServices = new Autofac.Core.Service[0];

        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();


        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration)
        {
            return EnableClassInterceptors(registration, ProxyGenerationOptions.Default);
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <typeparam name="TConcreteReflectionActivatorData">Activator data type.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration)
            where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {
            return EnableClassInterceptors(registration, ProxyGenerationOptions.Default);
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfaces">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, ScanningActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options,
            params Type[] additionalInterfaces)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.ActivatorData.ConfigurationActions.Add((t, rb) => rb.EnableClassInterceptors(options, additionalInterfaces));
            return registration;
        }

        /// <summary>
        /// Enable class interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or added with InterceptedBy().
        /// Only virtual methods can be intercepted this way.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TRegistrationStyle">Registration style.</typeparam>
        /// <typeparam name="TConcreteReflectionActivatorData">Activator data type.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfaces">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        private static IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> EnableClassInterceptors<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration,
            ProxyGenerationOptions options,
            params Type[] additionalInterfaces)
            where TConcreteReflectionActivatorData : ConcreteReflectionActivatorData
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.ActivatorData.ImplementationType =
            ProxyGenerator.ProxyBuilder.CreateClassProxyType(
                registration.ActivatorData.ImplementationType, additionalInterfaces ?? new Type[0],
                options);

            registration.OnPreparing(e =>
            {
                var proxyParameters = new List<Parameter>();
                int index = 0;

                if (options.HasMixins)
                {
                    foreach (var mixin in options.MixinData.Mixins)
                    {
                        proxyParameters.Add(new PositionalParameter(index++, mixin));
                    }
                }

                proxyParameters.Add(new PositionalParameter(index++, GetInterceptorServices(e.Component, registration.ActivatorData.ImplementationType)
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray()));

                if (options.Selector != null)
                {
                    proxyParameters.Add(new PositionalParameter(index, options.Selector));
                }

                e.Parameters = proxyParameters.Concat(e.Parameters).ToArray();
            });

            return registration;
        }

        /// <summary>
        /// Enable interface interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or interface, or added with InterceptedBy() calls.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> EnableInterfaceInterceptors<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration)
        {
            return MyEnableInterfaceInterceptors(registration, null);
        }
        /// <summary>
        /// Enable interface interception on the target type. Interceptors will be determined
        /// via Intercept attributes on the class or interface, or added with InterceptedBy() calls.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> MyEnableInterfaceInterceptors<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, ProxyGenerationOptions options)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.RegistrationData.ActivatingHandlers.Add((sender, e) =>
            {
                EnsureInterfaceInterceptionApplies(e.Component);

                var proxiedInterfaces = e.Instance.GetType().GetInterfaces()
                .Where(i => i.IsVisible || i.Assembly.IsInternalToDynamicProxy()).ToArray();

                if (!proxiedInterfaces.Any())
                {
                    return;
                }
                if (ProxyUtil.IsProxy(e.Instance))
                    return;

                var theInterface = proxiedInterfaces.First();
                var interfaces = proxiedInterfaces.Skip(1).ToArray();

                var interceptors = GetInterceptorServices(e.Component, e.Instance.GetType())
                .Select(s => e.Context.ResolveService(s))
                .Cast<IInterceptor>()
                .ToArray();

                e.Instance = options == null
                ? ProxyGenerator.CreateInterfaceProxyWithTarget(theInterface, interfaces, e.Instance, interceptors)
                : ProxyGenerator.CreateInterfaceProxyWithTarget(theInterface, interfaces, e.Instance, options, interceptors);
            });

            return registration;
        }
        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServices">The interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params Autofac.Core.Service[] interceptorServices)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (interceptorServices == null || interceptorServices.Any(s => s == null))
            {
                throw new ArgumentNullException(nameof(interceptorServices));
            }

            object existing;
            if (builder.RegistrationData.Metadata.TryGetValue(InterceptorsPropertyName, out existing))
            {
                builder.RegistrationData.Metadata[InterceptorsPropertyName] =
               ((IEnumerable<Autofac.Core.Service>)existing).Concat(interceptorServices).Distinct();
            }
            else
            {
                builder.RegistrationData.Metadata.Add(InterceptorsPropertyName, interceptorServices);
            }

            return builder;
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceNames">The names of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params string[] interceptorServiceNames)
        {
            if (interceptorServiceNames == null || interceptorServiceNames.Any(n => n == null))
            {
                throw new ArgumentNullException(nameof(interceptorServiceNames));
            }

            return InterceptedBy(builder, interceptorServiceNames.Select(n => new KeyedService(n, typeof(IInterceptor))).ToArray());
        }

        /// <summary>
        /// Allows a list of interceptor services to be assigned to the registration.
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TStyle">Registration style.</typeparam>
        /// <param name="builder">Registration to apply interception to.</param>
        /// <param name="interceptorServiceTypes">The types of the interceptor services.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        /// <exception cref="System.ArgumentNullException">builder or interceptorServices</exception>
        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InterceptedBy<TLimit, TActivatorData, TStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TStyle> builder,
            params Type[] interceptorServiceTypes)
        {
            if (interceptorServiceTypes == null || interceptorServiceTypes.Any(t => t == null))
            {
                throw new ArgumentNullException(nameof(interceptorServiceTypes));
            }

            return InterceptedBy(builder, interceptorServiceTypes.Select(t => new TypedService(t)).ToArray());
        }

        /// <summary>
        /// Intercepts the interface of a transparent proxy (such as WCF channel factory based clients).
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="additionalInterfacesToProxy">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> InterceptTransparentProxy<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, params Type[] additionalInterfacesToProxy)
        {
            return InterceptTransparentProxy(registration, null, additionalInterfacesToProxy);
        }

        /// <summary>
        /// Intercepts the interface of a transparent proxy (such as WCF channel factory based clients).
        /// </summary>
        /// <typeparam name="TLimit">Registration limit type.</typeparam>
        /// <typeparam name="TActivatorData">Activator data type.</typeparam>
        /// <typeparam name="TSingleRegistrationStyle">Registration style.</typeparam>
        /// <param name="registration">Registration to apply interception to.</param>
        /// <param name="options">Proxy generation options to apply.</param>
        /// <param name="additionalInterfacesToProxy">Additional interface types. Calls to their members will be proxied as well.</param>
        /// <returns>Registration builder allowing the registration to be configured.</returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> InterceptTransparentProxy<TLimit, TActivatorData, TSingleRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActivatorData, TSingleRegistrationStyle> registration, ProxyGenerationOptions options, params Type[] additionalInterfacesToProxy)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            registration.RegistrationData.ActivatingHandlers.Add((sender, e) =>
            {
                EnsureInterfaceInterceptionApplies(e.Component);

                if (!RemotingServices.IsTransparentProxy(e.Instance))
                {
                    throw new DependencyResolutionException(string.Format(
                    CultureInfo.CurrentCulture, "The instance of type '{0}' is not a transparent proxy.", e.Instance.GetType().FullName));
                }

                if (!e.Instance.GetType().IsInterface)
                {
                    throw new DependencyResolutionException(string.Format(
                    CultureInfo.CurrentCulture, "The transparent proxy of type '{0}' must be an interface.", e.Instance.GetType().FullName));
                }

                if (additionalInterfacesToProxy.Any())
                {
                    var remotingTypeInfo = (IRemotingTypeInfo)RemotingServices.GetRealProxy(e.Instance);

                    var invalidInterfaces = additionalInterfacesToProxy
                    .Where(i => !remotingTypeInfo.CanCastTo(i, e.Instance))
                    .ToArray();

                    if (invalidInterfaces.Any())
                    {
                        string message = string.Format(CultureInfo.CurrentCulture, "The transparent proxy does not support the additional interface(s): {0}.",
                                                                                                                            string.Join(", ", invalidInterfaces.Select(i => i.FullName)));
                        throw new DependencyResolutionException(message);
                    }
                }

                var interceptors = GetInterceptorServices(e.Component, e.Instance.GetType())
                    .Select(s => e.Context.ResolveService(s))
                    .Cast<IInterceptor>()
                    .ToArray();

                e.Instance = options == null
                ? ProxyGenerator.CreateInterfaceProxyWithTargetInterface(e.Instance.GetType(), additionalInterfacesToProxy, e.Instance, interceptors)
                : ProxyGenerator.CreateInterfaceProxyWithTargetInterface(e.Instance.GetType(), additionalInterfacesToProxy, e.Instance, options, interceptors);
            });

            return registration;
        }

        private static void EnsureInterfaceInterceptionApplies(IComponentRegistration componentRegistration)
        {
            if (componentRegistration.Services
            .OfType<IServiceWithType>()
            .Any(swt => !swt.ServiceType.IsInterface || (!swt.ServiceType.Assembly.IsInternalToDynamicProxy() && !swt.ServiceType.IsVisible)))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture,
               "The component {0} cannot use interface interception as it provides services that are not publicly visible interfaces. Check your registration of the component to ensure you're not enabling interception and registering it as an internal/private interface type.",
               componentRegistration));
            }
        }

        private static IEnumerable<Autofac.Core.Service> GetInterceptorServices(IComponentRegistration registration, Type implType)
        {
            if (registration == null)
            {
                throw new ArgumentNullException(nameof(registration));
            }

            if (implType == null)
            {
                throw new ArgumentNullException(nameof(implType));
            }

            var result = EmptyServices;

            object services;
            if (registration.Metadata.TryGetValue(InterceptorsPropertyName, out services))
            {
                result = result.Concat((IEnumerable<Autofac.Core.Service>)services);
            }

            if (implType.IsClass)
            {
                result = result.Concat(implType
                    .GetCustomAttributes(typeof(InterceptAttribute), true)
                    .Cast<InterceptAttribute>()
                    .Select(att => att.InterceptorService));

                result = result.Concat(implType
                    .GetInterfaces()
                    .SelectMany(i => i.GetCustomAttributes(typeof(InterceptAttribute), true))
                    .Cast<InterceptAttribute>()
                    .Select(att => att.InterceptorService));
            }

            return result.ToArray();
        }
    }
    /// <summary>
    /// Indicates that a type should be intercepted.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
    public sealed class InterceptAttribute : Attribute
    {
        /// <summary>
        /// Gets the interceptor service.
        /// </summary>
        public Autofac.Core.Service InterceptorService { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptAttribute"/> class.
        /// </summary>
        /// <param name="interceptorService">The interceptor service.</param>
        /// <exception cref="System.ArgumentNullException">interceptorService</exception>
        public InterceptAttribute(Autofac.Core.Service interceptorService)
        {
            if (interceptorService == null)
                throw new ArgumentNullException("interceptorService");

            InterceptorService = interceptorService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptAttribute"/> class.
        /// </summary>
        /// <param name="interceptorServiceName">Name of the interceptor service.</param>
        public InterceptAttribute(string interceptorServiceName)
            : this(new KeyedService(interceptorServiceName, typeof(IInterceptor)))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptAttribute"/> class.
        /// </summary>
        /// <param name="interceptorServiceType">The typed interceptor service.</param>
        public InterceptAttribute(Type interceptorServiceType)
            : this(new TypedService(interceptorServiceType))
        {
        }
    }
}
