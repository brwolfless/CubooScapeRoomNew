using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public float sliderValue = 0f;
    public Slider slider;

    // Update is called once per frame
    void Update()
    {
        slider.value = sliderValue;

        if(slider.value != 0f)
        {
            slider.gameObject.SetActive(true);
        }
        else
        {
            slider.gameObject.SetActive(false);
        }
    }
}
