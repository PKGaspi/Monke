using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchableEnemy : Enemy
{
    public Renderer helmetRenderer;
    public Material helmetMaterialCalm;
    public Material helmetMaterialAlert;

    public override void OnCatch() {
        // TODO: Increment catch counter
        print("catch!");
        Die();
    }

    public override void OnHit() {
        // Dont loose hp. Just hitstun.
        hitstunTimer = .5f;
    }

    protected override float OnTargetNotInRange() {
        helmetRenderer.material = helmetMaterialCalm;
        return base.OnTargetNotInRange();
    }

    protected override float OnTargetInRange() {
        // Run away
        float speed = base.OnTargetInRange();
        helmetRenderer.material = helmetMaterialAlert;
        moveDir = -moveDir;
        return speed;
    }

}
