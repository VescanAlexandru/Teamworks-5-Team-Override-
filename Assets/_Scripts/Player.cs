using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    //public GameObject body;

	// Use this for initialization
	void Start () {
		if (isLocalPlayer)
        {
            GameObject playerContainer = GameObject.Find("PlayerContainer");
            if (playerContainer != null)
            {
                GameObject rig = GameObject.Find("VRCameraRig");
                if (rig != null)
                {
                    MyInputManager myInputManager = rig.GetComponent<MyInputManager>();
                    if (myInputManager != null)
                    {
                        myInputManager.SetPlayer(this);
                    } else
                    {
                        Debug.Log("manager null");
                    }
                } else
                {
                    Debug.Log("rig null");
                }
            } else
            {
                Debug.Log("PlayerContainer is null");
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	}
}
