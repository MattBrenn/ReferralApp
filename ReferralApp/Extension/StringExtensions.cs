using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;

namespace ReferralApp.Extension
{
    public static class StringExtensions
    {
        public static string TrimAndReduce(this string str)
        {
            return RemoveAfterFirstSpace(str).Trim();
        }

        public static string RemoveAfterFirstSpace(this string value)
        {
            return Regex.Match(value, "^[^ ]+").Value;
        }

    }
}