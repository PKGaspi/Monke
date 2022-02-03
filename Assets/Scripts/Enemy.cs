using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public GameObject target;
    public float visionDistance;
    public WeightedDrop[] drops;

    protected float hitstunTimer = 0;
    protected float roamTimer = 0;
    protected float idleTimer = 0;


    void FixedUpdate()
    {
        hitstunTimer -= Time.deltaTime;
        if (hitstunTimer > 0) {
            return;
        }
        float speed = 0;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (target == null || distanceToTarget >= visionDistance) {
            speed = OnTargetNotInRange();
        }
        else {
            // Target is nearby
            speed = OnTargetInRange();
        }

        // Move
        
        Move(speed);
        // Face towards movement dir.
        if (velocity != Vector3.zero) {
            transform.forward = moveDir; 
        }
    }
    
    
    void OnControllerColliderHit(ControllerColliderHit col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player")) {
            // Hit player
            ThirdPersonCharacter character = col.gameObject.GetComponent<ThirdPersonCharacter>();
            if (character) {
                character.OnHit();
            }
        }
    }
    
    public virtual void OnCatch() {
        // Do nothing by default.
    }


    protected override void Die() {
        GameObject drop = Drop();
        if (drop != null) {
            Instantiate(drop, transform.position, transform.rotation);
        }
        base.Die();
    }

    private GameObject Drop() {
        int totalWeight = 0;
        foreach (WeightedDrop drop in drops) {
            totalWeight += drop.weight;
        }
        int random = Random.Range(0, totalWeight);
        foreach (WeightedDrop drop in drops) {
            if (random <= drop.weight) {
                return drop.gameObject;
            }
        }
        return null;
    }

    protected virtual float OnTargetNotInRange() {
        // Roam behaviour
        if (idleTimer <= 0) {
            if (roamTimer <= 0) {
                // Start Iddle and setup roaming
                idleTimer = Random.Range(.3f, 1f); // Seconds to be stopped.
                roamTimer = Random.Range(1.5f, 2.2f); // Seconds to roam after idleTimer.
                moveDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized; // Roam dir
            }
            else {
                roamTimer -= Time.deltaTime;
                return walkSpeed;
            }
        }
        else {
            idleTimer -= Time.deltaTime;
        }
        return 0;
    }

    protected virtual float OnTargetInRange() {
        // Chase by default.
        Vector3 targetDir = (target.transform.position - transform.position);
        targetDir.y = 0;
        moveDir = targetDir.normalized;
        return runSpeed;
    }
}
