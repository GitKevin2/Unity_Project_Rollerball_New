using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoController : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    private readonly Dictionary<Info, string> information = new Dictionary<Info, string>();
    private readonly Queue<Info> infoQueue = new Queue<Info>();

    void Start()
    {
        infoText.text = string.Empty;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInfo(Info infoName)
    {
        GameManager.MainHUD.PlayAudio(AudioStore.INFO_POPUP);
        if (information.ContainsKey(infoName))
            infoText.text = information[infoName];
        else
            Debug.LogWarning("\"" + infoName + "\" does not exist.");
    }

    public void AddToInfo(Info infoName, string path)
    {
        string newInfo = File.ReadAllText(path);
        if (information.ContainsKey(infoName))
            information[infoName] = newInfo;
        else
            information.Add(infoName, newInfo);
    }

    public void QueueInfo(Info infoName)
    {
        if (false == infoQueue.Contains(infoName)) infoQueue.Enqueue(infoName);
        if (false == gameObject.activeInHierarchy) RunQueue();
    }

    public void RunQueue()
    {
        if (infoQueue.Count == 0) return;
        gameObject.SetActive(true);
        StartCoroutine(ShowInfoCoroutine(infoQueue.Peek()));
    }

    private IEnumerator ShowInfoCoroutine(Info infoName)
    {
        SetInfo(infoName);
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        infoQueue.Dequeue();
        RunQueue();
    }
}


public enum Info
{
    None,
    LevelComplete,
    Controls,
    PauseMenu,
    Collectables,
    LaunchPad,
    WeakLaunchPad,
    DirectionalPad,
    BouncePad,
    DeathPit,
    DeathBlock
};
