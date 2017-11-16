using lvwei8.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.MvcBackend.Common
{


    public class ApkPackageUtil
    {
        private readonly static LimitedConcurrencyLevelTaskScheduler lcts;
        private readonly static TaskFactory factory;
        static ApkPackageUtil()
        {
            lcts = new LimitedConcurrencyLevelTaskScheduler(1);
            factory = new TaskFactory(lcts);
        }
        public static void createPackage(List<string> spreaderIds)
        {
            foreach (var spreadId in spreaderIds)
            {
                Task<string> t = factory.StartNew(() =>
                {
                    string apkPhsicialPath = ConfigurationManager.AppSettings["ApkPackageFolderName"].TrimEnd(new[] { '\\' });
                    string buildXmlPath = apkPhsicialPath + "/build.xml";

                    System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe");
                    //System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", String.Format(@"ant -f ""{0}"" -DuserId={1} -DuserDir={1}", buildXmlPath, spreadId));
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.CreateNoWindow = true;
                    procStartInfo.UseShellExecute = false;
                    procStartInfo.RedirectStandardOutput = true;
                    procStartInfo.RedirectStandardInput = true;
                    procStartInfo.RedirectStandardError = true;

                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo = procStartInfo;
                    proc.Start();
                    System.IO.StreamWriter sIn = proc.StandardInput;
                    System.IO.StreamReader sOut = proc.StandardOutput;
                    System.IO.StreamReader sError = proc.StandardError;
                    //C:\Users\Administrator>ant -f "D:\eXiuBackend\exiuCarApp-release-1.0-20140910/build.xml" -DuserId=30 -DuserDir=30 -Djava-folder="D:/Program Files (x86)/Java/jdk1.6.0_10" -Dsdk-folder="D:/eXiuBackend/android-sdk-19"
                    var command = String.Format(@"ant -f ""{0}"" -DuserId=""{1}"" -DappChannel={4}{1} -Djava-folder=""{2}"" -Dsdk-folder=""{3}"" -Dapk-name=""{5}"" -Doutdir-bin-user=""../{6}{1}""", buildXmlPath, spreadId,
                        ConfigurationManager.AppSettings["Apk-java-folder"],
                        ConfigurationManager.AppSettings["Apk-sdk-folder"],
                        ConfigurationManager.AppSettings["apk-appChannel"],
                        "exiu-car-" + DateTime.Now.ToString("yyyyMMddHHMM"),
                        ApkSubFolderName.carOwnerApp);
                    var baiduMapSettingValue = (String)ConfigurationManager.AppSettings["apk-baiduMap"];
                    if (!String.IsNullOrEmpty(baiduMapSettingValue))
                    {
                        command += " -DbaiduMap=" + baiduMapSettingValue;
                    }
                    var baiduCountSettingValue = (String)ConfigurationManager.AppSettings["apk-baiduCount"];
                    if (!String.IsNullOrEmpty(baiduCountSettingValue))
                    {
                        command += " -DbaiduCount=" + baiduCountSettingValue;
                    }
                    var serverHost = (String)ConfigurationManager.AppSettings["Apk-serverHost"];
                    if (!String.IsNullOrEmpty(serverHost))
                    {
                        command += " -DserverHost=" + serverHost;
                    }
                    var serverHttpsHost = (String)ConfigurationManager.AppSettings["Apk-serverHttpsHost"];
                    if (!String.IsNullOrEmpty(serverHttpsHost))
                    {
                        command += " -DserverHttpsHost=" + serverHttpsHost;
                    }
                    var chatHost = (String)ConfigurationManager.AppSettings["Apk-chatHost"];
                    if (!String.IsNullOrEmpty(chatHost))
                    {
                        command += " -DchatHost=" + chatHost;
                    }
                    var sipHost = (String)ConfigurationManager.AppSettings["Apk-sipHost"];
                    if (!String.IsNullOrEmpty(sipHost))
                    {
                        command += " -DsipHost=" + sipHost;
                    }
                    log4net.LogManager.GetLogger("ApkAntConsole").Info("Write Ant command: " + command);
                    sIn.WriteLine(command);
                    //proc.WaitForExit();                
                    //log4net.LogManager.GetLogger("ApkAntConsole").Info(sOut.ReadToEnd().Trim());

                    sIn.WriteLine("EXIT");
                    proc.Close();
                    log4net.LogManager.GetLogger("ApkAntConsole").Info(sOut.ReadToEnd().Trim());
                    log4net.LogManager.GetLogger("ApkAntConsole").Info(sError.ReadToEnd().Trim());
                    sIn.Close();
                    sOut.Close();
                    sError.Close();
                    return string.Empty;
                });
            }
        }
    }
}