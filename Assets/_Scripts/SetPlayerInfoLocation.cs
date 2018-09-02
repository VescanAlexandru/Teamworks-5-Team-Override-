using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerInfoLocation : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
	}
}
