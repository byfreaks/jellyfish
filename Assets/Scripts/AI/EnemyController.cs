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
    public Animator an;
    #endregion

    #region AI Properties
    public Vector2 CurrentDirection = Vector2.zero;
    public float Speed;
    public float CurrentSpeed;
    public float TimeBetweenActions;
    private float timeSinceLastAction;
    private EnemyBehaviour currentBehaviour;
    private float testTimeBetweenBehaviours = 0;
    #endregion

    #region AI Methods
    public void ChangeBehaviour(EnemyBehaviours newBehaviour)
    {
        while (true)
        {
            if(newBehaviour == EnemyBehaviours.Idle && enemyData.hasIdleBehaviour) 
            {
                currentBehaviour = new Idle();
                break;
            }
            else if(newBehaviour == EnemyBehaviours.FollowPlayer && enemyData.hasFollowPlayerBehaviour) 
            {
                currentBehaviour = new FollowPlayer();
                break;
            }
            else if(newBehaviour == EnemyBehaviours.Attack && enemyData.hasAttackBehaviour) 
            {
                currentBehaviour = new Attack();
                break;
            }
            else if(newBehaviour == EnemyBehaviours.Escape && enemyData.hasEscapeBehaviour)
            {
                currentBehaviour = new Escape();
                break;
            } 
            else
            {
                newBehaviour += 1;
                if(newBehaviour > EnemyBehaviours.Escape) newBehaviour = 0;
            }
        }
        Debug.Log(currentBehaviour.type);
        currentBehaviour.Init(this);
    }
    public void UpdateVelocity()
    {
        //Calculate Velocity
        Vector2 newVelocity = PhysicsHelper.calculateVelocity(ref CurrentSpeed, Speed, CurrentDirection, timeSinceLastAction);
        //Speed limit
        if(newVelocity.magnitude > Speed)
            newVelocity = newVelocity.normalized * Speed;
        //Update Velocity
        rb.velocity = newVelocity;
    }
    public void UpdateSpriteDirection()
    {
        //Sprite point to right or left
        sr.flipX = (CurrentDirection.x > 0);
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

        ChangeBehaviour(EnemyBehaviours.FollowPlayer);
    }
    void Update()
    {
        timeSinceLastAction += Time.deltaTime;
        
        //[DEBUG] Change Behaviours each 10 seconds for testing
        testTimeBetweenBehaviours += Time.deltaTime;
        if(testTimeBetweenBehaviours > 5f)   
        {
            ChangeBehaviour(currentBehaviour.type+1);
            testTimeBetweenBehaviours = 0;
            timeSinceLastAction = 0;
        }

        /* BEHAVIOUR ACTIONS */
        if(TimeBetweenActions != -1 && timeSinceLastAction > TimeBetweenActions)
        {
            timeSinceLastAction = 0;
            currentBehaviour.BehaviorAction(this);
            //Flock adjustments
            CurrentDirection += AIHelper.FlockMainBehaviour(this).normalized;
        }

        UpdateVelocity();
        UpdateSpriteDirection();
    }
    #endregion
}
