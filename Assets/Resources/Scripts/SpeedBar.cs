using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    private readonly Color YELLOW = new Color(255f/255f, 208f/255f, 0f);
    private readonly Color BLUE = new Color(0f, 255f/255f, 242f/255f);

    public float MaxSpeed
    {
        get => slider.maxValue;
        set => slider.maxValue = value;
    }

    public float Speed
    {
        get => slider.value;
        set
        {
            slider.value = value;
            fill.color = value > MaxSpeed + 5f ? BLUE : YELLOW;
        }
    }


}
