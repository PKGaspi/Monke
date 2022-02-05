using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GadgetHandler : MonoBehaviour {

    public Gadget[] equipedGadgets = new Gadget[4];
    public Gadget currentGadget;
    public AudioSource setSound;

    public void Use(float inclination) {
        currentGadget.Use(inclination);
    }

    public void OnSetGadget(InputAction.CallbackContext value) {
        if (!value.started) {
            return;
        }

        int gadgetIndex = (int) value.ReadValue<float>();
        
        if (equipedGadgets[gadgetIndex]) {
            SetGadget(equipedGadgets[gadgetIndex]);
        }
    }

    public void SetGadget(Gadget gadget) {
        if (currentGadget.GetType() == gadget.GetType()) {
            return;
        }
        setSound.Play();
        Destroy(currentGadget.gameObject);
        currentGadget = Object.Instantiate(gadget, transform.position, transform.rotation);
        currentGadget.transform.parent = transform;
    }
}
