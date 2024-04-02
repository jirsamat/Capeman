using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    
    [SerializeField] Transform target;
    private Vector3 velocity = Vector3.zero;
    public Vector3 CameraVector = new Vector3(0, 5, -20);
    public float smoothTime = 0.5f;

    // Update is called once per frame
    void Update()
    {
        //targets the player
        Vector3 targetPosition = target.TransformPoint(CameraVector);
        //smoothens the following of the player
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
