using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region [HARDCODE] Load values from other System
    public float SPEED = 100.0f;
    public int HEALTH = 5;
    public float TIME_BETWEEN_SWAMS = 2.5f;
    public float TIME_BETWEEN_SWAMS_FOLLOWING = 0.1f;
    public float SPEED_FOLLOWING = 50.0f;
    #endregion

    #region [REVIEW] Move to other Scripts
    private PlayerController player;
    public PlayerController Player
    { 
        get { 
            if(player == null) player = GameObject.Find("Player").GetComponent<PlayerController>();
            return player;
        } 
    }
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
    private float testTimeBetweenBehaviours = 0;
    [SerializeField] private EnemyBehaviour currentBehaviour;
    #endregion

    #region AI Methods
    public void SetBehaviour(EnemyBehaviour newBehaviour)
    {
        if(newBehaviour == EnemyBehaviour.Idle)
            sr.color = new Color32(255,255,255,255);
        else if(newBehaviour == EnemyBehaviour.FollowPlayer)
            sr.color = new Color32(255,100,0,255);
        currentBehaviour = newBehaviour;
    }
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
        timeSinceLastSwam += Time.deltaTime;
        
        //[DEBUG] Change Behaviours each 10 seconds for testing
        testTimeBetweenBehaviours += Time.deltaTime;
        if(testTimeBetweenBehaviours > 10f)   
        {
            if(currentBehaviour == EnemyBehaviour.Idle) SetBehaviour(EnemyBehaviour.FollowPlayer);
            else if(currentBehaviour == EnemyBehaviour.FollowPlayer) SetBehaviour(EnemyBehaviour.Idle);
            testTimeBetweenBehaviours = 0;
            timeSinceLastSwam = 0;
        }

        //Movement Default or Idle
        if(currentBehaviour == EnemyBehaviour.Idle)
        {
            if(timeSinceLastSwam > enemy.TimeBetweenSwams)
            {
                timeSinceLastSwam = 0;
                currentSpeed = enemy.Speed;
                currentDirection = new Vector2(-(currentDirection.x), Random.Range(-1f,1f));
            }
            rb.velocity = PhysicsHelper.calculateVelocity(ref currentSpeed, enemy.Speed, currentDirection, timeSinceLastSwam);
        }
        
        //Follow Player
        else if(currentBehaviour == EnemyBehaviour.FollowPlayer)
        {
            if(timeSinceLastSwam > TIME_BETWEEN_SWAMS_FOLLOWING)
            {
                timeSinceLastSwam = 0;
                currentSpeed = SPEED_FOLLOWING;
                currentDirection = (Player.transform.position - transform.position);
                currentDirection += new Vector2(Random.Range(-5f,5f), Random.Range(-5f,5f));
                currentDirection = currentDirection.normalized;
            }
            rb.velocity = PhysicsHelper.calculateVelocity(ref currentSpeed, enemy.Speed, currentDirection, timeSinceLastSwam);
        }

        //Attack

        //Run away
    }
    #endregion
}

