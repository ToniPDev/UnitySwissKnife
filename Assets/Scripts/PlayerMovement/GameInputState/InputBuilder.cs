using System.Collections.Generic;
using UnityEngine;

namespace PlayerMovement.GameInputState
{
    public class InputBuilder : MonoBehaviour
    {
        public List<Component> parts = new List<Component>();

        public void Add(Component part) => parts.Add(part);
    }
}