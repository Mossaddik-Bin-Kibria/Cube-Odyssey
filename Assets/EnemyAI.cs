using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Player's transform
    public float speed = 5.0f; // Speed at which the enemy moves
    public float followDistance = 10.0f; // Distance within which the enemy will start following

    private bool isFollowing = false; // Is the enemy currently following the player?

    void Update()
    {
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        // Check if the player is within the follow distance
        if (distanceToPlayer < followDistance)
        {
            isFollowing = true;
        }
        else
        {
            isFollowing = false;
        }

        // Move towards the player if following
        if (isFollowing)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
