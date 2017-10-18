using System;
using System.Reflection;

namespace Synergy.Contracts.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsInstanceOfType(this Type type, object obj)
        {
            return obj != null && type.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo());
        }
    }
}