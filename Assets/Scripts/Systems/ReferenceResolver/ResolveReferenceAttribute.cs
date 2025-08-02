using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace Systems.ReferenceResolver
{
    /// <summary>
    /// Attribute used for inject the referenced field 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ResolveReferenceAttribute : Attribute
    {
        public static class ReferenceResolver
        {
            /// <summary>
            /// Gets the instance of the reference resolver singleton and use it for resolve the field reference.
            /// </summary>
            /// <param name="target"></param>
            public static async Task ResolveAsync(MonoBehaviour target)
            {
                await ReferenceResolverSystem.GetInstanceAsync();
                ResolveReferences(target);
            }

            /// <summary>
            /// Loops into the fields that contains the ResolveReference Attribute and uses the singleton by reflection 
            /// to find and set the reference that has cached.
            /// </summary>
            /// <param name="target"></param>
            private static void ResolveReferences(MonoBehaviour target)
            {
                var fields = target.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var field in fields)
                {
                    var attribute = field.GetCustomAttribute<ResolveReferenceAttribute>();
                    if (attribute == null) continue;

                    if (!typeof(IReferenciable).IsAssignableFrom(field.FieldType))
                    {
                        Debug.LogError(
                            $"Field {field.Name} is marked with [ResolveReference] but is not a Referenciable!");
                        continue;
                    }

                    var method = typeof(ReferenceResolverSystem)
                        .GetMethod(nameof(ReferenceResolverSystem.GetReference))
                        ?.MakeGenericMethod(field.FieldType);

                    if (method == null)
                    {
                        Debug.LogError($"Could not find method GetReference for {field.FieldType}");
                        continue;
                    }

                    var reference = method.Invoke(ReferenceResolverSystem.Instance, null);

                    if (reference != null) field.SetValue(target, reference);
                    
                    else Debug.LogWarning($"No reference found for field {field.Name} in {target.name}");
                }
            }
            
            //Used by the installer to optimize the search
            public static bool HasResolvableFields(MonoBehaviour target)
            {
                var fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                return fields.Any(field => field.GetCustomAttribute<ResolveReferenceAttribute>() != null);
            }
        }
    }
}