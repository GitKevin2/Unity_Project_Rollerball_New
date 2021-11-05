using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public PlayerController player;
    public CameraController mainCamera;
    public PauseMenuController pauseMenu;

    public void OnMove(InputValue input)
    {
        player.SetMoveInput(input.Get<Vector2>());
    }

    public void OnLook(InputValue input)
    {
        mainCamera.Orbit(input.Get<Vector2>());
    }

    public void OnJump() => player.Jump();

    public void OnPause()
    {
        if (GameManager.Paused)
            pauseMenu.ResumeGame();
        else
            pauseMenu.PauseGame();
    }
}
