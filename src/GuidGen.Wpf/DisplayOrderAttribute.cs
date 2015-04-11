using System;
using System.Reflection;

namespace GuidGen
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class DisplayOrderAttribute : Attribute
    {
        public int Order { get; private set; }

        public DisplayOrderAttribute(int order)
        {
            Order = order;
        }
    }

    internal static class DisplayOrderExtensionMethods
    {
        internal static DisplayOrderAttribute GetDisplayOrder(this Type type)
        {
            DisplayOrderAttribute result;
            try
            {
                result = (type == null || !type.IsClass)
                    ? null
                    : type.GetCustomAttribute<DisplayOrderAttribute>();
            }
            catch (Exception)
            {
                result = null;
            }
            return result ?? new DisplayOrderAttribute(0);
        }
    }
}
