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
    public Sprite sprite; //[VALUE DEFINED FROM EDITOR]
    public Enemy enemyData; //[VALUE DEFINED FROM EDITOR]
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private SpriteRenderer sr;
    private HealthComponent hc;
    #endregion

    #region AI Properties
    private Vector2 currentDirection = Vector2.right;
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
            sr.color = enemyData.IdleColor;
            speed = enemyData.IdleSpeed;
            timeBetweenSwams = enemyData.IdleTimeBetweenSwams;
        }
        else if(newBehaviour == EnemyBehaviour.FollowPlayer)
        {
            sr.color = enemyData.FollowPlayerColor;
            speed = enemyData.FollowPlayerSpeed;
            timeBetweenSwams = enemyData.FollowPlayerTimeBetweenSwams;
        }
        else if(newBehaviour == EnemyBehaviour.Escape)
        {
            sr.color = enemyData.EscapeColor;
            speed = enemyData.EscapeSpeed;
            timeBetweenSwams = enemyData.EscapeTimeBetweenSwams;
        }
        currentBehaviour = newBehaviour;
    }
    #endregion

    #region Unity Engine Loop
    void Start()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        sr.sprite = sprite; //[REVIEW] How to load sprites?

        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        bc = gameObject.AddComponent<BoxCollider2D>();

        hc = gameObject.AddComponent<HealthComponent>();
        hc.Setup(enemyData.MaxHealth);

        SetBehaviour(EnemyBehaviour.Idle);

        Debug.Log("EnemyController: " + gameObject.GetInstanceID());
        Debug.Log("EnemyData: " + enemyData.GetInstanceID());
    }
    void Update()
    {
        timeSinceLastSwam += Time.deltaTime;
        
        //[DEBUG] Change Behaviours each 10 seconds for testing
        testTimeBetweenBehaviours += Time.deltaTime;
        if(testTimeBetweenBehaviours > 10f)   
        {
            if(currentBehaviour == EnemyBehaviour.Idle) SetBehaviour(EnemyBehaviour.FollowPlayer);
            else if(currentBehaviour == EnemyBehaviour.FollowPlayer) SetBehaviour(EnemyBehaviour.Escape);
            else if(currentBehaviour == EnemyBehaviour.Escape) SetBehaviour(EnemyBehaviour.Escape);
            testTimeBetweenBehaviours = 0;
            timeSinceLastSwam = 0;
        }

        //Swim again
        if(timeSinceLastSwam > timeBetweenSwams)
        {
            timeSinceLastSwam = 0;
            currentSpeed = speed;
            
            //Movement Default or Idle
            if(currentBehaviour == EnemyBehaviour.Idle) 
                currentDirection = new Vector2(-currentDirection.x, Random.Range(-0.2f,0.2f)).normalized;
            //Follow Player
            else if(currentBehaviour == EnemyBehaviour.FollowPlayer)
                currentDirection = (Player.transform.position - transform.position).normalized;
            //Escape
            else if(currentBehaviour == EnemyBehaviour.Escape) 
                currentDirection = -(Player.transform.position - transform.position).normalized;
        }
        rb.velocity = PhysicsHelper.calculateVelocity(ref currentSpeed, speed, currentDirection, timeSinceLastSwam);
    }
    #endregion
}
