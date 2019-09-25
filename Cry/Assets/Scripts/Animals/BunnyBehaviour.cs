using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunnyBehaviour : MonoBehaviour
{

    public Vector3 destination;
    public float wanderRadius;
    public float arrivingProximity;
    public float followRange;
    public GameObject fetchQuery;
    public GameObject holdLocation;

    private Vector3 spawnLocation;


    public int currentState;
    private bool isMoving;
    public bool isFollowing;
    public bool hasFetch;
    public bool isSleeping;
    private bool enteringState;
    private GameObject player;
    private DayNightCycle timeOfTheday;
    private GameObject currentFecth;
    public float idleTimer;

    private Animator animator;
    NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        spawnLocation = transform.position;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        timeOfTheday = GameObject.FindGameObjectWithTag("DayNight").GetComponent<DayNightCycle>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeToSleep();
        CheckIdleTimer();
        CheckFetch();

        Brain();
    }

    void SetDestination()
    {
        if(destination!=null)
        {
            Vector3 targetVector = destination;
            navAgent.SetDestination(targetVector);
            isMoving = true;
        }
    }


    void Brain()
    {
        switch (currentState)
        {
            case 0:
                GoToSPawn();
               
                break;
            case 1:
                Wonder();
                
                break;
            case 2:
                Sleep();
               
                break;
            case 3:
                Follow();
                
                break;
            case 4:
                Fetch();
                
                break;

        }
    }


    //STATE 0 
    void GoToSPawn()
    {
        // On entering state
        if(enteringState)
        {
            destination = spawnLocation;
            animator.SetBool("isRunning", true);
            SetDestination();
            enteringState = false;
            Debug.Log("going to spawn state 0");
        }
        
        //Update


        //EXIT CONDITIONS
        //arrived at its destination
        if(Vector3.Distance(this.transform.position,destination)<=arrivingProximity)
        {
            animator.SetBool("isRunning", false);
            if (isSleeping)
            {
                currentState = 2;//sleep
                Debug.Log("exit go home to sleep");
                enteringState = true;
            }
            else
            {
                currentState = 1;//wonder
                Debug.Log("exit go home to wander");
                enteringState = true;
            }
            
        }
       
    }

    //STATE 1 
    void Wonder()
    {
        // On entering state
        if (enteringState)
        {

            Vector3 randomPoint = transform.position + Random.insideUnitSphere * wanderRadius;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                destination = hit.position;
                SetDestination();
                enteringState = false;
                animator.SetBool("isRunning", true);
            }
            Debug.Log("wonder state 1");
        }
        else
        {
            //Update


            //EXIT CONDITIONS

            int randNum = Random.Range(0, 100);
            //arrived at its destination
            if (Vector3.Distance(this.transform.position, destination) <= arrivingProximity)
            {
                if (randNum > 60)
                {
                    currentState = 1;//wonder
                    enteringState = true;
                }
                else
                {
                    idleTimer = Random.Range(10, 30);
                    currentState = 2;//idle
                    enteringState = true;
                }

            }
        }

        
    
        


        
    }

    //STATE 2 
    //used also for idle
    void Sleep()
    {
        // On entering state
        if (enteringState)
        {
            navAgent.isStopped = true;
            animator.SetBool("isRunning", false);
            enteringState = false;
            Debug.Log("sleep/idle state 2");
        }

        //Update


        //EXIT CONDITIONS

        
        

        //is following and player moved away
        if (Vector3.Distance(this.transform.position, destination) >= followRange && isFollowing)
        {
            navAgent.isStopped = false;
            currentState = 3;//go back to follow
            Debug.Log("exit to follow when it was idle but following");
            enteringState = true;
        }
        else
        {
            //time to wake up
            if (timeOfTheday.timeOfDay > 0.3f && timeOfTheday.timeOfDay < 0.65f && isSleeping)
            {
                navAgent.isStopped = false;
                currentState = 1;//wander
                isSleeping = false;
                enteringState = true;
            }
            else if(!isSleeping)
            {
                //done waiting in idle
                if(idleTimer<0)
                {
                    navAgent.isStopped = false;
                    currentState = 1;//wander
                    enteringState = true;
                }
            }
        }
        

    }

    //STATE 3 
    void Follow()
    {
        // On entering state
        if (enteringState)
        {
            destination = player.transform.position;
            SetDestination();
            animator.SetBool("isRunning", true);
            isFollowing = true;
            enteringState = false;
            Debug.Log("following state 3");
        }

        //Update
        //player is moving calculate again the path
        destination = player.transform.position;
        SetDestination();

        

        //EXIT CONDITIONS
        //arrived at its destination
        if (Vector3.Distance(this.transform.position, destination) <= arrivingProximity)
        {
            if(hasFetch)
            {
                //drop item 

                currentFecth.transform.position = holdLocation.transform.position;
                currentFecth.gameObject.transform.parent = null;
                currentFecth.GetComponent<DisableColl>().Dropped();

                currentState = 0;//go home
                enteringState = true;
                isFollowing = false;
                hasFetch = false;
                Debug.Log("done with fetch going home");
            }
            else
            {
                Debug.Log("going to idle");
                currentState = 2;//idle
                enteringState = true;
            }
            
        }

    }

    //STATE 4 
    void Fetch()
    {
        // On entering state
        if (enteringState)
        {
            destination = fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0].transform.position;
            animator.SetBool("isRunning", true);
            SetDestination();
            enteringState = false;
            Debug.Log("going to fetch state 4");
        }

        //Update


        //EXIT CONDITIONS
        //arrived at its destination
        if (Vector3.Distance(this.transform.position, destination) <= arrivingProximity)
        {
            hasFetch = true;

            currentFecth = fetchQuery.GetComponent<FetchQueryStore>().fetchObjs[0];
            currentFecth.transform.position = holdLocation.transform.position;
            currentFecth.gameObject.transform.parent = this.transform;
            currentFecth.GetComponent<DisableColl>().PickedUp();
          
            Debug.Log("exit fetch going to player");
            currentState = 3;//back to player
            enteringState = true;
        }

    }



    void CheckTimeToSleep()
    {
        if(!isSleeping)
        {
            //sleeping time
            if (!isFollowing)
            {
                if (timeOfTheday.timeOfDay <= 0.3f || timeOfTheday.timeOfDay >= 0.65f)
                {
                    navAgent.isStopped = false;
                    currentState = 0;//go home to sleep
                    isSleeping = true;
                    enteringState = true;

                }
            }
        }
        
        
    }

    void CheckIdleTimer()
    {
        if(currentState==2)
        {
            if (idleTimer > 0)
            {
                idleTimer -= Time.deltaTime;
            }
        }
         

    }

    void CheckFetch()
    {
        if(isFollowing && !hasFetch)
        {
            if (fetchQuery.GetComponent<FetchQueryStore>().fetchObjs.Count != 0)
            {
                navAgent.isStopped = false;
                hasFetch = true;
                currentState = 4;//go fetch
                enteringState = true;
            }
        }
        
    }
  

    /// <summary>
    /// called by the player controls
    /// </summary>
    public void Called()
    {
        if(!isFollowing)
        {
            navAgent.isStopped = false;
            currentState = 3; //follow player
            enteringState = true;
        }
        else
        {
            navAgent.isStopped = true;
            currentState = 2; //stop follwing player
            idleTimer = Random.Range(10, 30);
            isFollowing = false;
            if(hasFetch)
            {
                //drop item 
                currentFecth.transform.position = holdLocation.transform.position;
                currentFecth.gameObject.transform.parent = null;
                currentFecth.GetComponent<DisableColl>().Dropped();

            }
            enteringState = true;
        }
        
    }

    

}
