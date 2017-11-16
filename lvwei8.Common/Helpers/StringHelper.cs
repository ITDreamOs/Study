using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace lvwei8.Common.Helpers
{
    public static class StringHelper
    {
        #region URL字符串编码
        /// <summary>
        /// URL字符串编码
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string UrlEncode(string str, Encoding encode)
        {
            try
            {
                return HttpUtility.UrlEncode(str.ToString(), encode);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static string UrlEncode(string str)
        {
            return UrlEncode(str, Encoding.UTF8);
        }
        #endregion

        #region 去除链接
        /// <summary>
        /// 去除链接（包含链接内的文字）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        static public string RemoveLinkString(string content)
        {
            string regexstr = @"<a[^>]*>[^><]*</a[^>]*>";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 去除链接
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        static public string RemoveLink(string content)
        {
            string regexstr = @"(<a[^>]*>|</a[^>]*>)";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        #endregion

        #region 截取字符串
        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="sourceInput">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <param name="tail">超出的部分用指定字符串代替</param>
        /// <returns>子字符串</returns>
        public static string CutString(string input, int start, int length, string tail)
        {
            string source = input;
            if (length < 0)
            {
                length = source.Length + length;
                if (length < 0)
                {
                    length = 0;
                }
            }
            if (start < 0)
            {
                start = source.Length + start;
                if (start < 0)
                {
                    start = 0;
                }
            }
            if (start > source.Length)
            {
                return string.Empty;
            }
            if (source.Length - start < length)
            {
                length = source.Length - start;
            }
            if (length >= source.Length - start)
                return source.Substring(start, length);
            else
                return source.Substring(start, length) + tail;
        }

        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string source, int length)
        {
            return CutString(source, 0, length, "");
        }
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="length">截取长度</param>
        /// <returns>子字符串</returns>
        public static string Cut(this string source, int length)
        {
            return CutString(source, 0, length, "");
        }
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="start">开始位置</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string CutString(string source, int start, int length)
        {
            return CutString(source, start, length, "");
        }

        #endregion

        #region 混合截取字符串（中文和英文同时截取）
        /// <summary>
        /// 混合截取字符串（中文和英文同时截取）
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringChinese(string source, int length)
        {
            return SubStringChinese(source, 0, length, string.Empty);
        }
        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <param name="length">指定长度</param>
        /// <param name="tip">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string SubStringChinese(string source, int length, string tip)
        {
            return SubStringChinese(source, 0, length, tip);
        }
        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <param name="start">起始位置</param>
        /// <param name="length">指定长度</param>
        /// <param name="tip">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string SubStringChinese(string source, int start, int length, string tip)
        {
            string myResult = source;

            //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
            if (System.Text.RegularExpressions.Regex.IsMatch(source, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(source, "[\xAC00-\xD7A3]+"))
            {
                //当截取的起始位置超出字段串长度时
                if (start >= source.Length)
                {
                    return "";
                }
                else
                {
                    return source.Substring(start, ((length + start) > source.Length) ? (source.Length - start) : length);
                }
            }
            if (length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(source);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > start)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (start + length))
                    {
                        p_EndIndex = length + start;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        length = bsSrcString.Length - start;
                        tip = "";
                    }
                    int nRealLength = length;
                    int[] anResultFlag = new int[length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = start; i < p_EndIndex; i++)
                    {

                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                            {
                                nFlag = 1;
                            }
                        }
                        else
                        {
                            nFlag = 0;
                        }

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[length - 1] == 1))
                    {
                        nRealLength = length + 1;
                    }

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, start, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);

                    myResult = myResult + tip;
                }
            }
            return myResult;
        }
        #endregion

        #region 字符串处理
        /// <summary>
        /// 移除特殊字符
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        static public string RemoveSpecialString(string content)
        {
            string regexstr = @"([\W]+?)";
            return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 格式化手机号
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static string FormatMobilePhone(string phone)
        {
            string strRtn = phone;
            strRtn = strRtn.Trim();
            strRtn = strRtn.Replace("+86", "");
            strRtn = RemoveSpecialString(strRtn);
            return strRtn;
        }
        public static string TrimAll(string str)
        {
            Regex r = new Regex(@"\s+");
            return r.Replace(str, "");
        }
        static public string GetRandomMumber(int intSize)
        {
            string strRtn = "";

            Random Rnd = new Random();

            char[] ocdChars = "0123456789".ToCharArray();
            for (int i = 0; i < intSize; i++)
            {
                strRtn += ocdChars[Rnd.Next(0, ocdChars.Length)].ToString();
            }

            return strRtn;
        }
        /// <summary>
        /// 将字符串分割为数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<T> StringSplit<T>(string source, string split = ",")
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrWhiteSpace(source))
            {
                var stringArray = source.Split(split.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                stringArray.ForEach(e =>
                {
                    result.Add((T)Convert.ChangeType(e, typeof(T)));
                });
            }
            return result;
        }

        public static string Join(string separator, List<string> list, string defaultValue)
        {
            if (list == null || list.Count == 0) return defaultValue;
            return string.Join(separator, list);
        }
        #endregion

        #region 判断逗号或者分号分隔分隔的
        /// <summary>
        /// 判断逗号或者分号分隔的字符串
        /// 区域码规则;
        /// 河南省,郑州市，金水区=》郑州市金水区
        /// 河南省,郑州市，金水区;河南省,郑州市，二七区=》郑州市，金水区；郑州市，二七区
        /// 全车系,奥迪,奥迪A4=》奥迪A4
        /// </summary>
        /// <param name="inputstring">河南省,郑州市，金水区</param>
        /// <param name="IsAreaName">true</param>
        /// <returns>郑州市，金水区</returns>
        public static string CodeNamePartitionMaster(string inputstring, bool IsAreaName)
        {

            string result = null;
            if (!inputstring.Contains(";"))
            {
                return CodeNamePartition(inputstring, IsAreaName);
            }
            var liststr = inputstring.Split(';');
            var listmatch = new List<string>();
            foreach (var item in liststr)
            {
                listmatch.Add(CodeNamePartition(item, IsAreaName));
            }
            result = string.Join(";", listmatch);
            return result;
        }
        /// <summary>
        /// 单个选项的解析
        /// </summary>
        /// <param name="inputstring"></param>
        /// <param name="IsAreaName"></param>
        /// <returns></returns>
        public static string CodeNamePartition(string inputstring, bool IsAreaName)
        {

            Match match = Regex.Match("", "");
            string matchstr = null;
            int matchgroup = 1;
            //一般都是3级或者两级
            int count = inputstring.Split(',').Length;
            if (count == 1) return inputstring;
            if (count == 2)
            {
                matchstr = @"^([\S\s]*?,([\S\s]*?))$";
            }
            else if (count == 3)
            {
                if (IsAreaName)
                {
                    matchgroup = 2;
                }
                else
                {
                    matchgroup = 3;
                }

                matchstr = @"^([\S\s]*?,([\S\s]*?,([\S\s]*?)))$";
            }
            match = Regex.Match(inputstring.Replace(" ", ""), matchstr);

            return match.Groups[matchgroup].Value;

        }
        #endregion

        /// <summary>
        /// 为手机号加掩码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <param name="mask">掩码</param>
        /// <returns>加掩码后手机号</returns>
        public static string MaskPhone(string phone, char mask = '*')
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;
            if (CheckerHelper.IsMobile(phone))
            {
                var sb = new StringBuilder();
                sb.Append(phone.Substring(0, 3));
                sb.Append(mask);
                sb.Append(mask);
                sb.Append(mask);
                sb.Append(mask);
                sb.Append(phone.Substring(7));
                return sb.ToString();
            }
            return phone;
        }

        /// <summary>
        /// 为车牌号加掩码
        /// </summary>
        /// <param name="carnumber">车牌号</param>
        /// <param name="mask">掩码</param>
        /// <returns>加掩码后车牌号</returns>
        public static string MaskCarNumber(string carnumber, char mask = '*')
        {
            //川AA1234
            if (string.IsNullOrWhiteSpace(carnumber)) return string.Empty;
            if (carnumber.Length!=7)
            {
                return carnumber.Substring(0,1)+ "******";
            }
            var sb = new StringBuilder();
            sb.Append(carnumber.Substring(0, 2));
            sb.Append(mask);
            sb.Append(mask);
            sb.Append(mask);
            sb.Append(mask);
            sb.Append(carnumber.Substring(6));
            return sb.ToString();
        }

        /// <summary>
        /// 构建InSQl
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static string BuildStringInSql(List<string> items)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                sb.AppendFormat("'{0}'", items[i]);
                if (i != items.Count - 1)
                {
                    sb.AppendFormat(",");
                }
            }
            return sb.ToString();
        }

        #region sql注入处理

        #region 关键字检测
        /// <summary>
        /// 关键字检测
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
        public static bool CheckKeyWord(string input, ref string resultstr)
        {
            var result = false;
            //过滤关键字
            string keyword = @"select|insert|delete|from|where|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|or|and|into|outfile|' or '1'='1'";
            //过滤关键字符
            string StrRegex = @"[-|;|,|/|\(|\)|\[|\]|}|{|%|\@|*|!|']";
            if (Regex.IsMatch(input, keyword, RegexOptions.IgnoreCase) || Regex.IsMatch(input, StrRegex))
            {
                result = true;
                resultstr = Regex.Replace(input, keyword, "", RegexOptions.IgnoreCase).Trim();
                resultstr = Regex.Replace(resultstr, StrRegex, "").Trim();
                return result;
            }
            resultstr = input;
            return result;
        }
        #endregion


        #region 关键字简单过滤
        /// <summary>
        /// 关键字简单过滤
        /// </summary>
        /// <param name="input">输入</param>
        /// <param name="resultstr">返回结果</param>
        /// <returns></returns>
        public static bool CheckSimpleKeyWord(string input, ref string resultstr)
        {
            var result = false;
            //过滤关键字
            string keyword = @"select|insert|delete|where|from|count\(|drop table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec master|netlocalgroup administrators|:|net user|""|into|outfile|' or '1'='1'";
            //过滤关键字符
            string StrRegex = @"[-|;|,|/|\(|\)|\[|\]|}|{|\@|*]";
            if (Regex.IsMatch(input, keyword, RegexOptions.IgnoreCase) || Regex.IsMatch(input, StrRegex))
            {
                result = true;
                resultstr = Regex.Replace(input, keyword, "", RegexOptions.IgnoreCase).Trim();
                resultstr = Regex.Replace(resultstr, StrRegex, "").Trim();
                return result;
            }
            resultstr = input;
            return result;
        }

        #endregion

        #region 检查正规IP地址
        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #endregion


        #region 字符串匹配正则
        /// <summary>
        /// 字符串匹配正则
        /// </summary>
        /// <param name="inputStr">输入的字符串</param>
        /// <param name="Regstr">正则表达式字符串</param>
        /// <returns></returns>
        public static Match StrIsMatch(string inputStr, string Regstr)
        {
            return Regex.Match(inputStr, Regstr);
        }
        #endregion
    }
    public static class StringExt
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
        /// <summary>
        /// 将关键字分隔，并组合成用于mysql REGEXP的匹配字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSqlMultiKeywordsRegexp(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return ".*";
            var splidedKeywords = value.Split(new string[] { "'", " ", ",", ";", "　" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => e.Trim()).ToArray();
            var result = splidedKeywords.Count() > 0 ? string.Join("|", splidedKeywords) : ".*";
            return result;
        }
    }
}
