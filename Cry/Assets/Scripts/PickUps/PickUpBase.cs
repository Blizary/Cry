using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpType
{
    Food,
    Water,
    Obj
}

public class PickUpBase : MonoBehaviour
{
    [Header("Status")]
    public PickUpType pickUpType;
    [Tooltip("Amount restored from consuming this pickup")]
    public float pickUpAmount;

    [HideInInspector]
    public PoolManager poolmanager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Wrong class added to object. Add specific class instead of the base pick up class");
    }

    public void SetVariables()
    {
        poolmanager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().poolmanager.GetComponent<PoolManager>();

    }



    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Call this function when the obj is consumed or deleted
    /// </summary>
    public virtual void OnDeath()
    {
        
    }


}
