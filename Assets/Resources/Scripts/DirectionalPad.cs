using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalPad : MonoBehaviour
{

    public int speedMultiplier = 30;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Tags.PLAYER))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            //Add to velocity if player approaches from behind, else replace the velocity.
            if (Vector3.Dot(transform.forward, player.transform.position - transform.position) < 0)
            {
                player.Velocity += transform.forward * speedMultiplier;
            }
            else
                player.Velocity = transform.forward * speedMultiplier;

        }
    }
}
