using System.Linq;
using PlayerMovement.GameInputState.Input_Adapter;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement.GameInputState
{
    public class InputConfigurationInstaller : MonoBehaviour
    {
        public IInput GetInput()
        {
            var devices = InputSystem.devices;
            
            return devices.Any(device => device.name == "XInputControllerWindows")
                ? new XboxInputAdapter()
                : new KeyboardInputAdapter();
        }
    }
}