using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public Rigidbody playerContainer;

    private GameObject ground;
    
    private void OnTriggerEnter(Collider col)
    {
        ground = col.gameObject;
        playerContainer.useGravity = false;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject == ground)
        {
            playerContainer.useGravity = true;
        }
    }
}
