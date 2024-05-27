using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class InventoryVR : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Anchor;
    bool UIActive;

    private InputDevice rightHandDevice;
    private bool buttonFourPressed;

    private void Start()
    {
        Inventory.SetActive(false);
        UIActive = false;
        InitializeInputDevices();
    }

    private void InitializeInputDevices()
    {
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);

        if (inputDevices.Count > 0)
        {
            rightHandDevice = inputDevices[0];
        }
    }

    private void Update()
    {
        if (!rightHandDevice.isValid)
        {
            InitializeInputDevices();
        }

        if (rightHandDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out buttonFourPressed) && buttonFourPressed)
        {
            UIActive = !UIActive;
            Inventory.SetActive(UIActive);
        }

        if (UIActive)
        {
            Inventory.transform.position = Anchor.transform.position;
            Inventory.transform.eulerAngles = new Vector3(Anchor.transform.eulerAngles.x + 15, Anchor.transform.eulerAngles.y, 0);
        }
    }
}
