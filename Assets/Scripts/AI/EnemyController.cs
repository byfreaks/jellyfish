using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region [HARDCODE]
    public float SPEED = 100.0f;
    public int HEALTH = 5;
    public float TIME_BETWEEN_SWAMS = 2.5f;
    #endregion

    #region Entities Related
    public Sprite sprite; //[VALUE DEFINED FROM EDITOR]
    private Enemy enemy;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private HealthComponent hc;
    #endregion

    #region AI Properties
    private Vector2 currentDirection = Vector2.right;
    private float currentSpeed = 0;
    private float timeSinceLastSwam = 0;
    #endregion

    #region Unity Engine Loop
    void Start()
    {
        enemy = new Enemy(SPEED, HEALTH, TIME_BETWEEN_SWAMS);

        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite; //[REVIEW] How to load sprites?

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        bc = gameObject.AddComponent<BoxCollider2D>();

        hc = gameObject.AddComponent<HealthComponent>();
        hc.Setup(enemy.MaxHealth);
    }
    void Update()
    {
        
        //Movement Default or Idle
        timeSinceLastSwam += Time.deltaTime;
        if(timeSinceLastSwam > enemy.TimeBetweenSwams)
        {
            timeSinceLastSwam = 0;
            currentSpeed = enemy.Speed;
            currentDirection = new Vector2(-(currentDirection.x), Random.Range(-1f,1f));
            Debug.Log(rb.velocity);
        }
        rb.velocity = PhysicsHelper.calculateVelocity(ref currentSpeed, enemy.Speed, currentDirection, timeSinceLastSwam);
        
        //Follow Player

        //Attack

        //Run away
    }
    #endregion
}

