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
    public float invencibilityTime = 1.5f;
    public CharacterController controller;
    public Animator animator;

    public int hp;
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
            animator.SetFloat("invencibilityTimer", invencibilityTimer);
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

    public virtual void OnHit() {
        if (IsInvencible()) {
            return;
        }
        // TODO: Play sound
        invencibilityTimer = invencibilityTime;
        TakeDamage();
    }

    public void TakeDamage(int ammount=1) {
        SetHP(hp - ammount);
        if (hp <= 0) {
            Die();
        }
    }

    public void Heal(int ammount=1) {
        SetHP(hp + ammount);
    }

    private void SetHP(int value) {
        hp = value;
        if (HPBar != null) {
            HPBar.value = hp;
        }
    }

    protected virtual void Die() {
        // TODO: play sound
        // Delete this instance.
        Destroy(gameObject);
    }

    public bool IsInvencible() {
        return invencibilityTimer > 0f;
    }
}