using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{

    public ObjectPooL<RabbitMeatPickUObjPool> rabbitMeatPickUpObjPool;
    public ObjectPooL<RabbitObjPool> rabbitObjPool;

    public ObjectPooL<BerryPickUpObjPool> berryPickUpObjPool;
    public ObjectPooL<BerryobjPool> berryObjPool;



    // Use this for initialization
    void Start()
    {

        rabbitMeatPickUpObjPool = CreatePool<RabbitMeatPickUObjPool>();
        rabbitObjPool = CreatePool<RabbitObjPool>();

        berryPickUpObjPool = CreatePool<BerryPickUpObjPool>();
        berryObjPool = CreatePool<BerryobjPool>();

        //DONT FORGET TO ADD NEW ONES TO REFILL


    }

    ObjectPooL<T> CreatePool<T>() where T : Pools
    {
        ObjectPooL<T> newPool = new ObjectPooL<T>();
        newPool.Start();
        newPool.initialSize = this.GetComponent<T>().initialSize;
        newPool.curObj = this.GetComponent<T>().prefab;
        newPool.parentobj = this.GetComponent<T>().parentObj;
        FillList(newPool.curObj, newPool.pool, newPool.initialSize,newPool.parentobj);
        return newPool;
    }



    // Update is called once per frame
    void Update()
    {

    }





    void Refill()
    {

        FillList(rabbitMeatPickUpObjPool.curObj, rabbitMeatPickUpObjPool.pool, rabbitMeatPickUpObjPool.initialSize,rabbitMeatPickUpObjPool.parentobj);
        FillList(rabbitObjPool.curObj, rabbitObjPool.pool, rabbitObjPool.initialSize, rabbitObjPool.parentobj);

        FillList(berryPickUpObjPool.curObj, berryPickUpObjPool.pool, berryPickUpObjPool.initialSize, berryPickUpObjPool.parentobj);
        FillList(berryObjPool.curObj, berryObjPool.pool, berryObjPool.initialSize, berryObjPool.parentobj);

    }





    public void FillList(GameObject curObj, List<GameObject> pool, int initialSize,GameObject parentobj)
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject objCreate = Instantiate(curObj, parentobj.transform);
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