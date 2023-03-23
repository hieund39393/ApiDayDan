//------------------------------------------------
// Author:                      Nhan Phan
//
// Copyright 2021
//------------------------------------------------

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EVN.Core.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Regex tìm shortcode dạng {{abc}} trong chuỗi
        /// </summary>
        // ReSharper disable once InconsistentNaming
        private const string SHORTCODE_REGEX = @"(\{\{)([^}]*)(\}\})";       

        /// <summary>
        /// Update shortcode với data được truyền vào
        /// </summary>
        /// <param name="source">EmailTemplate muốn render HtmlBody</param>
        /// <param name="data">Data muốn render</param>
        /// <returns>String được update shortcode với data</returns>
        public static string ReplaceShortCode(string source, ExpandoObject data)
        {
            if (string.IsNullOrWhiteSpace(source) || data == null)
                return source;

            var template = source;
            var matchShortCodes = Regex.Matches(template, SHORTCODE_REGEX).Select(c => c.Value).Distinct().ToList();
            var dictionaryData = (IDictionary<string, object>)data;
            foreach (var shortCode in matchShortCodes)
            {
                var key = shortCode.Replace("{{", "").Replace("}}", "");
                dictionaryData.TryGetValue(key, out var shortCodeData);
                if (shortCodeData != null)
                    template = template.Replace(shortCode, shortCodeData.ToString());
            }

            return template;
        }

        public static string RandomString(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var res = new StringBuilder();
            var rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public static string RandomStringAdvance(int length)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*";

            var middle = length / 2;
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                if (middle == length)
                {
                    res.Append(number[rnd.Next(number.Length)]);
                }
                else if (middle - 1 == length)
                {
                    res.Append(special[rnd.Next(special.Length)]);
                }
                else
                {
                    if (length % 2 == 0)
                    {
                        res.Append(lower[rnd.Next(lower.Length)]);
                    }
                    else
                    {
                        res.Append(upper[rnd.Next(upper.Length)]);
                    }
                }
            }
            return res.ToString();
        }
        

        // Slug
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }        
    }
}
