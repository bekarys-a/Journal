using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journal.Extensions;


internal static class StringExtensions
{
    internal static string CapitalizeFirstLetter(this string? text, CultureInfo? culture = null)
    {
        if (text == null)
        {
            return string.Empty;
        }

        culture ??= CultureInfo.InvariantCulture;

        if (text.Length > 0 && char.IsLower(text[0]))
        {
            text = string.Format(culture, "{0}{1}", char.ToUpper(text[0], culture), text.Substring(1));
        }

        return text;
    }
}
