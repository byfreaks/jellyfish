using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitboxController : MonoBehaviour
{
    private BoxCollider2D hitbox;

    void Start()
    {
        hitbox = gameObject.GetComponent<BoxCollider2D>();
        hitbox.isTrigger = true;
    }

    void Update()   
    {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "Player"){
            HealthComponent hc = other.gameObject.GetComponent<HealthComponent>();
            hc.Hurt();
            SFXHelper.PlayEffect(SFXs.ReceiveDamage);
        }
    }

}
