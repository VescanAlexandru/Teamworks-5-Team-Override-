using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour {
    public bool isLobby;
    public SphereCollider containerCollider;
    public SphereCollider groundCollider;
    public GameObject head;
    private Player player;
    
    public float runningSpeed = 1.0f;
    public Transform rightControllerTransform;
    public Transform leftControllerTransform;
    public GameObject cameraRig;
    public Rigidbody cameraRigRigid;
    public Transform playerContainer;

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

    private bool setInSpawn;

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
        setInSpawn = false;
    }

    void Update()
    {
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
        containerCollider.center = new Vector3(head.transform.position.x - transform.position.x, containerCollider.center.y, head.transform.position.z - transform.position.z);
        groundCollider.center = new Vector3(head.transform.position.x - transform.position.x, groundCollider.center.y, head.transform.position.z - transform.position.z);

        if (player == null )//|| !player.alive)
        {
            return;
        }

        if (setInSpawn)
        {
            player.head.transform.position = head.transform.position;
            player.head.transform.rotation = head.transform.rotation;
            player.leftHand.transform.position = leftControllerTransform.position;
            player.leftHand.transform.rotation = leftControllerTransform.rotation;
            player.rightHand.transform.position = rightControllerTransform.position;
            player.rightHand.transform.rotation = rightControllerTransform.rotation;
        } else
        {
            Debug.Log("Parent: " + playerContainer.position);
            Debug.Log("Parent: " + player.transform.position);
            playerContainer.position = player.transform.position;
            setInSpawn = true;
            cameraRigRigid.useGravity = true;
            Debug.Log("Parent: " + playerContainer.position);
            transform.localPosition = new Vector3(0, 0, 0);
        }
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
    }

    public void RightTouchPadPressUp(Vector2 pos)
    {
        rightTouchPadPress = false;
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

    public void SetPlayer(Player player)
    {
        //fade out
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        //fade in
        Debug.Log("player set");
        this.player = player;
    }
}
