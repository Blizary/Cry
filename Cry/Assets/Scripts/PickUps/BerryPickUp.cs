using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryPickUp : PickUpBase
{
    berrybush berrybush;

    // Start is called before the first frame update
    void Start()
    {
        SetVariables();

        berrybush = GetComponent<berrybush>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDeath()
    {
        //poolmanager.berryPickUpObjPool.ReturnToPool(this.gameObject);
        berrybush.WasCollected = true;
    }
}
