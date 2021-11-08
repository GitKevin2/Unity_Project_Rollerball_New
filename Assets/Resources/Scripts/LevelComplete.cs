using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelComplete : MonoBehaviour
{
    public TextMeshProUGUI timeText, collectablesText;
    public float goldTime = 60f;
    public GameObject collectables;
    public Transform stars;

    private int numCollectables;
    private int starsEarned = 1;

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

        if (timer.Seconds <= goldTime)
        {
            starsEarned++;
        }
    }

    public void SetCollectables(int counter)
    {
        collectablesText.text = "Collectables: " + counter;
        if (counter - GameManager.totalCollectables == numCollectables)
        {
            starsEarned++;
        }
    }
    
    
    public void Rate()
    {
        for (int i = 0; i < starsEarned; i++)
        {
            stars.GetChild(i).gameObject.SetActive(true);
        }
    }
}
