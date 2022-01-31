using UnityEngine;

public abstract class Gadget : MonoBehaviour {

    public Animation activateAnimation;
    public float activeTime = .3f;

    private Vector3 defaultRotation;
    private Vector3 defaultPosition;
    private Vector3 inclinatedRotation = new Vector3(0f, 0f, 70f);
    private Vector3 inclinatedPosition = new Vector3(-.3f, -.35f, 0f);

    protected const float NEEDED_SPEED = .01f;

    protected float inclination = 0f; // From 0 to 1, how much actuated this gadget is.
    protected float speed = 0f; // Speed at which the gadget was used.
    protected float activeTimer = 0f;


    public void Start() {
        defaultRotation = transform.localRotation.eulerAngles;
        defaultPosition = transform.localPosition;
    }

    public void Update() {
        transform.localRotation = Quaternion.Euler(Vector3.Lerp(defaultRotation, inclinatedRotation, inclination));
        transform.localPosition = Vector3.Lerp(defaultPosition, inclinatedPosition, inclination);
        activeTimer = Mathf.Max(0, activeTimer - Time.deltaTime);
    }

    public void Use(float inclination) {
        this.speed = ((inclination - this.inclination) + this.speed) / 2;
        this.speed = Mathf.Max(0, this.speed);
        this.inclination = inclination;

        if (speed >= NEEDED_SPEED && inclination >= 1) {
            Activate();
        }
    }

    protected void Activate() {
        activateAnimation.Play();
        activeTimer = activeTime;
    }

    public bool IsActive() {
        return activeTimer > 0;
    }

    private void OnTriggerEnter(Collider col) {
        Enemy enemy = col.gameObject.GetComponent<Enemy>();
        if (enemy && IsActive()) {
            OnEnemyHit(enemy);
        }
    }

    protected abstract void OnEnemyHit(Enemy enemy);

}