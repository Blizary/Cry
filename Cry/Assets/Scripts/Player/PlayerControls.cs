using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float velocity;
    public float jumpPower;
    public float rotationSpeed;
    public GameObject holdingItem;
    public Transform holdingItemLocation;//location where the holding Item will stay
    public bool wireframeVision; //true if on
    public GameObject animalQuery;


    private Transform cam;

    private Rigidbody rb;
    private bool grounded;//true if on the floor , false if jumping or mid air
    private float horizontal;
    private float vertical;
    private Animator animator;
    private bool stopedMov; //true if stopedMoving false is moving
    private float stopedTimer;// slight wait before triggering iddle animation
    private bool isSleeping;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        animator =GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Jump();
        TriggerIdle();
        TurnOnWireFrame();
        Sleep();
        Cry();
    }

    void FixedUpdate()
    {
        Movement();
    }


    void TurnOnWireFrame()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            wireframeVision = !wireframeVision;
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


    void PickUpObject(GameObject newPickUp)
    {
        //was holding an item already
        if (holdingItem != null)
        {
            //let go of the item
            holdingItem.transform.parent = null;
           

        }

        //reset its parent
        newPickUp.transform.parent = null;
        //set new location
        newPickUp.transform.position = holdingItemLocation.position;
        //set player as parent
        newPickUp.transform.parent = gameObject.transform;
        //pick up an item
        holdingItem = newPickUp;

    }


    public void Sleep()
    {
        //Debug.Log("timescale: " + Time.timeScale);
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(Time.timeScale!=1)
            {
                Time.timeScale = 1;
                animator.SetBool("isSleeping", false);
                
            }
            else
            {
                animator.SetBool("isSleeping", true);
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
