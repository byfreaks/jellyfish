using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    HealthComponent hc;
    //Input
    Vector2 inputDirection;
    bool inputSwam;
    float timeSinceLastSwam;

    //Physics
    [Header("PHYSICS")]
    [SerializeField] float waterPullHorizontalRange = 4f;
    Vector2 movement;
    Vector2 direction;
    float currentSpeed;

    //Mechanics
    [Header("MECHANICS")]
    [SerializeField] float maxOxygen = 60f;
    [SerializeField] float currentOxygen;
    [SerializeField] float lastBreath = 6f;

    //Settings
    [Header("SETTINGS")]
    [SerializeField] float movementSpeed = 200;
    [SerializeField] float waterFriction = 25;
    [SerializeField] float timeForWaterPull = 3f;
    [SerializeField] int startingHealth = 6;

    struct status{
        public bool canMove;

        public status Init(){
            canMove = true;

            return this;
        }

        public void Dead(){
            canMove = false;
        }
    }

    status st;

    void InitializeComponents(){
        rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        hc = this.gameObject.AddComponent<HealthComponent>();
        hc.Setup(startingHealth);
        
    }
    
    void InitializeParams(){
        currentOxygen = maxOxygen;
    }

    void Start()
    {
        InitializeParams();
        InitializeComponents();
    }

    void Update()
    {

        st = new status().Init();

        if(hc.isDead)
            st.Dead();

        //Movement
        HandleInput();
        HandleMovement();

        //Loop
        currentOxygen -= Time.deltaTime;

        if(currentOxygen <= lastBreath)
            //TODO: implement vision loss

        if(currentOxygen <= 0)
            hc.Kill();
    }

    #region functions
    bool HandleInput(){
        inputDirection.Set(0,0);
        inputSwam = false;

        if(Input.GetKey(KeyCode.W))
            inputDirection.y += 1;
        if(Input.GetKey(KeyCode.S))
            inputDirection.y -= 1;
        if(Input.GetKey(KeyCode.D))
            inputDirection.x += 1;
        if(Input.GetKey(KeyCode.A))
            inputDirection.x -= 1;

        if(Input.GetKeyDown(KeyCode.Space))
            inputSwam = true;

        inputDirection.Normalize();

        return inputDirection != Vector2.zero || inputSwam != false;
    }

    void HandleMovement(){
            if(inputSwam && st.canMove){
                if(inputDirection != Vector2.zero){
                    direction = inputDirection;
                } else {
                    direction = Vector2.up;
                }
                timeSinceLastSwam = 0;
                currentSpeed = movementSpeed;
            } else {
                timeSinceLastSwam += Time.deltaTime;
            }
            
            var drag = (waterFriction * Time.deltaTime) / 2f;

            currentSpeed = Mathf.SmoothStep(currentSpeed, 0, drag);

            movement = direction * currentSpeed;

        if(timeSinceLastSwam > timeForWaterPull){
            var horPull = waterPullHorizontalRange;
            var verPull = -(waterFriction / 2f);
            var waterPull = new Vector2(Random.Range(-horPull, +horPull), verPull);

            movement += waterPull;
        }

        rb.velocity = movement;
    }
    #endregion
}
