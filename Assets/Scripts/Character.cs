using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    protected const float GRAVITY = -15.0f;
    
    public int maxHp = 5;
    public float walkSpeed = 3.0f;
    public float runSpeed = 4;
    public float jumpSpeed = 6.0f;
    public UIBar HPBar;
    public CharacterController controller;

    int hp;
    protected Vector3 velocity;
    protected Vector3 moveDir = Vector3.zero;

    void Start() {
        hp = maxHp;
    }

    protected void Move(float speed) {
        // Apply gravity
        velocity.y += GRAVITY * Time.deltaTime;
        velocity.x = speed * moveDir.x;
        velocity.z = speed * moveDir.z;

        // Apply movement.
        controller.Move(velocity * Time.deltaTime);

        // Update values after movement.
        velocity = controller.velocity;
    }

    public void OnHit() {
        TakeDamage();
    }

    public void TakeDamage(int dmg=1) {
        hp -= dmg;
        if (HPBar != null) {
            HPBar.value = hp;
        }
        if (hp <= 0) {
            Die();
        }
    }

    private void Die() {
        print("im ded");
    }
}