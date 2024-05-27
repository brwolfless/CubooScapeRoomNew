using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOnOff : MonoBehaviour
{
    // Liga e desliga um objeto
   public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

   
}
