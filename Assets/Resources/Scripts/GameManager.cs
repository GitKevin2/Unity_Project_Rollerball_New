using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static bool firstAccess = true;
    public static int totalCollectables = 0;
    private static CameraController _currentCamera;
    private static List<LevelResult> starsPerLevel = new List<LevelResult>(6);
    
    public static HUD MainHUD { get; set; }

    public static PauseMenuController PauseMenu { get; set; }

    public static CameraController CurrentCamera
    {
        get => _currentCamera;
        set
        {
            _currentCamera = value;
            if (!firstAccess && _currentCamera != null) value.LookSensitivity = Options.LookSensitivty;
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
            Options.LookSensitivty = CurrentCamera.LookSensitivity;
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

    public static void AddLevelResult(int numStars)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        starsPerLevel.Add(new LevelResult(buildIndex, numStars));
    }

    public static List<LevelResult> StarsPerLevel => starsPerLevel;

    public class LevelResult
    {
        public int BuildIndex { get; private set; }
        public int CountStars { get; set; }

        public LevelResult(int buildIndex, int numStars)
        {
            BuildIndex = buildIndex;
            CountStars = numStars;
        }
    }


    public static class Options
    {
        public static float LookSensitivty { get; set; }
    }
}


public static class AudioStore
{
    public static readonly AudioClip EXPLOSION, COLLECTABLE, INTRO_MAIN, INTRO_END, INFO_POPUP;

    static AudioStore()
    {
        string directory = "Sounds/SFX/";
        EXPLOSION = Resources.Load<AudioClip>(directory + "Explosion");
        COLLECTABLE = Resources.Load<AudioClip>(directory + "Collectable_pickup_1");
        INTRO_MAIN = Resources.Load<AudioClip>(directory + "Intro_Dialogue_reverb_v2_trim-end");
        INTRO_END = Resources.Load<AudioClip>(directory + "Intro_Trim-end-sample_reverb");
        INFO_POPUP = Resources.Load<AudioClip>(directory + "Info_ui_sound");

    }
}
