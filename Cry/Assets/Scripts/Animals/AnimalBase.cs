﻿using System.Collections;
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

    public Vector3 destination;//destination of the animal - defined in code
    public float wanderRadius;//radius around the animal where he can find random locations to move to - define in hierarchy
    public float arrivingProximity;//When this distance is reached it is concidered that the animal has arrived at its destination - define in hierarchy
    public float followRange;//Above this distance the animal should follow otherwise should wait - define in hierarchy
    public GameObject fetchQuery;//object that hold the trigger to detect pick up objs - define in hierarchy
    public GameObject holdLocation;//position where the animal holds pick up objs - define in hierarchy
    public AnimalType animalType; //the type of the animal - define in hierarchy
    public int animalLVL;//the level of the animal - define in hierarchy

    [HideInInspector]
    public Vector3 spawnLocation;//the location where the animal has spawned, home location - defined in code 

    private Animator animator;
    NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        spawnLocation = this.transform.position;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
}
