using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Paused = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToIntro()
    {
        SceneManager.LoadScene("Intro");
    }

    public void ToAbout()
    {
        SceneManager.LoadScene("About");
    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
