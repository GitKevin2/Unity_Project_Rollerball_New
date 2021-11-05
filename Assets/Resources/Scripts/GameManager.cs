using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameManager
{
    public static int totalCollectables = 0;
    
    public static HUD MainHUD { get; set; }

    public static PauseMenuController PauseMenu { get; set; }

    public static CameraController CurrentCamera { get; set; }

    private static bool _paused;

    /// <summary>
    /// Sets the pause state of the game. if true most of the game will pause.
    /// </summary>
    public static bool Paused
    {
        get => _paused;
        set
        {
            _paused = value;
            Time.timeScale = _paused ? 0f : 1f;
            Cursor.lockState = _paused ? CursorLockMode.None : CursorLockMode.Locked;
            AudioListener.pause = _paused;
        }
    }
}


public static class AudioStore
{
    public static readonly AudioClip EXPLOSION, COLLECTABLE;

    static AudioStore()
    {
        string directory = "Sounds/SFX/";
        EXPLOSION = Resources.Load<AudioClip>(directory + "Explosion");
        COLLECTABLE = Resources.Load<AudioClip>(directory + "Collectable_pickup_1");

    }
}
