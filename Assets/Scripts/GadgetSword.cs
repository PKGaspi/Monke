using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetSword : Gadget {


    protected override void Activate() {
        activateAnimation.Play();
    }
}
