using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float boostMultiplier = 50f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PLAYER))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            var boost = transform.up * boostMultiplier;
            if ((player.Velocity + boost).y < boost.y)
            {

                player.Velocity = boost;
            }
            else
                player.Velocity += boost;

        }
    }
}
