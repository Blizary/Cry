using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMeatPickUp : PickUpBase
{
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeath()
    {
        poolmanager.rabbitMeatPickUpObjPool.ReturnToPool(this.gameObject);
    }
}
