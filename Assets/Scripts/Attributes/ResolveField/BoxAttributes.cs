using System;
using System.Diagnostics;

namespace Attributes.ResolveField
{
    /// <summary>
    /// Draw the properties with a darker background and
    /// borders, optionally.
    /// </summary>
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class DarkBoxAttribute : Attribute
    {
        /// <summary>
        /// Dark
        /// </summary>
        public readonly bool withBorders;

        public DarkBoxAttribute()
        { }

        public DarkBoxAttribute(bool withBorders) => this.withBorders = withBorders;
    }
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ColorBox : Attribute
    {
    }
}
