using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public GameObject target;
    public float visionDistance;

    private float roamTimer = 0;
    private float idleTimer = 0;


    void FixedUpdate()
    {
        float speed = 0;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (target == null || distanceToTarget >= visionDistance) {
            // Roam behaviour
            if (idleTimer <= 0) {
                if (roamTimer <= 0) {
                    // Start Iddle and setup roaming
                    idleTimer = Random.Range(.3f, 1f); // Seconds to be stopped.
                    roamTimer = Random.Range(1.5f, 2.2f); // Seconds to roam after idleTimer.
                    moveDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized; // Roam dir
                    print(moveDir);
                }
                else {
                    roamTimer -= Time.deltaTime;
                    speed = walkSpeed;
                }
            }
            else {
                idleTimer -= Time.deltaTime;
            }
        }
        else {
            // Target is nearby, attack
            speed = runSpeed;
        }
        // Move

        Move(speed);
        print(velocity);
        // Face towards movement dir.
        if (velocity != Vector3.zero) {
            transform.forward = moveDir; 
        }
    }
}
