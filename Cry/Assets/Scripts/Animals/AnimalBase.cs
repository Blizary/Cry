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
    
    public float wanderRadius;//radius around the animal where he can find random locations to move to - define in hierarchy
    public float arrivingProximity;//When this distance is reached it is concidered that the animal has arrived at its destination - define in hierarchy
    public float followRange;//Above this distance the animal should follow otherwise should wait - define in hierarchy
    public FetchQueryStore fetchQuery;//object that hold the trigger to detect pick up objs - define in hierarchy
    public GameObject holdLocation;//position where the animal holds pick up objs - define in hierarchy
    public AnimalType animalType; //the type of the animal - define in hierarchy
    public int animalLVL;//the level of the animal - define in hierarchy
    public int health;//max health of the animal - set in hierarchy


    [HideInInspector]
    public Vector3 destination;//destination of the animal - defined in code
    [HideInInspector]
    public Vector3 spawnLocation;//the location where the animal has spawned, home location - defined in code 
    [HideInInspector]
    public List<Vector3> moveToLocations;//stores the locations where the animal is meant to move to - defined by BT
    [HideInInspector]
    public bool isFollowing;//true is following the player false otherwise - defined by interactions

    private float currentHealth;
    private Animator animator;
    private NavMeshAgent navAgent;
    protected PoolManager poolManager;
    protected WorldManager worldManager;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Wrong class added to object. Add specific class instead of the animal base class");
    }


    public void SetVariables()
    {
        spawnLocation = this.transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        moveToLocations = new List<Vector3>();
        currentHealth = health;
        worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        poolManager = worldManager.poolmanager.GetComponent<PoolManager>();
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
        if(currentHealth<=0)
        {
            OnDeath();
        }
    }




    //virtual voids to override on child classes

    public virtual void OnDeath()
    {

    }

    public virtual void OnDamageTaken(float damage)
    {
        currentHealth -= damage;
    }


}

