using System.Collections.Generic;
using System.Linq;

namespace Weekly.Utils
{
    public static class MarkDownUtils
    {
        public static readonly string TableOfContents = "[[_TOC_]]";

        //make builder!



        //public static string Table(object[][] enumerable)
        //{
        //    if (enumerable.Length == 0) return string.Empty;
        //    int maxColumns = enumerable.Select((x) => x.Length).Max();

        //}

        //public static string Table(IEnumerable<object[]> enumerable, "")
        //{
        //    return Table(enumerable.ToArray());
        //}

        public static string Table(IEnumerable<IEnumerable<object>> vs)
        {
            return Table(vs.Select((x) => x.ToArray()));
        }
    }
}
