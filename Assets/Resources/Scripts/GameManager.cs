using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameManager
{
    public static bool firstAccess;
    public static int totalCollectables = 0;
    public static CameraController _currentCamera;
    
    public static HUD MainHUD { get; set; }

    public static PauseMenuController PauseMenu { get; set; }

    public static CameraController CurrentCamera
    {
        get => _currentCamera;
        set
        {
            _currentCamera = value;
            if (!firstAccess) value.lookSensitivity = Options.LookSensitivty;
            else firstAccess = false;
        }
    }

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

    public static void Save<T>()
    {
        Type type = typeof(T);
        if (type == typeof(CameraController))
        {
            Options.LookSensitivty = CurrentCamera.lookSensitivity;
        }
        else if (type == typeof(HUD))
        {
            totalCollectables = MainHUD.Counter;
        }
        else if (type == typeof(PauseMenuController))
        {

        }
        else throw new InvalidOperationException();
    }

    public static class Options
    {
        public static float LookSensitivty { get; set; }
    }
}


public static class AudioStore
{
    public static readonly AudioClip EXPLOSION, COLLECTABLE, INTRO_MAIN, INTRO_END;

    static AudioStore()
    {
        string directory = "Sounds/SFX/";
        EXPLOSION = Resources.Load<AudioClip>(directory + "Explosion");
        COLLECTABLE = Resources.Load<AudioClip>(directory + "Collectable_pickup_1");
        INTRO_MAIN = Resources.Load<AudioClip>(directory + "Intro_Dialogue_reverb_v2_trim-end");
        INTRO_END = Resources.Load<AudioClip>(directory + "Intro_Trim-end-sample_reverb");

    }
}
