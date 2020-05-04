﻿using System;
using System.Text.RegularExpressions;

namespace FM.Portal.Core.Extention.ReadingTime
{
    public static class CalculateWordsCount
    {
        private static readonly Regex _matchAllTags =
            new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static int WordsCount(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            text = text.CleanTags().Trim();
            text = text.Replace("\t", " ");
            text = text.Replace("\n", " ");
            text = text.Replace("\r", " ");

            var words = text.Split(
                new[] { ' ', ',', ';', '.', '!', '"', '(', ')', '?', ':', '\'', '«', '»', '+', '-' },
                StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private static string CleanTags(this string data)
        {
            return data.Replace("\n", "\n ").RemoveHtmlTags();
        }

        private static string RemoveHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                        string.Empty :
                        _matchAllTags.Replace(text, " ").Replace("&nbsp;", " ");
        }
    }
}
