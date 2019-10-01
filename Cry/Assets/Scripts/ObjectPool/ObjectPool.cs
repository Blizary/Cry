using UnityEngine;
using System.Collections.Generic;

public class ObjectPooL<T> where T:Pools
{
	T cPool;

	public GameObject curObj;
	public int initialSize;
    public GameObject parentobj;

	public List<GameObject> pool;



	public void Start () 
	{ 
		//curObj = cPool.prefab;
		pool = new List<GameObject> ();

	}





	public GameObject GetNext()
	{
		if (pool.Count >0) 
		{
			GameObject obj = pool [pool.Count - 1];
			obj.SetActive (true);
			pool.RemoveAt (pool.Count - 1);
			return obj;
		}
		return null;
	}

	public void ReturnToPool (GameObject obj)
	{
		pool.Add (obj);
		obj.SetActive (false);
	}


}