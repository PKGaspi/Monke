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
    public float invencibilityTime = 2.5f;
    public CharacterController controller;
    public Animation onHitAnimation;

    int hp;
    protected Vector3 velocity;
    protected Vector3 moveDir = Vector3.zero;
    protected float invencibilityTimer = 0f;
    protected bool invencible = false;
 
    protected void Start() {
        hp = maxHp;
    }

    void Update() {
        if (IsInvencible()) {
            invencibilityTimer -= Time.deltaTime;
        }
        else if (onHitAnimation != null && onHitAnimation.isPlaying){
            onHitAnimation.Stop();
            onHitAnimation.Rewind("CharacterHit");
        }
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
        if (IsInvencible()) {
            return;
        }
        onHitAnimation.Play();
        invencibilityTimer = invencibilityTime;
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

    public bool IsInvencible() {
        return invencibilityTimer > 0f;
    }
}