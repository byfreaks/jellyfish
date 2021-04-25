using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunrayBehaviour : MonoBehaviour
{

    [SerializeField] float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] Vector2 pos1, pos2;
    int path = 1;

    void Start()
    {
        if(pos1 == Vector2.zero)
            pos1 = this.transform.position;
    }

    void Update()
    {
        if(path == 1){
            var destination = pos2;
            this.transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            if((pos2 - (Vector2)transform.position).magnitude < 5f) path = 2;
        } else {
            var destination = pos1;
            this.transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            if((pos1 - (Vector2)transform.position).magnitude < 5f) path = 1;
        }
    }
}
