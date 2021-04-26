using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeComponent : MonoBehaviour
{

    HealthComponent healthComponent;
    BubbleEmitter be;
    public int health = 3;

    public int amount;
    float speed = 50f;

    float damageCooldown = 0.8f;
    float currentCooldown;

    void Start()
    {
        healthComponent = this.gameObject.GetComponent<HealthComponent>();
        healthComponent.Setup(health);

        be = this.gameObject.GetComponent<BubbleEmitter>();
    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.name.Contains("Fire") && currentCooldown <= 0){
            healthComponent.Hurt();
            currentCooldown = damageCooldown;
        }
    }

    void Update()
    {
        if(currentCooldown >= 0)
            currentCooldown -= Time.deltaTime;

        if(healthComponent.isDead){
            var am = amount == 0 ? Random.Range(2,5) : amount;
            be.Emit(am, speed, Vector2.up);
            GameObject.Destroy(this.gameObject, 0.01f);
        }

    }
}
