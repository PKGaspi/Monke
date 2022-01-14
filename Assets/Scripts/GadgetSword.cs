using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetSword : Gadget {

    public Animation animation;

    public override void Use(Vector2 dir) {
        animation.Play();
    }
}
