using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnim : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        audioSource.ignoreListenerPause = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.clip == AudioStore.INTRO_MAIN && !audioSource.isPlaying)
        {
            audioSource.clip = AudioStore.INTRO_END;
            audioSource.Play();
            animator.enabled = true;
            animator.Play("intro_animation");
            Debug.Log("Played");
        }
    }


}
