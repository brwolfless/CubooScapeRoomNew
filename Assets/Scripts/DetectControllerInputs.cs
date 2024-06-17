using UnityEngine;
using UnityEngine.InputSystem;

public class DetectControllerInputs : MonoBehaviour
{
    void Start()
    {
        // List all connected devices
        foreach (var device in InputSystem.devices)
        {
            Debug.Log($"Device: {device.name}, Layout: {device.layout}");
        }
    }

    void Update()
    {
        // Iterate over all connected devices
        foreach (var device in InputSystem.devices)
        {
            if (device is Gamepad)
            {
                var gamepad = device as Gamepad;

                if (gamepad.buttonSouth.wasPressedThisFrame)
                {
                    Debug.Log("Button South Pressed");
                }
                if (gamepad.buttonNorth.wasPressedThisFrame)
                {
                    Debug.Log("Button North Pressed");
                }
                if (gamepad.buttonEast.wasPressedThisFrame)
                {
                    Debug.Log("Button East Pressed");
                }
                if (gamepad.buttonWest.wasPressedThisFrame)
                {
                    Debug.Log("Button West Pressed");
                }

                Vector2 leftStick = gamepad.leftStick.ReadValue();
                if (leftStick != Vector2.zero)
                {
                    Debug.Log("Left Stick: " + leftStick);
                }

                Vector2 rightStick = gamepad.rightStick.ReadValue();
                if (rightStick != Vector2.zero)
                {
                    Debug.Log("Right Stick: " + rightStick);
                }

                if (gamepad.leftTrigger.wasPressedThisFrame)
                {
                    Debug.Log("Left Trigger Pressed");
                }
                if (gamepad.rightTrigger.wasPressedThisFrame)
                {
                    Debug.Log("Right Trigger Pressed");
                }
            }
            else if (device is Keyboard)
            {
                var keyboard = device as Keyboard;
                if (keyboard.spaceKey.wasPressedThisFrame)
                {
                    Debug.Log("Space Key Pressed");
                }
                // Add more keyboard keys detection as needed
            }
            else if (device is Mouse)
            {
                var mouse = device as Mouse;
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    Debug.Log("Mouse Left Button Pressed");
                }
                // Add more mouse buttons detection as needed
            }
            else
            {
                //Debug.Log($"Unknown device: {device.name}, Layout: {device.layout}");
                // Handle other device types if needed
            }
        }
    }
}
