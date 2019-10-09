using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AnimalType
{
    Neutral,
    Predator,
    Prey
}



public class AnimalBase : MonoBehaviour
{
    [Header("General Info")]
    public AnimalType animalType; //the type of the animal - define in hierarchy
    public int animalLVL;//the level of the animal - define in hierarchy
    public int health;//max health of the animal - set in hierarchy
    public float invunerabilityDuration; // amount of time after a hit the animal cant take damage again - set in hierarchy
    public float currentHealth;
    public FetchQueryStore fetchQuery;//object that hold the trigger to detect pick up objs - define in hierarchy
    public GameObject holdLocation;//position where the animal holds pick up objs - define in hierarchy


    [Header("Movement/Wander Info")]
    public float wanderRadius;//radius around the animal where he can find random locations to move to - define in hierarchy
    public float arrivingProximity;//When this distance is reached it is concidered that the animal has arrived at its destination - define in hierarchy
    [Range(0, 100)]
    public int waitingPercent;// percentage for the possibility to wait after moving when wandering - set in hierarchy
    public float followRange;//Above this distance the animal should follow otherwise should wait - define in hierarchy

    [Header("Panic settings")]
    public float wanderRadiusPanic;
    public float speedPanic;
    public float panicTimer;





    [HideInInspector]
    public Vector3 destination;//destination of the animal - defined in code
    [HideInInspector]
    public Vector3 spawnLocation;//the location where the animal has spawned, home location - defined in code 
    [HideInInspector]
    public List<Vector3> moveToLocations;//stores the locations where the animal is meant to move to - defined by BT
    [HideInInspector]
    public bool isFollowing;//true is following the player false otherwise - defined by interactions
    [HideInInspector]
    public bool waiting;//true is the animal is just waiting false otherwise - defined by BT


    private Animator animator;
    private NavMeshAgent navAgent;
    protected PoolManager poolManager;
    protected WorldManager worldManager;

    //default values
    private float defaultSpeed;
    private float defaulwanderRadius;
    private int defaultWaitingPercent;



    protected bool canBeHit = true; // true if it can take damage false otherwise - changes on hit works with the invunerability duration
    public List<GameObject> preyAnimals;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Wrong class added to object. Add specific class instead of the animal base class");

    }


    public void SetVariables()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        spawnLocation = this.transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        moveToLocations = new List<Vector3>();
        currentHealth = health;
        worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        poolManager = worldManager.poolmanager.GetComponent<PoolManager>();
        defaultSpeed = GetComponent<NavMeshAgent>().speed;
        defaulwanderRadius = wanderRadius;
        defaultWaitingPercent = waitingPercent;
        preyAnimals = new List<GameObject>();
        InvokeRepeating("CheckCloseByAnimals", 2, 5);

    }

    // Update is called once per frame
    void Update()
    {

    }




    public void SetAnimation(string animatioName, bool stateOfAnimation)
    {
        animator.SetBool(animatioName, stateOfAnimation);
    }


    public void SetDestination(Vector3 newDestination)
    {
        if (newDestination != null)
        {
            Vector3 targetVector = newDestination;
            navAgent.SetDestination(targetVector);
        }
    }

    public void Called()
    {
        isFollowing = !isFollowing;
    }

    public void SetNavmeshMov(bool newbool)
    {
        navAgent.isStopped = newbool;
    }


    public void CheckLife()
    {
        if (currentHealth <= 0)
        {
            OnDeath();
        }
    }




    //virtual voids to override on child classes

    public virtual void OnDeath()
    {

    }

    /// <summary>
    /// called by other objs when they hit this obj
    /// </summary>
    /// <param name="_damage"></param>
    public void TakeDamage(float _damage)
    {
        if (canBeHit)
        {
            StartCoroutine(IEInvunerabilityTimer(invunerabilityDuration, _damage));
        }

    }


    protected virtual void OnDamageTaken(float _damage)
    {
        currentHealth -= _damage;
    }

    IEnumerator IEInvunerabilityTimer(float _invunerabilityDuration, float _damage)
    {
        canBeHit = false;
        OnDamageTaken(_damage);
        yield return new WaitForSeconds(_invunerabilityDuration);
        canBeHit = true;
    }

    protected virtual void ResetVariables()
    {
        currentHealth = health;
        canBeHit = true;
        isFollowing = false;
        GetComponent<NavMeshAgent>().speed = defaultSpeed;
        wanderRadius = defaulwanderRadius;
        waitingPercent = defaultWaitingPercent;
    }


    public void StartPanicking()
    {
        StartCoroutine(Panicking());
    }

    IEnumerator Panicking()
    {
        GetComponent<NavMeshAgent>().speed = speedPanic;
        wanderRadius = wanderRadiusPanic;
        isFollowing = false;
        waitingPercent = 0;
        waiting = false;
        Debug.Log("In panic");
        yield return new WaitForSeconds(panicTimer);
        GetComponent<NavMeshAgent>().speed = defaultSpeed;
        wanderRadius = defaulwanderRadius;
        waitingPercent = defaultWaitingPercent;
        Debug.Log("End panic");
    }

    public void CheckCloseByAnimals()
    {
        //ADD LAYER MASK LATER PLS
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 4);//4 is radius , 10 is the layer mask named "AnimalPersonal"
        for(int i = 0; i<hitColliders.Length;i++)
        {
            if(!preyAnimals.Contains(hitColliders[i].gameObject))
            {
                if(hitColliders[i].gameObject.GetComponent<AnimalBase>() && hitColliders[i].gameObject.GetComponent<AnimalBase>().animalType== AnimalType.Prey)
                {
                    preyAnimals.Add(hitColliders[i].gameObject);
                }
            }
        }
    }
}

