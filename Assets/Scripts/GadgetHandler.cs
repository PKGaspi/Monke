using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GadgetHandler : MonoBehaviour {

    public Gadget[] equipedGadgets = new Gadget[4];
    public Gadget currentGadget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        if (currentGadget == gadget) {
            return;
        }
        Destroy(currentGadget.gameObject);
        currentGadget = Object.Instantiate(gadget, transform.position, transform.rotation);
        currentGadget.transform.parent = transform;
    }
}
