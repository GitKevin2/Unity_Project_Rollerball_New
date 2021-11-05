using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public TMPro.TextMeshProUGUI valueText;

    private Slider slider;
    private CameraController mainCamera;
    private float initialValue;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.GetComponent<CameraController>();
        slider.value = mainCamera.lookSensitivity;
        SetValueText(slider.value);
        initialValue = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetValueText(float value)
    {
        valueText.text = value.ToString();
        if (mainCamera != null) mainCamera.lookSensitivity = value;
        
    }


    public void OK()
    {
        initialValue = slider.value;
        gameObject.SetActive(false);
    }

    public void Cancel()
    {
        slider.value = initialValue;
        SetValueText(initialValue);
        gameObject.SetActive(false);
    }
}
