﻿using lvwei8.Service.Base.DTO;
using lvwei8.Service.Base.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Service.Base
{
    /// <summary>
    /// 只读数据仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IReadOnlyRepository<TEntity> where TEntity : class, new()
    {
        ///// <summary>
        ///// 查找单个实体
        ///// </summary>
        ///// <param name="id">id</param>
        ///// <returns>查询到的结果</returns>
        //TEntity Get(int id);
        ///// <summary>
        ///// 查找单个实体
        ///// </summary>
        ///// <param name="id">id</param>
        ///// <returns>查询到的结果</returns>
        //TEntity Get(string id);
        /// <summary>
        /// 根据条件查询单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 考虑主从延迟的查找单个实体, 先从slave，如果为空从master取
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        TEntity GetLag(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 根据条件查询单个实体(更新用)
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        TEntity GetForUpdate(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询记录条数
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的条数</returns>
        int Count(Expression<Func<TEntity, bool>> predicate, bool tryMainStoreIfZero = false);

        /// <summary>
        /// 查询所有实体
        /// </summary>
        /// <returns>所有实体</returns>
        IQueryable<TEntity> Query();

        /// <summary>
        /// 查询所有实体并排序
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Dictionary<Func<TEntity, TValue>, SortType> orderBy);

        /// <summary>
        /// 分页查询所有的实体
        /// </summary>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Dictionary<Func<TEntity, TValue>, SortType> orderBy, PageModel page);

        /// <summary>
        /// 根据条件查询所有符合条件的实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件查询所有符合条件的实体(更新用)
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>查询到的结果</returns>
        IQueryable<TEntity> QueryForUpdate(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件和排序查询实体
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Dictionary<Func<TEntity, TValue>, SortType> orderBy);

        /// <summary>
        /// 分页查询所有符合条件的实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Dictionary<Func<TEntity, TValue>, SortType> orderBy, PageModel page);

        IQueryable<TEntity> Query<TValue>(Expression<Func<TEntity, bool>> predicate, Func<TEntity, TValue> orderBy, SortType sortType, PageModel page);

        ///// <summary>
        ///// SQL语句查询
        ///// </summary>
        ///// <param name="query">查询语句</param>
        ///// <param name="parameters">查询参数</param>
        ///// <returns>查询到的结果</returns>
        //IQueryable<TEntity> SelectQuery(string query, params object[] parameters);
    }
}
