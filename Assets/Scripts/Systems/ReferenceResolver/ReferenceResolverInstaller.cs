using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.ReferenceResolver
{
    /// <summary>
    /// Installer of the reference resolver system that loops over all MonoBehaviours in scene, and if the component has 
    /// the reference resolver attribute, executes the singleton logic for resolve the field.
    /// </summary>
    public class ReferenceResolverInstaller : MonoBehaviour
    {
        private const string VariableReferenceResourcesError = "Elements on scene couldn't be resolved.";
        
        private async void Awake()
        {
            try
            {
                DontDestroyOnLoad(gameObject);
                await ResolveAllReferencesInScene();
            }
            catch (Exception e)
            {
                Debug.LogError(VariableReferenceResourcesError);
            }
        }

        private void OnEnable() => SceneManager.sceneLoaded  += ResolveAllReferencesInScene;

        private void OnDisable() => SceneManager.sceneLoaded -= ResolveAllReferencesInScene;

        /// <summary>
        /// Loops monoBehaviours and execute the attribute logic
        /// </summary>
        private async Task ResolveAllReferencesInScene()
        {
            try
            {
                var allMonoBehaviours = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None); // incluye inactivos

                var tasks = (from mono in allMonoBehaviours where ResolveReferenceAttribute.ReferenceResolver.HasResolvableFields(mono) select ResolveReferenceAttribute.ReferenceResolver.ResolveAsync(mono)).ToList();

                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                Debug.LogError("Couldn't resolve all references");
            }
        }

        private async void ResolveAllReferencesInScene(Scene arg0, LoadSceneMode arg1)
        {
            try
            {
                await ResolveAllReferencesInScene();
            }
            catch (Exception e)
            {
                Debug.LogError("Couldn't resolve all references");
            }
        }
    }
}