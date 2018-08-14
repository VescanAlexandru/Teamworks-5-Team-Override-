using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightController : MonoBehaviour {
   
    public MyInputManager inputManager;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        inputManager.SetRightController(Controller);
    }

    // Update is called once per frame
    void Update()
    {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            inputManager.RightTouchPadPressUp(Controller.GetAxis());
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            inputManager.RightTouchPadPress();
        }

        if (Controller.GetAxis().x != 0 || Controller.GetAxis().y != 0)
        {
            inputManager.SetRightAxis(Controller.GetAxis());
        } else
        {
            inputManager.ResetRightAxis();
        }
    }
}
