using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableColl : MonoBehaviour
{
    public Collider solidCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickedUp()
    {
        solidCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }

    public void Dropped()
    {
        solidCollider.enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
