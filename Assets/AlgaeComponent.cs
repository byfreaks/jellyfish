using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeComponent : MonoBehaviour
{

    HealthComponent healthComponent;
    BubbleEmitter be;

    public int amount;
    float speed = 50f;

    void Start()
    {
        healthComponent = this.gameObject.GetComponent<HealthComponent>();
        be = this.gameObject.GetComponent<BubbleEmitter>();
    }


    void Update()
    {
        if(healthComponent.isDead){
            be.Emit(6, speed, Vector2.up);
            GameObject.Destroy(this.gameObject, 0.01f);
        }

    }
}
