using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour
{

    public Vector3 pos1, pos2;
    public int path = 1;
    public float dampTime;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        if(path == 1){
            var destination = pos2;
            this.transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            if((pos2 - transform.position).magnitude < 5f) path = 2;
        } else {
            var destination = pos1;
            this.transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            if((pos1 - transform.position).magnitude < 5f) path = 1;
        }
    }
}