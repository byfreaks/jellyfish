using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float ignoreDeltaLimit = 0.2f;

    [SerializeField] List<Clamp> clamps;
    public int clampToUse = 1;

    void Update () 
    {

        var clamp = clamps[clampToUse-1];

        if (target)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            if(Mathf.Abs(delta.x) < ignoreDeltaLimit)
                delta = new Vector3(0, delta.y, delta.z);
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3( Mathf.Clamp(transform.position.x, clamp.minHorizontalClamp, clamp.maxHorizontalClamp),
                                              Mathf.Clamp(transform.position.y, clamp.minVerticalClamp, clamp.maxVerticalClamp), -10);
        }
    
    }

    public void ResetClamp(int clamp){
        clampToUse = clamp;
    }
}

[System.Serializable]
public class Clamp {
    public float minHorizontalClamp, maxHorizontalClamp;
    public float minVerticalClamp, maxVerticalClamp;
}
