using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class infoPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(gameObject);
        Debug.Log(isClient);
        Debug.Log(isLocalPlayer);
        Debug.Log(isServer);
	}
}
