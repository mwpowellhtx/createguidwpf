using System;
using System.Diagnostics;
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
        internal static DisplayOrderAttribute GetDisplayOrder(this IFormat format)
        {
            DisplayOrderAttribute result;
            var formatType = format.GetType();
            Debug.Assert(format != null);
            try
            {
                //Expecting a class, not the interface, and not abstract, must be concrete implementation.
                result = (!formatType.IsClass || formatType.IsAbstract || formatType.IsInterface)
                    ? null
                    : formatType.GetCustomAttribute<DisplayOrderAttribute>();
            }
            catch (Exception)
            {
                result = null;
            }
            return result ?? new DisplayOrderAttribute(0);
        }
    }
}
