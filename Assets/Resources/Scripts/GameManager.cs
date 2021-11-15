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
    private static readonly Dictionary<Info, string> information = new Dictionary<Info, string>();

    static GameManager()
    {
        AddToInfo(Info.LevelComplete, InfoFiles.LEVEL_COMPLETE);
        AddToInfo(Info.Controls, InfoFiles.DISCUSS_CONTROLS);
        AddToInfo(Info.PauseMenu, InfoFiles.PAUSE_MENU);
        AddToInfo(Info.Collectables, InfoFiles.COLLECTABLES);
        AddToInfo(Info.LaunchPad, InfoFiles.LAUNCH_PAD);
        AddToInfo(Info.WeakLaunchPad, InfoFiles.WEAK_LAUNCH_PAD);
        AddToInfo(Info.DirectionalPad, InfoFiles.DIRECTIONAL_PAD);
        AddToInfo(Info.BouncePad, InfoFiles.BOUNCE_PAD);
        AddToInfo(Info.DeathBlock, InfoFiles.DEATH_BLOCK);
        AddToInfo(Info.DeathPit, InfoFiles.DEATH_PIT);
        AddToInfo(Info.HUD, InfoFiles.HUD);
    }

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

    public static T Get<T>()
    {
        if (MainHUD is T hud)
        {
            return hud;
        }
        else if (CurrentCamera is T camera)
        {
            return camera;
        }
        else if (PauseMenu is T pauseMenu)
        {
            return pauseMenu;
        }
        else throw new InvalidOperationException();
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

    public static void AddToInfo(Info infoName, TextAsset textAsset)
    {
        InfoFiles.added = true;
        var newInfo = textAsset.text;
        if (information.ContainsKey(infoName))
            information[infoName] = newInfo;
        else
            information.Add(infoName, newInfo);
    }

    public static void AddLevelResult(int numStars)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        starsPerLevel.Add(new LevelResult(buildIndex, numStars));
    }

    public static List<LevelResult> StarsPerLevel => starsPerLevel;

    public static Dictionary<Info, string> InformationList => information;
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

public static class InfoFiles
{
    public static bool added = false;
    public static readonly TextAsset LEVEL_COMPLETE, DISCUSS_CONTROLS, PAUSE_MENU, COLLECTABLES, 
        LAUNCH_PAD, WEAK_LAUNCH_PAD, DIRECTIONAL_PAD, BOUNCE_PAD, DEATH_PIT, DEATH_BLOCK, HUD;

    static InfoFiles()
    {
        string directory = "_Misc/Infofiles/";
        LEVEL_COMPLETE = Resources.Load<TextAsset>(directory + "level_complete");
        DISCUSS_CONTROLS = Resources.Load<TextAsset>(directory + "discuss_controls");
        PAUSE_MENU = Resources.Load<TextAsset>(directory + "pause_menu");
        COLLECTABLES = Resources.Load<TextAsset>(directory + "collectables");
        LAUNCH_PAD = Resources.Load<TextAsset>(directory + "launch_pad");
        WEAK_LAUNCH_PAD = Resources.Load<TextAsset>(directory + "weak_launch_pad");
        DIRECTIONAL_PAD = Resources.Load<TextAsset>(directory + "directional_pad");
        BOUNCE_PAD = Resources.Load<TextAsset>(directory + "bounce_pad");
        DEATH_PIT = Resources.Load<TextAsset>(directory + "death_pit");
        DEATH_BLOCK = Resources.Load<TextAsset>(directory + "death_block");
        HUD = Resources.Load<TextAsset>(directory + "hud");
    }
}
