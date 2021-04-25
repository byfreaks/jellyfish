using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

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
    public Enemy enemyData; //[VALUE DEFINED FROM EDITOR]
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D bc;
    private SpriteRenderer sr;
    private HealthComponent hc;
    private Animator an;
    #endregion

    #region AI Properties
    private Vector2 currentDirection = Vector2.zero;
    private float speed;
    private float currentSpeed = 0;
    private float timeBetweenSwams;
    private float timeSinceLastSwam = 0;
    private float testTimeBetweenBehaviours = 0;
    [SerializeField] private EnemyBehaviour currentBehaviour;
    #endregion

    #region AI Methods
    public void SetBehaviour(EnemyBehaviour newBehaviour)
    {
        if(newBehaviour == EnemyBehaviour.Idle)
        {
            speed = enemyData.IdleSpeed;
            timeBetweenSwams = enemyData.IdleTimeBetweenSwams;
        }
        else if(newBehaviour == EnemyBehaviour.FollowPlayer)
        {
            speed = enemyData.FollowPlayerSpeed;
            timeBetweenSwams = enemyData.FollowPlayerTimeBetweenSwams;
        }
        else if(newBehaviour == EnemyBehaviour.Escape)
        {
            speed = enemyData.EscapeSpeed;
            timeBetweenSwams = enemyData.EscapeTimeBetweenSwams;
        }
        currentBehaviour = newBehaviour;
    }
    public void CalculateNewDirection()
    {
        //Follow Player
        if(currentBehaviour == EnemyBehaviour.FollowPlayer)
            currentDirection = (Player.transform.position - transform.position).normalized * enemyData.FollowPlayerWeight;
        //Escape
        else if(currentBehaviour == EnemyBehaviour.Escape) 
            currentDirection = -(Player.transform.position - transform.position).normalized * enemyData.EscapeWeight;
    
        //Flock adjustments
        currentDirection += AIHelper.FlockMainBehaviour(this).normalized;
    }
    public void MoveEnemy()
    {
        //Move
        Vector2 newVelocity = PhysicsHelper.calculateVelocity(ref currentSpeed, speed, currentDirection, timeSinceLastSwam);
        //Speed limit
        if(newVelocity.magnitude > speed)
            newVelocity = newVelocity.normalized * speed;
        rb.velocity = newVelocity;
    }
    public void HandleAnimation()
    {
        //Sprite point to right or left
        sr.flipX = (currentDirection.x > 0);
    }
    #endregion

    #region Unity Engine Loop
    void Start()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = enemyData.sprite;
        sr.material = enemyData.material;

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        bc = gameObject.AddComponent<BoxCollider2D>();

        hc = gameObject.AddComponent<HealthComponent>();
        hc.Setup(enemyData.MaxHealth);

        an = gameObject.AddComponent<Animator>();
        an.runtimeAnimatorController = enemyData.animatorController;

        SetBehaviour(EnemyBehaviour.Idle);
    }
    void Update()
    {
        timeSinceLastSwam += Time.deltaTime;
        
        //[DEBUG] Change Behaviours each 10 seconds for testing
        testTimeBetweenBehaviours += Time.deltaTime;
        if(testTimeBetweenBehaviours > 5f)   
        {
            if(currentBehaviour == EnemyBehaviour.Idle) SetBehaviour(EnemyBehaviour.FollowPlayer);
            else if(currentBehaviour == EnemyBehaviour.FollowPlayer) SetBehaviour(EnemyBehaviour.Escape);
            else if(currentBehaviour == EnemyBehaviour.Escape) SetBehaviour(EnemyBehaviour.Idle);
            testTimeBetweenBehaviours = 0;
            timeSinceLastSwam = 0;
        }

        //Each x seconds is calculated a new direction
        if(timeSinceLastSwam > timeBetweenSwams)
        {
            timeSinceLastSwam = 0;
            currentSpeed = speed;
            CalculateNewDirection();
        }

        //Update velocity
        MoveEnemy();
        HandleAnimation();
    }
    #endregion
}
