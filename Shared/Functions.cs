using System.Linq;

namespace AspNetCoreWebApiProjManager.Shared
{
    public static class Functions
    {
        public static string ToTitleCase(this string text) => text[..1].ToUpper() + text[1..].ToLower();
        public static string GetDisplayName(this string columnName) => string.Join(string.Empty, columnName.Split('_').Select(x => x.ToTitleCase()));
    }
}