using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public bool isPlaceholder = true;
    public bool isLevel = true;
    public float delay = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            //GameManager.Paused = true;
            if (isPlaceholder)
            {
                Debug.Log("Goal reached.");
                return;
            }
            if (isLevel)
            {
                GameManager.MainHUD.WinCond();

            }
            // Loads the next scene according to the order of the build.
            NextLevel();
        }
    }
    
    public void NextLevel()
    {
        StartCoroutine(ToNextLevel());
    }

    private IEnumerator ToNextLevel()
    {
        //SceneManager.LoadScene("LoadingScreen");
        yield return new WaitForSecondsRealtime(delay);
        Debug.Log("new scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
