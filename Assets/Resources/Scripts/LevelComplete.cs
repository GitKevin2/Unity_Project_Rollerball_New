using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    public TextMeshProUGUI timeText, collectablesText;
    public float goldTime = 60f;
    public GameObject collectables;

    private int numCollectables;

    // Start is called before the first frame update
    void Start()
    {
        numCollectables = collectables.GetComponentsInChildren(typeof(PickUpController)).Length;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime(Timer timer)
    {
        timeText.text = "Time: " + timer.CurrentTime;
    }

    public void SetCollectables(int counter)
    {
        collectablesText.text = "Collectables: " + counter;
    }
    
    
    public void Rate()
    {

    }
}
