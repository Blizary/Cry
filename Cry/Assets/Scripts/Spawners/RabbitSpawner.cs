using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{

    public float respawnTimer;


    private GameObject activeRabbit;
    private float innerRespawnTimer;
    private WorldManager worldManager;
    // Start is called before the first frame update
    void Start()
    {
        worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();

    }

    // Update is called once per frame
    void Update()
    {
        RespawnRabbit();
    }

    void RespawnRabbit()
    {
        if(activeRabbit == null || !activeRabbit.activeInHierarchy)//obj is not active, this means it was returned to the pool
        {
            //timer
            if(innerRespawnTimer<0)
            {
                //time to respawn
                 GameObject newRabbit = worldManager.poolmanager.GetComponent<PoolManager>().rabbitObjPool.GetNext();
                newRabbit.transform.position = this.transform.position;
                activeRabbit = newRabbit;
                innerRespawnTimer = respawnTimer;
            }
            else
            {
                innerRespawnTimer -= Time.deltaTime;//tick down timer
            }
        }
    }
}
