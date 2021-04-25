using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    HealthComponent hc;
    SpriteRenderer sr;
    Animator an;

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
    Controller2D controller;

    //Mechanics
    [Header("MECHANICS")]
    [SerializeField] float maxOxygen = 60f;
    [SerializeField] float currentOxygen;
    [SerializeField] float lastBreath = 6f;

    //Settings
    [Header("SETTINGS")]
    [SerializeField] float movementSpeed = 200;
    [SerializeField] float waterFriction = 25;
    [SerializeField] float waterFrictionDefaultFactor = 1f;
    [SerializeField] float waterFrictionDiveFactor = 0.4f;
    [SerializeField] float timeForWaterPull = 3f;
    [SerializeField] int startingHealth = 6;
    [SerializeField] Material material;
    [SerializeField] float timeToFreeRoam = 1.12f;
    [SerializeField] float freeRoamSpeed = 20f;

    //Tools
    [Header("TOOLS")]
    [SerializeField] GameObject equippedTool;

    //Inventory
    public InventoryManager inventoryManager;
    private bool openInventory = false;
    
    int selectedItem;
    [SerializeField] GameObject torch, harpoonGun;

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

        an = this.gameObject.GetComponent<Animator>();

        sr = this.gameObject.GetComponent<SpriteRenderer>();
        sr.material = new Material(material);

        controller = this.gameObject.GetComponent<Controller2D>();
        
    }
    
    void InitializeParams(){
        currentOxygen = maxOxygen;
    }

    void Start()
    {
        InitializeParams();
        InitializeComponents();

        torch = ToolHelper.InstantiateTool(tools.blowtorch, this.transform);
        torch.SetActive(false);

        harpoonGun = ToolHelper.InstantiateTool(tools.harpoon, this.transform);
        harpoonGun.SetActive(false);

        inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
    }

    void Update()
    {
        // Debug.Log(controller.collisions.below);

        st = new status().Init();

        if(Input.GetKeyDown(KeyCode.Q))
            gameObject.GetComponent<BubbleEmitter>().Emit(10, 50f, new Vector2(0.5f, 0.5f));

        if(hc.isDead){
            st.Dead();
            an.Play("player_death");
        }

        //Movement
        var dir = HandleInput();
        HandleMovement();
        OrganizeInventory();

        //Animation
        if(!hc.isDead)
            Animate();

        //Loop
        currentOxygen -= Time.deltaTime;

        if(currentOxygen <= lastBreath)
            //TODO: implement vision loss

        if(currentOxygen <= 0)
            hc.Kill();

        //Open Inventory
        if (Input.GetKeyDown(KeyCode.E)){
            openInventory = !openInventory;
            if(Time.timeScale == 1.0f){
                Time.timeScale = 0;
                inventoryManager.SetAvailableItems(GameObject.Find("ProximityRadius").GetComponent<ProximityFinder>().nearPickables);
                inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
            }else{
                Time.timeScale = 1.0F;
                inventoryManager.inventoryPanel.gameObject.SetActive(openInventory);
            }   
        }   
    }

    void OrganizeInventory(){
        //direct
        if(Input.GetKeyDown(KeyCode.Alpha1))
            SelectTool(torch);

        if(Input.GetKeyDown(KeyCode.Alpha2))
            SelectTool(harpoonGun);
        
    }

    void SelectTool(GameObject tool){
        if(tool == equippedTool) return;
        else {
            if(equippedTool != null)
                equippedTool.SetActive(false);
            equippedTool = tool;
            equippedTool.SetActive(true);
        } 
    }

    void Animate(){
        var up = Vector2.up;
        var up_angle_left = (Vector2.up + Vector2.left).normalized;
        var up_angle_right = (Vector2.up + Vector2.right).normalized;
        var left = Vector2.left;
        var right = Vector2.right;
        var down_angle_left = (Vector2.down + Vector2.left).normalized;
        var down_angle_right = (Vector2.down + Vector2.right).normalized;
        var down = (Vector2.down);

        var dir = (timeSinceLastSwam > timeForWaterPull) ? inputDirection : direction;
        var mouseToRight = PointHelper.MouseWorldPos().x >= this.transform.position.x;
        var sameDir = (mouseToRight && dir.x > 0) || (!mouseToRight && dir.x <= 0);

        if(dir == up)
            an.Play("player_swim_up");
        if(dir == up_angle_left || dir == up_angle_right){
            if(!sameDir)
                an.Play("player_swim_up_angled_reversed");
            else
                an.Play("player_swim_up_angled");
        }
        if(dir == left || dir == right){
            if(!sameDir)
                an.Play("player_swim_left_reversed");
            else
                an.Play("player_swim_left");
        }
        if(dir == down_angle_left || dir == down_angle_right ){
            if(!sameDir)
                an.Play("player_swim_down_angled_reversed");
            else
                an.Play("player_swim_down_angled");
        }
        if(dir == down)
            an.Play("player_swim_down");

        sr.flipX = dir.x >= 0;

        var defAnSpeed = 1f;
        var slowAnSpeed = 0.45f;
        var anSpeed = timeSinceLastSwam > 2 ? slowAnSpeed:defAnSpeed;

        an.speed = anSpeed;
    }

    #region functions
    Vector2 HandleInput(){
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

        return inputDirection;
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
            
            var factor = Input.GetKey(KeyCode.Space) ? waterFrictionDiveFactor : waterFrictionDefaultFactor;
            var drag = ((waterFriction * factor) * Time.deltaTime) / 2f;

            currentSpeed = Mathf.SmoothStep(currentSpeed, 0, drag);

            movement = direction * currentSpeed;

        if(timeSinceLastSwam > timeForWaterPull && inputDirection == Vector2.zero){
            // var horPull = waterPullHorizontalRange;
            var horPull = 0;
            var verPull = -(waterFriction / 2f);
            var waterPull = new Vector2(Random.Range(-horPull, +horPull), verPull);

            movement += waterPull;
        }

        // rb.velocity = movement;
        controller.Move(movement * Time.deltaTime);

        if(timeSinceLastSwam > timeToFreeRoam)
            controller.Move( (inputDirection * freeRoamSpeed) * Time.deltaTime );
    }
    #endregion
}
