using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    public TextMeshProUGUI timeText, collectablesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(int seconds)
    {
        int minutes = seconds / 60;
        int remainingSeconds = seconds - (minutes * 60);
        timeText.text = $"Time: {minutes}m {remainingSeconds}s";
    }

    public void SetTime(Timer timer)
    {
        timeText.text = "Time: " + timer.CurrentTime;
    }

    public void SetCollectables(int counter)
    {
        collectablesText.text = "Collectables: " + counter;
    } 
}
