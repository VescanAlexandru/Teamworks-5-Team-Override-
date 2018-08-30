using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform mainCamera;   //The camera's transform


    void Start()
    {
        //Set the Main Camera as the target
        mainCamera = Camera.main.transform;
    }

    //Update after all other updates have run
    void LateUpdate()
    {
        if (mainCamera == null)
            return;
        
        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.position);
    }
}