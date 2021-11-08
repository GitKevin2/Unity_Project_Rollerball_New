using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    public OptionsMenu optionsMenu;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.PauseMenu ??= this;
        GameManager.Paused = false;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.enterKey.isPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("Pause");
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        GameManager.Paused = true;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        GameManager.Paused = false;
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        GameManager.MainHUD.Counter = GameManager.totalCollectables;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Paused = false;

    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
