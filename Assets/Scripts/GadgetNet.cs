using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetNet : Gadget {
    protected override void OnEnemyHit(Enemy enemy) {
        enemy.OnCatch();
    }
}
