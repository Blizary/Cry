using UnityEngine;
using System.Collections.Generic;

public class ObjectPooL<T> where T:Pools
{
	T cPool;

	public GameObject curObj;
	public int initialSize;

	public List<GameObject> pool;



	public void Start () 
	{ 
		//curObj = cPool.prefab;
		pool = new List<GameObject> ();

	}



/*	public void FillList()
	{
		for(int i=0; i<initialSize; i++)
		{
			GameObject objCreate = Instantiate (curObj,transform);
			pool.Add (objCreate);
			objCreate.SetActive (false);
		}
	}
*/



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









	/*
	public static ObjectPool instance {
		get{
			return _instance;
		}
	}

	private static ObjectPool _instance;

	public GameObject prefab;
	public int initialSize;

	List<GameObject> pool;

	void Awake(){
		if (_instance = null) {
			Destroy (_instance);
		}
		_instance = this; 
	}

	// Use this for initialization
	void Start () {
		pool = new List<GameObject> ();

		for(int i=0; i<initialSize; i++){
			GameObject objCreate = Instantiate (prefab,transform) as GameObject;
			pool.Add (objCreate);
			objCreate.SetActive (false);
		}
	}

	public GameObject GetNext(){
		if (pool.Count >0) {
			GameObject obj = pool [pool.Count - 1];
			obj.SetActive (true);
			pool.RemoveAt (pool.Count - 1);
			return obj;
		}
		return null;
	}

	public void ReturnToPool (GameObject obj){
		pool.Add (obj);
		obj.SetActive (false);
	}

*/
}