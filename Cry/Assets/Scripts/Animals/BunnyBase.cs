using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyBase : AnimalBase
{
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
        SetAnimation("isWalking", true);
        SetAnimation("isRunning", false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckLife();
    }

    public override void OnDeath()
    {
        //spawn meat
        int randInt = Random.Range(2, 4);
        for (int i = 0; i < randInt;i++)
        {
            Vector3 randDir = new Vector3(Random.Range(0, 1f), Random.Range(0.5f, 1f), Random.Range(0, 1f));
            randDir *= 50; //adds more force
            GameObject newMeat = poolManager.rabbitMeatPickUpObjPool.GetNext();
            newMeat.transform.position = this.transform.position;
            newMeat.GetComponent<Rigidbody>().AddForce(randDir);
            
        }
        //return to objectpool
        worldManager.poolmanager.GetComponent<PoolManager>().rabbitObjPool.ReturnToPool(this.gameObject);
        ResetVariables();
    }


    protected override void OnDamageTaken(float _damage)
    {
        base.OnDamageTaken(_damage);

        Debug.Log("took " + _damage + " damage");
        StartCoroutine(Panicking());

    }

    protected override void ResetVariables()
    {
        base.ResetVariables();
    }


}
