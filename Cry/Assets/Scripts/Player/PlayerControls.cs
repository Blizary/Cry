using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    public float velocity;
    public float jumpPower;
    public float rotationSpeed;
    public GameObject holdingItem;
    public Transform holdingItemLocation;//location where the holding Item will stay
    public bool wireframeVision; //true if on
    public GameObject animalQuery;
    public PlayerPickUps pickUpsFound;

    [Header("Food/Water/Sleep")]
    public GameObject fwsUI;
    public float maxFoodLvl;
    public float maxWaterLvl;
    public float maxSleepLvl;
    public Image foodImage;
    public Image waterImage;
    public Image sleepImage;
    public float foodTrigger;
    public float waterTrigger;
    public float sleepTrigger;
    public float fwsTimer;

    private Transform cam;

    private Rigidbody rb;
    private bool grounded;//true if on the floor , false if jumping or mid air
    private float horizontal;
    private float vertical;
    private Animator animator;
    private bool stopedMov; //true if stopedMoving false is moving
    private float stopedTimer;// slight wait before triggering iddle animation
    private bool isSleeping;


    private float foodLvl;
    private float waterLvl;
    private float sleepLvl;
    private float innerfwsTimer;


    //Pushing and Pulling objects --- Testing
    public bool Holding = false;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        animator =GetComponent<Animator>();
        foodLvl = maxFoodLvl;
        waterLvl = maxWaterLvl;
        sleepLvl = maxSleepLvl;
        innerfwsTimer = fwsTimer;
    }

    // Update is called once per frame
    void Update()
    {
        FWSTick();
        SleepEffects();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
       // Jump();
        TriggerIdle();
        TurnOnWireFrame();
        Sleep();
        DebugUiTrigger();
        Cry();
        EatAndDrink();
        UpdateBars();
        Attack();


    }


    void SleepEffects()
    {
       
        GetComponent<Animal>().runSpeed.animator = Mathf.Lerp(1, 0.3f,1- (sleepLvl / maxSleepLvl));
    }


    void FWSTick()
    {
        if(innerfwsTimer>0)
        {
            innerfwsTimer -= Time.deltaTime;
        }
        else
        {
            innerfwsTimer = fwsTimer;
            foodLvl -= foodTrigger;
            if(foodLvl<0)
            {
                foodLvl = 0;
            }
            waterLvl -= waterTrigger;
            if (waterLvl < 0)
            {
                waterLvl = 0;
            }
            sleepLvl -= sleepTrigger;
            if (sleepLvl < 0)
            {
                sleepLvl = 0;
            }
        }
    }


    void FixedUpdate()
    {
       // Movement();
    }


    void TurnOnWireFrame()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            wireframeVision = !wireframeVision;
            GetComponent<MalbersInput>().canMove = !GetComponent<MalbersInput>().canMove;
        }
    }



    /// <summary>
    /// movement function that uses the horizontal and vertical axis default to unity
    /// </summary>
    void Movement()
    {
        
        if(horizontal==0 && vertical==0)
        {
            //idle
            //
            if(!stopedMov)
            {
                stopedMov = true;
                stopedTimer = 0.1f;//just a few seconds for the animation to have time to trigger
            }
            
        }
        else
        {
            if(!wireframeVision && !isSleeping)
            {
                //moving
                Vector3 dir = (cam.right * horizontal) + (cam.forward * vertical);
                dir.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.fixedDeltaTime * rotationSpeed);
                rb.MovePosition(transform.position + dir * velocity * Time.fixedDeltaTime);
                animator.SetBool("isRunning", true);
                stopedMov = false;
            }
            

        }

    }

    /// <summary>
    /// makes the player jump if they arent already in the air
    /// single jump mode
    /// </summary>
    void Jump()
    {
        if (!wireframeVision && !isSleeping)
        {
            if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
            {
                rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                grounded = false;
                animator.SetBool("isRunning", true);
                stopedMov = false;
            }
        }
        
        
    }

    /// <summary>
    /// triggers the idle animation after a few seconds
    /// used to prevent idle animation from triggering when changing direction
    /// and give a more natural flow
    /// </summary>
    void TriggerIdle()
    {
        if(stopedMov)
        {
            if (stopedTimer > 0)
            {
                stopedTimer -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("isRunning", false);
            }
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animal>().SetAttack(4);
        }
    }



    void EatAndDrink()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if (pickUpsFound.food.Count != 0)
            {
                GetComponent<Animal>().SetAction(2);
                GetComponent<Animal>().Loops = 1;


                foodLvl += pickUpsFound.food[0].GetComponent<PickUpBase>().pickUpAmount;
                if(foodLvl>100)
                {
                    foodLvl = 100;
                }

                pickUpsFound.food[0].GetComponent<PickUpBase>().OnDeath();
            }
            else if (pickUpsFound.closeToWater)
            {
                GetComponent<Animal>().SetAction(7);

                waterLvl += 40;
                if(waterLvl>100)
                {
                    waterLvl = 100;
                }

            }
        }
       
    }


    

    void DebugUiTrigger()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            fwsUI.SetActive(!fwsUI.activeInHierarchy);
        }
    }
    public void Sleep()
    {
        if(isSleeping)
        {
            sleepLvl += Time.deltaTime*5;
            if(sleepLvl>100)
            {
                sleepLvl = 100;
            }
        }

        //Debug.Log("timescale: " + Time.timeScale);
        if(Input.GetKeyDown(KeyCode.E))
        {

            if (Time.timeScale != 1)
            {
                Time.timeScale = 1;
                GetComponent<MalbersInput>().canMove = true;
                isSleeping = false;
                //animator.SetBool("isSleeping", false);

            }
            else
            {
                GetComponent<Animal>().SetAction(6);
                GetComponent<MalbersInput>().canMove = false;
                isSleeping = true;
                //animator.SetBool("isSleeping", true);
                StartCoroutine(WaitSleep(7));
            }
        }
    }
    IEnumerator WaitSleep(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 5;
    }



    void Cry()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(!isSleeping)
            {
                animator.SetBool("isRunning", false);
                //queu sound
                if(animalQuery.GetComponent<AnimalQuery>().bunnyList.Count!=0)
                {
                    for(int i=0;i< animalQuery.GetComponent<AnimalQuery>().bunnyList.Count;i++)
                    {
                        animalQuery.GetComponent<AnimalQuery>().bunnyList[i].GetComponent<AnimalBase>().Called();
                    }
                }
            }
            
        }
    }


    void UpdateBars()
    {
        foodImage.fillAmount = foodLvl / maxFoodLvl;
        waterImage.fillAmount = waterLvl / maxWaterLvl;
        sleepImage.fillAmount = sleepLvl / maxSleepLvl;
    }


    private void OnCollisionStay(Collision collision)
    {
        //checks if the player is on the ground in order to reset the jump
        if(collision.gameObject.CompareTag("Ground"))
        {
              grounded = true;
           
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        //checks if the player is on the ground in order to prevent double jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            animator.SetBool("isRunning", true);
        }
    }




}
