using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightController : MonoBehaviour {
   
    public MyInputManager inputManager;
    public bool menuScene;
    public LineRenderer menuLineRenderer;

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
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

        if (menuScene)
        {
            menuLineRenderer.gameObject.SetActive(true);
            menuLineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
            {
                menuLineRenderer.SetPosition(1, new Vector3(hit.point.x, hit.point.y, hit.point.z));
                Button button = hit.collider.gameObject.GetComponent<Button>();
                if (button != null)
                {
                    Debug.Log("Button");
                    if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                    {
                        button.onClick.Invoke();
                    }
                }
            }
        } else
        {
            if (menuLineRenderer != null)
            {
                menuLineRenderer.gameObject.SetActive(true);
            }
        }
    }
}
