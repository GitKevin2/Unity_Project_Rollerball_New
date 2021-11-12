using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // Animation is played before entire voiceover is finished.
        if (audioSource.clip == AudioStore.INTRO_MAIN && !audioSource.isPlaying)
        {
            audioSource.clip = AudioStore.INTRO_END;
            audioSource.Play();
            animator.enabled = true;
            animator.Play("intro_animation");
            Debug.Log("Played");
        }

        // Use the ENTER key to skip the Intro cutscene.
        if (UnityEngine.InputSystem.Keyboard.current.enterKey.isPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
