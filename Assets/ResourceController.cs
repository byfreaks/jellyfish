using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{

    BoxCollider2D bc;
    Rigidbody2D rb;
    HealthComponent hc;

    [SerializeField] ResourceData resource;
    [SerializeField] int amount;

    float damageCooldown = 0.8f;
    float currentCooldown;

    private void OnTriggerStay2D(Collider2D other) {
        if(other.name == "Fire" && currentCooldown <= 0){
            hc.Hurt();
            currentCooldown = damageCooldown;
        }
    }

    void Start()
    {
        bc = this.gameObject.AddComponent<BoxCollider2D>();

        rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        hc = this.gameObject.GetComponent<HealthComponent>();
        hc.Setup(3);
        // hc.Setup(3);
    }

    void Update()
    {
        if(currentCooldown >= 0)
            currentCooldown -= Time.deltaTime;

        if(hc.isDead){
            for (int i = 0; i < amount; i++)
            {
                Instantiate(resource.droppable, this.transform.position, Quaternion.identity);
            }
            this.gameObject.SetActive(false);
        }
    }
}