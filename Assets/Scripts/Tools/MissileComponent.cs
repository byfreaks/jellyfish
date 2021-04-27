using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponent : MonoBehaviour
{
    SpriteRenderer sr;
    Rigidbody2D rb;
    BoxCollider2D bc;

    [SerializeField] Sprite sprite;

    private float speed;
    private float distance;
    private Vector2 direction;

    private float resistance = 25f;

    private bool launched = false;
    private bool launch = false;

    public void Setup(float speed, float distance, Vector2 direction, bool launch = true){
        this.speed = speed;
        this.distance = distance;
        this.direction = direction;
        this.launch = launch;
    }

    void Start()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingLayerName = "Game";

        rb = gameObject.AddComponent<Rigidbody2D>();
        // rb.bodyType = RigidbodyType2D.Kinematic;

        bc = gameObject.AddComponent<BoxCollider2D>();

        if(launch)
            Launch();

    }

    public void Launch()
    {
        launched = true;
        rb.velocity = direction * speed;
        PointHelper.PointAtTarget( this.transform, PointHelper.MouseWorldPos(), true);
    }

    private void Update() {
        if(launched){
            var drag  = (resistance * Time.deltaTime) / distance;
            var vel = Mathf.SmoothStep(rb.velocity.magnitude, 0, drag);
            rb.velocity = vel * rb.velocity.normalized;
        }
    }

    
}
