using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timeText;
    [SerializeField] private bool playOnAwake = true;
    private float seconds;
    private int minutes;
    // Start is called before the first frame update
    void Awake()
    {
        seconds = 0;
        minutes = 0;
        gameObject.SetActive(playOnAwake);
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (TotalSeconds % 60 == 0) minutes = TotalSeconds / 60;
    }

    public void Begin()
    {
        gameObject.SetActive(true);
    }

    public void Stop()
    {
        gameObject.SetActive(false);
    }

    public void ResetTime()
    {
        seconds = 0; minutes = 0;
    }

    public string CurrentTime
    {
        get => minutes + "m " + (int)(seconds - (minutes * 60)) + "s";
    }

    public float Seconds => seconds;

    public int TotalSeconds => (int)seconds;

    public int Minutes => minutes;


}
