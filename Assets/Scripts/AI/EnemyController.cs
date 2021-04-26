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
    [HideInInspector] public EnemyBrain enemyBrain;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public BoxCollider2D bc;
    private SpriteRenderer sr;
    private HealthComponent hc;
    [HideInInspector] public Animator an;
    #endregion

    #region AI Properties
    [HideInInspector] public Vector2 CurrentDirection = Vector2.zero;
    [HideInInspector] public float Speed;
    [HideInInspector] public float CurrentSpeed;
    [HideInInspector] public float TimeBetweenActions;
    private float timeSinceLastAction;
    [HideInInspector] public EnemyBehaviour currentBehaviour;
    #endregion

    #region AI Methods
    public void ChangeBehaviour(EnemyBehaviours newBehaviour)
    {
        //[DEBUG]
        if(currentBehaviour !=null) Debug.Log("Old behaviour:" + currentBehaviour.type);
        //
        if(newBehaviour == EnemyBehaviours.Idle) currentBehaviour = new Idle();
        if(newBehaviour == EnemyBehaviours.FollowPlayer) currentBehaviour = new FollowPlayer();
        if(newBehaviour == EnemyBehaviours.Attack) currentBehaviour = new Attack();
        if(newBehaviour == EnemyBehaviours.Escape) currentBehaviour = new Escape();
        currentBehaviour.Init(this);
        timeSinceLastAction = 0;
        //[DEBUG]
        Debug.Log("New behaviour:" + currentBehaviour.type);
        //
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
    /*public IEnumerator DelayBehaviourTransition(float seconds)
    {

    }*/
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

        enemyBrain = enemyData.enemyBrain;
        ChangeBehaviour(EnemyBehaviours.Idle);
    }
    void Update()
    {
        timeSinceLastAction += Time.deltaTime;
        /* BEHAVIOUR ACTIONS */
        if(timeSinceLastAction > TimeBetweenActions)
        {
            timeSinceLastAction = 0;
            currentBehaviour.BehaviorAction(this);
            //Flock adjustments
            CurrentDirection += AIHelper.FlockMainBehaviour(this).normalized;
        }

        UpdateVelocity();
        UpdateSpriteDirection();
        enemyBrain.CheckConditionsForNewBehaviours(this);
    }
    #endregion
}
