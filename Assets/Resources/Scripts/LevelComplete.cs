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
    void Awake()
    {
        numCollectables = collectables.transform.GetComponentsInChildren(typeof(PickUpController), true).Length;

        Debug.Log(numCollectables != 0);
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
        Debug.Log("Test: " + numCollectables);
        collectablesText.text = "Collectables: " + (counter - GameManager.totalCollectables);
        Debug.Log((counter - GameManager.totalCollectables) + " : " + numCollectables);
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
        GameManager.AddLevelResult(starsEarned);
    }
}
