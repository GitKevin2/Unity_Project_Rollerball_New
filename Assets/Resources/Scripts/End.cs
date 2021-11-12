using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private Transform levels;
    [SerializeField] private Transform finalStars;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        // Fetch stars earned per level and total them up.
        List<GameManager.LevelResult> starsPerLevel = GameManager.StarsPerLevel;
        if (starsPerLevel.Count == 0)
        {
            return;
        }
        int totalStars = 0;
        for (int i = 0; i < starsPerLevel.Count; i++)
        {
            //Fetch star images which is the second child object of the current level object.
            Transform stars = levels.GetChild(i).GetChild(1);
            //Iterate an enable each star equal to amount of stars earned.
            for (int j = 0; j < starsPerLevel[i].CountStars; j++)
            {
                stars.GetChild(j).gameObject.SetActive(true);
                totalStars++;
            }
        }

        // calculate final number of stars.
        int finalCount = totalStars / starsPerLevel.Count;
        for (int i = 0; i < finalCount; i++)
        {
            finalStars.GetChild(i).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
