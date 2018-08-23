using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyInputManager : NetworkBehaviour {

    public GameObject head;
    private GameObject player;
    
    public float runningSpeed = 1.0f;
    public Transform rightControllerTransform;
    public Transform leftControllerTransform;
    public GameObject cameraRig;
    public Rigidbody cameraRigRigid;

    private SteamVR_Controller.Device rightController;
    private SteamVR_Controller.Device leftController;

    private bool leftTouchPadPress;
    private bool rightTouchPadPress;

    private Vector3 prevPosLeft;
    private Vector3 prevParentPosLeft;
    private Vector3 prevDiffPosLeft;
    private Vector3 newPosLeft;
    private Vector3 newParentPosLeft;
    private Vector3 newDiffPosLeft;
    private Vector3 controllerVelocityLeft;

    private Vector3 prevPosRight;
    private Vector3 prevParentPosRight;
    private Vector3 prevDiffPosRight;
    private Vector3 newPosRight;
    private Vector3 newParentPosRight;
    private Vector3 newDiffPosRight;
    private Vector3 controllerVelocityRight;

    private Vector2 leftAxis;
    private Vector2 rightAxis;

    void Start()
    {
        //left
        prevPosLeft = leftControllerTransform.position;
        prevParentPosLeft = cameraRig.transform.position;
        prevDiffPosLeft = prevParentPosLeft - prevPosLeft;
        //right
        prevPosRight = rightControllerTransform.position;
        prevParentPosRight = cameraRig.transform.position;
        prevDiffPosRight = prevParentPosRight - prevPosRight;
    }

    void Update()
    {
       /* if (isServer)
        {
            if (player != null)
            {
                selectedUnit = GameObject.Find("Unit(Clone)");
            }
        }*/
        //left
        newPosLeft = leftControllerTransform.position;
        newParentPosLeft = cameraRig.transform.position;
        newDiffPosLeft = newParentPosLeft - newPosLeft;
        controllerVelocityLeft = (newDiffPosLeft - prevDiffPosLeft) / Time.deltaTime;
        prevPosLeft = newPosLeft;
        prevParentPosLeft = newParentPosLeft;
        prevDiffPosLeft = newDiffPosLeft;

        //right
        newPosRight = rightControllerTransform.position;
        newParentPosRight = cameraRig.transform.position;
        newDiffPosRight = newParentPosRight - newPosRight;
        controllerVelocityRight = (newDiffPosRight - prevDiffPosRight) / Time.deltaTime;
        prevPosRight = newPosRight;
        prevParentPosRight = newParentPosRight;
        prevDiffPosRight = newDiffPosRight;

        //Movement
        UpdateRunning();
        //Debug.Log(player);
        //player.gameObject.transform.position = head.transform.position - new Vector3(0, 1, 0);
        //Debug.Log(player);
        if (player != null)
        {
            player.transform.position = head.transform.position - new Vector3(0, 1, 0);
        } else
        {
            player = GameObject.Find("PlayerNetworkObject(Clone)");
        }
    }

    public void SetRightController(SteamVR_Controller.Device controller)
    {
        rightController = controller;
    }

    public void SetLeftController(SteamVR_Controller.Device controller)
    {
        leftController = controller;
    }

    public void LeftTouchPadPress()
    {
        leftTouchPadPress = true;
        newPosLeft = leftControllerTransform.position;
    }

    public void RightTouchPadPress()
    {
        rightTouchPadPress = true;
        newPosRight = rightControllerTransform.position;
    }

    public void LeftTouchPadPressUp(Vector2 pos)
    {
        leftTouchPadPress = false;
        leftAxis = pos;
    }

    public void RightTouchPadPressUp(Vector2 pos)
    {
        rightTouchPadPress = false;
        rightAxis = pos;
    }

    public void SetLeftAxis(Vector2 axis)
    {
        leftAxis = axis;
    }

    public void SetRightAxis(Vector2 axis)
    {
        rightAxis = axis;
    }

    public void ResetLeftAxis()
    {
        leftAxis = new Vector2(0, 0);
    }

    public void ResetRightAxis()
    {
        rightAxis = new Vector2(0, 0);
    }

    private void UpdateRunning()
    {
        float leftY = 0;
        float rightY = 0;
        if (leftTouchPadPress)
        {
            leftY = (cameraRig.transform.rotation.eulerAngles.y - leftControllerTransform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
        }

        if (rightTouchPadPress)
        {
            rightY = (cameraRig.transform.rotation.eulerAngles.y - rightControllerTransform.rotation.eulerAngles.y + 90) * Mathf.Deg2Rad;
        }

        if (leftTouchPadPress && rightTouchPadPress)
        {
            cameraRigRigid.AddForce((Vector3.Magnitude(controllerVelocityLeft) + Vector3.Magnitude(controllerVelocityRight)) / 2 * runningSpeed * new Vector3(Mathf.Cos((leftY + rightY) / 2),
                0,
                Mathf.Sin((leftY + rightY) / 2)));
            return;
        }

        if (leftTouchPadPress)
        {
            cameraRigRigid.AddForce(Vector3.Magnitude(controllerVelocityLeft) * runningSpeed * new Vector3(Mathf.Cos(leftY),
                0,
                Mathf.Sin(leftY)));
            return;
        }

        if (rightTouchPadPress)
        {
            cameraRigRigid.AddForce(Vector3.Magnitude(controllerVelocityRight) * runningSpeed * new Vector3(Mathf.Cos(rightY),
                0,
                Mathf.Sin(rightY)));
            return;
        }
    }
}
