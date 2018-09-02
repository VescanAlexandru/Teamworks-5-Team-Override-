using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Networking;

public class AttackHandler : MonoBehaviour {

    //[ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        //if (isServer)
        //{
        
            if (other.CompareTag("PlayerHead") && other.transform.parent != transform.parent) //Don't eliminate self
            {
                Debug.Log("Hit");
                Player otherPlayer = other.transform.parent.gameObject.GetComponent<Player>();
                otherPlayer.Eliminate();//null here
            }
            //}
    }
}
