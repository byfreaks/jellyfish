using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float slowSpeed; //once it reaches this vel, it moves linearly upwards
    [SerializeField] float bubbleDrag;

    [SerializeField] Vector2 impulseDirection;
    [SerializeField] float upwardsOffset;

    Rigidbody2D rb;

    public void Initialize(Vector2 dir, float speed, Sprite spr){
        impulseDirection = dir;
        this.speed = speed;
        GetComponent<SpriteRenderer>().sprite = spr;
    }

    private void Start() {
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(speed > slowSpeed)
        {
            var drag = (bubbleDrag * Time.deltaTime) / 0.5f;
            speed = Mathf.SmoothStep(speed, slowSpeed, drag);   
            rb.velocity = impulseDirection * speed;
        } else {
            rb.velocity = (Vector2.up + new Vector2(Random.Range(-upwardsOffset, upwardsOffset), Random.Range(0f, 2f)) ) * slowSpeed;
        }
    }
}
