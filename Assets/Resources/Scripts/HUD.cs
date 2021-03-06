using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class HUD : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public LevelComplete levelCompleteBox;
    public SpeedBar speedBar;
    public InfoController infoBox;
    public Gradient collectableHighlighter;

    private AudioSource audioSource;
    private int counter;
    private Vector3 playerVelocity;
    private Timer timer;
    private int timeTaken = 0;
    private float colorValue = 0f;

    private void Awake()
    {
        GameManager.MainHUD ??= this;
    }


    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Timer>();
        audioSource = GetComponent<AudioSource>();
        Counter = GameManager.totalCollectables;
        SetSpeedValue(0.0f);
        speedBar.MaxSpeed = 50f;


        if (SceneManager.GetActiveScene().name == "L1") ShowInfo(Info.Controls);
    }

    void FixedUpdate()
    {
        SetSpeedValue();
    }

    private void Update()
    {
        colorValue = Mathf.Clamp01(colorValue - 2f * Time.deltaTime);
        counterText.color = collectableHighlighter.Evaluate(colorValue);
    }

    private void OnDestroy()
    {
        if (GameManager.MainHUD == this)
        {
            GameManager.Save<HUD>();
            GameManager.MainHUD = null;
        }
    }

    public void SetSpeedValue(float value)
    {
        speedBar.Speed = Mathf.Lerp(speedBar.Speed, value, 15f * Time.fixedDeltaTime);
    }

    public void SetSpeedValue() => SetSpeedValue(new Vector2(playerVelocity.x, playerVelocity.z).magnitude);

    public void WinCond()
    {
        ShowInfo(Info.LevelComplete);
        FinishTime();
    }

    public void ShowInfo(Info infoName) => infoBox.QueueInfo(infoName);


    public void StartTime() => timer.Begin();

    public void FinishTime()
    {
        timer.End();
        levelCompleteBox.gameObject.SetActive(true);
        levelCompleteBox.SetTime(timer);
        levelCompleteBox.SetCollectables(Counter);
        levelCompleteBox.Rate();
        timer.ResetTime();
    }
    
    public void PlayAudio()
    {
        audioSource.Play();
        
    }

    public void PlayAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void HighlightCounter() => colorValue = 1f;


    public int Counter
    {
        get => counter;
        set
        {
            counter = value;
            counterText.text = value.ToString();
        }
    }

    public Vector3 PlayerVelocity
    {
        get => playerVelocity;
        set => playerVelocity = value;
    }

    public double TimeTaken => timeTaken;
}

