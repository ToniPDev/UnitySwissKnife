using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Services.ShortcutService
{

    [Serializable]
    public struct ShortcutNode : IEquatable<KeyCode[]>
    {
        #region Public Variables

        public KeyCode[] keys;
        public UnityEvent callback;
        
        #endregion

        #region Utility Methods

        public bool Equals(KeyCode[] other)
        {
            if (keys.Length != other!.Length) return false;
            return !keys.Where((t, idx) => t != other[idx]).Any();
        }

        #endregion
    }

    public class ShortcutsService : MonoBehaviour
    {
        //[("THERE IS MORE THAN ONE SHORTCUT ASSIGNED TO THE SAME MENU","DuplicatedKeysAssigned")]
        [SerializeField]
        public ShortcutNode[] shortcutHandlers;

        private void Update()
        {
    // #if UNITY_EDITOR
            foreach (var shortcut in shortcutHandlers)
            {
                var active = true;
                var lastKey = shortcut.keys[^1];

                foreach (var key in shortcut.keys)
                {
                    if (key == lastKey && !Input.GetKeyDown(key))
                    {
                        active = false;

                        break;
                    }

                    if (key == lastKey || Input.GetKey(key)) continue;
                    
                    active = false;

                    break;
                }

                if (active) shortcut.callback.Invoke();
            }
    // #endif
        }

        private bool DuplicatedKeysAssigned() =>
            shortcutHandlers
                .Select(currentHandler => shortcutHandlers
                    .Count(node => node.Equals(currentHandler.keys)))
                .Any(count => count > 1);
    }

}

