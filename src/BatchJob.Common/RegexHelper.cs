using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BatchJob.Common
{
    public class RegexHelper
    {
        public const string REGEX_EMAIL = @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$";

        /// <summary>
        /// 匹配方法
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 匹配方法忽略大小写
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static bool IsMatchIgnoreCase(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 返回所有匹配字符串中的字符串集合
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>匹配字符串集合</returns>
        public static IList<string> MatchString(string input, string pattern, RegexOptions regexOption = RegexOptions.None)
        {
            IList<string> matchStringList = new List<string>();
            MatchCollection matchCollection = Regex.Matches(input, pattern, regexOption);
            for (int i = 0; i < matchCollection.Count; i++)
            {
                matchStringList.Add(matchCollection[i].Value);
            }
            return matchStringList;
        }

        /// <summary>
        /// 返回所第一个有匹配字符串中的字符串
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>匹配字符串</returns>
        public static string MatchFirstString(string input, string pattern)
        {
            IList<string> matchStringList = MatchString(input, pattern);
            if (matchStringList.Count > 0)
            {
                return matchStringList[0];
            }
            return null;
        }

        /// <summary>
        /// 匹配并替换为空
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="input">替换字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns>替换后的字符串</returns>
        public static string MatchAndReplace(string input, string replace, string pattern, RegexOptions regexOption = RegexOptions.None)
        {
            return Regex.Replace(input, pattern, replace, regexOption);
        }
    }
}
