using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchableEnemy : Enemy
{
    private bool catched = false;
    public Renderer helmetRenderer;
    public Material helmetMaterialCalm;
    public Material helmetMaterialAlert;

    public CatchHandler catchHandler;

    public override void OnCatch() {
        // TODO: play sound
        if (catched) {
            return;
        }
        catched = true;
        catchHandler.RegisterCatch();
        Die();
    }

    public override void OnHit() {
        // TODO: play sound
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
