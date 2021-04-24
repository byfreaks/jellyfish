using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    //Input
    Vector2 inputDirection;
    bool inputSwam;
    float timeSinceLastSwam;

    //Physics
    Vector2 movement;
    Vector2 direction;
    float currentSpeed;
    [SerializeField] float waterPullHorizontalRange = 4f;

    //Settings
    [SerializeField] float movementSpeed = 200;
    [SerializeField] float waterFriction = 25;
    [SerializeField] float timeForWaterPull = 3f;

    void InitializeComponents(){
        rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        InitializeComponents();
    }

    void Update()
    {
        HandleInput();
        HandleMovement();
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
        if(inputSwam){
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
