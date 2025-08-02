using System;
using System.Diagnostics;
using UnityEngine;

namespace Attributes.ResolveField
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class SerializeReferenceDrawer : PropertyAttribute { }
}