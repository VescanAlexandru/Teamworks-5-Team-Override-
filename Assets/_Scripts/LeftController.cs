using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftController : MonoBehaviour
{

    public MyInputManager inputManager;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            inputManager.LeftTouchPadPressUp(Controller.GetAxis());
        }

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            inputManager.LeftTouchPadPress();
        }
    }
}
