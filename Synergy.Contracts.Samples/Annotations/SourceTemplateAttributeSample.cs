using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Synergy.Contracts.Samples.Annotations
{
    public static class SourceTemplateAttributeSample
    {
        [SourceTemplate]
        public static void forEach<T>(this IEnumerable<T> xs)
        {
            foreach (T x in xs)
            {
                //$ $END$
            }
        }

        [SourceTemplate]
        public static void newGuid(this object obj, [Macro(Expression = "guid()", Editable = -1)] string newguid)
        {
            Console.WriteLine("$newguid$");
        }

        private static void test()
        {
            var enumerable = new List<string>();

            //enumerable.forEach
            //enumerable.newGuid

            string s = null;
            //s.fi
        }
    }
}