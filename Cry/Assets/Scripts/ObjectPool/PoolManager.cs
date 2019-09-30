using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public ObjectPooL<RabbitMeatPickUObjPool> rabbitMeatPickUpObjPool;
    /*
     * 
     * 
     * worldManager.objManager.GetComponent<PoolManager>().arcaneFlareGenerator.ReturnToPool(this.gameObject);
     *       GameObject bullet = worldManager.objManager.GetComponent<PoolManager>().arcaneFlareGenerator.GetNext();
*/


    // Use this for initialization
    void Start()
    {

        rabbitMeatPickUpObjPool = CreatePool<RabbitMeatPickUObjPool>();


        //DONT FORGET TO ADD NEW ONES TO REFILL


    }

    ObjectPooL<T> CreatePool<T>() where T : Pools
    {
        ObjectPooL<T> newPool = new ObjectPooL<T>();
        newPool.Start();
        newPool.initialSize = this.GetComponent<T>().initialSize;
        newPool.curObj = this.GetComponent<T>().prefab;
        FillList(newPool.curObj, newPool.pool, newPool.initialSize);
        return newPool;
    }



    // Update is called once per frame
    void Update()
    {

    }





    void Refill()
    {

        FillList(rabbitMeatPickUpObjPool.curObj, rabbitMeatPickUpObjPool.pool, rabbitMeatPickUpObjPool.initialSize);

    }





    public void FillList(GameObject curObj, List<GameObject> pool, int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject objCreate = Instantiate(curObj, transform);
            pool.Add(objCreate);
            objCreate.SetActive(false);
        }
    }

    public void ResetAllObjects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Refill();

    }



}