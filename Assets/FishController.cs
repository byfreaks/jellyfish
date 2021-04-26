using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishController : MonoBehaviour
{

    Vector2 dir;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float spd;

    Vector2 rnd => new Vector2(0, Random.Range(-1f,1f));

    public void SetUp(Vector2 dir, Sprite spr, Material mat){
        this.dir = dir;
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = spr;
        sr.material = new Material(mat);
        sr.flipX = dir == Vector2.right;

        this.gameObject.AddComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        spd = Random.Range(10f, 32f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name.Contains("DeleteFish")){
            GameObject.Destroy(this.gameObject, 0.01f);
        }
    }

    void Update()
    {
        rb.velocity = (dir + rnd) * spd;
    }
}
