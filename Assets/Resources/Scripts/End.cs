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
        List<GameManager.LevelResult> starsPerLevel = GameManager.StarsPerLevel;
        if (starsPerLevel.Count == 0) return;
        int totalStars = 0;
        for (int i = 0; i < levels.childCount; i++)
        {
            Transform stars = levels.GetChild(i).GetChild(1);
            for (int j = 0; j < starsPerLevel[i].CountStars; j++)
            {
                stars.GetChild(j).gameObject.SetActive(true);
                totalStars++;
            }
        }

        int finalCount = totalStars / (3 * starsPerLevel.Count);
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
