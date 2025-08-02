#region

using System;

#endregion

namespace Attributes.ResolveField
{
    /// <summary>
    /// Finds the reference of the field in different ways, depending on the value of the Getter, it uses the type of the
    /// field to search but can ve override by setting a cast type.
    /// </summary>
    /// <param name="Value">The way the field will be searched.</param>
    /// <param name="CastType">The type of Component to retrieve.</param>
    public class FindComponentAttribute : Attribute
    {
        public readonly Getter Value;
        public string MatchingName { get; set; } = "";
        public Type CastType { get; set; } = null;
        public FindComponentAttribute(Getter value = Getter.GetComponent) => Value = value;
    }

    public enum Getter
    {
        GetComponent,
        GetComponentInChildren,
        GetComponentInParent,
        FindObjectMatchingName,
        FindObjectOfType,
        FindOnProject
    }
}