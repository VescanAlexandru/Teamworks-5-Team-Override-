using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayerScript : MonoBehaviour
{

  public  NavMeshAgent nav;

    // Use this for initialization
    private float timeToChangeDirection;

    // Use this for initialization

    public void Start()
    {
        nav = this.GetComponent<NavMeshAgent>();
       nav.transform.Translate(Vector3.forward * Time.deltaTime);
    }
    // Update is called once per frame
    public void Update()
    {
        nav.transform.Translate(Vector3.forward * Time.deltaTime);
        timeToChangeDirection -= Time.deltaTime;      
        if (timeToChangeDirection <= 0)
        {
            ChangeDirection();          
        }
    }
    private void ChangeDirection()
    {     
        timeToChangeDirection = Random.Range(0f,5f);
       // this.transform.rotation = Random.rotation;
        float rotatey = Random.Range(0, 180);
        transform.eulerAngles = new Vector3(0,rotatey*Time.deltaTime,0);
    }
}

