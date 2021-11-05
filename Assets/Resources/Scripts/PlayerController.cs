using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight = 130;
    public Transform playerCamera;
    public ParticleSystem explosion;

    private HUD mainHUD;
    private Vector3 spawnPoint;
    private bool jumping = false;
    private Vector3 movement;
    private Vector2 moveInput;
    private Rigidbody body;
    private AudioSource explosionSound;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Paused = false;
        mainHUD = GameManager.MainHUD;
        spawnPoint = transform.position;
        body = GetComponent<Rigidbody>();
        explosionSound = explosion.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        GetMoveInput();
        Move(movement);
        mainHUD.PlayerVelocity = Velocity;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag(Tags.PICKUP))
        {
            mainHUD.Counter++;
            mainHUD.PlayAudio();
            mainHUD.HighlightCounter();
        }
    }

    public void Explode()
    {
        explosion.transform.position = transform.position;
        explosion.Play();
        explosionSound.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.GROUND) || collision.gameObject.transform.up == Vector3.up)
        {
            jumping = false;
        }

    }

    void GetMoveInput()
    {
        if (moveInput == Vector2.zero) movement = Vector3.zero;
        else
        {
            // moves left/right and backward/forward relative to the camera's direction.
            movement += playerCamera.right * moveInput.x;
            movement += playerCamera.forward * moveInput.y;
            movement.y = 0;
        }
        movement.Normalize();
    }

    Vector3 Move(Vector3 velocity)
    {
        body.AddForce(velocity * speed);
        body.drag = (velocity == Vector3.zero && !jumping) ? 1f : 0f;
        return velocity;
    }

    public void Jump()
    {
        if (!jumping)
        {
            jumping = true;
            body.AddForce(Vector3.up * jumpHeight);
        }
        else
        {
            movement.y = 0;
        }
    }


    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    public Vector3 Movement => movement;

    public Vector3 SpawnPoint => spawnPoint;

    public Vector3 Velocity
    {
        get => body.velocity;
        set => body.velocity = value;
    }


    public HUD Hud => mainHUD;
}
