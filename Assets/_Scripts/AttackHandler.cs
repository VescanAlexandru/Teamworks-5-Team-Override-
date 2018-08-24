using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHandler : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHead") && other.transform.parent != transform.parent) //Don't eliminate self
        {
            other.GetComponent<Player>().Eliminate();
        }
    }
}
