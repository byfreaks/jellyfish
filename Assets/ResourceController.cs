using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ResourceController : MonoBehaviour
{

    PolygonCollider2D pc;
    Rigidbody2D rb;
    HealthComponent hc;
    Animator an;

    [SerializeField] ResourceData resource;
    [SerializeField] int minAmount;
    [SerializeField] int maxAmount;
    [SerializeField] Light2D resLight;

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
        pc = this.gameObject.GetComponent<PolygonCollider2D>();

        rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        hc = this.gameObject.GetComponent<HealthComponent>();
        hc.Setup(3);

        an = this.gameObject.GetComponent<Animator>();
    }

    float rndOffset => Random.Range(-1.2f, 1.2f);

    void Update()
    {
        if(currentCooldown >= 0)
            currentCooldown -= Time.deltaTime;

        if(resLight)
        {
            var currIn = resLight.intensity;
            resLight.intensity = Mathf.Clamp(Mathf.Sin(Time.time * 1.3f), 0, 1);
            Debug.Log(Mathf.Sin(Time.time) );
        }

        if(hc.isDead){
            var drops = Random.Range(minAmount, maxAmount);
            for (int i = 0; i < drops; i++)
            {
                Instantiate(resource.droppable, this.transform.position, Quaternion.identity);
            }
            this.gameObject.SetActive(false);
        }
    }
}