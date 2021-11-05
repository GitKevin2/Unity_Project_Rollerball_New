using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Death : MonoBehaviour
{
    private AudioSource voice;

    // Start is called before the first frame update
    void Start()
    {
        voice = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.PLAYER))
        {
            PlayerController player = collider.gameObject.GetComponent<PlayerController>();
            player.Explode();
            StartCoroutine(Kill(player));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(Tags.PLAYER))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Explode();
            StartCoroutine(Kill(player));
        }
    }

    private IEnumerator Kill(PlayerController player)
    {
        player.gameObject.SetActive(false);
        player.Velocity = Vector3.zero;
        GameManager.MainHUD.PlayerVelocity = Vector3.zero;
        yield return new WaitForSeconds(3);
        player.transform.position = player.SpawnPoint;
        player.gameObject.SetActive(true);
        voice.Play();
    }
}
