using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalQuery : MonoBehaviour
{
    public List<GameObject> bunnyList;
    // Start is called before the first frame update
    void Start()
    {
        bunnyList = new List<GameObject>();
        InvokeRepeating("CheckIfActive", 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bunny"))
        {
            if(!bunnyList.Contains(other.gameObject))
            {
                bunnyList.Add(other.gameObject);
            }
            
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bunny"))
        {
            bunnyList.Remove(other.gameObject);
        }
    }


    void CheckIfActive()
    {
        if(bunnyList.Count!=0)
        {
            List<GameObject> removeAtEnd = new List<GameObject>();

            for(int i = 0;i<bunnyList.Count;i++)
            {
                if(!bunnyList[i].activeInHierarchy)
                {
                    removeAtEnd.Add(bunnyList[i]);
                }
            }

            if(removeAtEnd.Count!=0)
            {
                for (int i = 0; i < removeAtEnd.Count; i++)
                {
                    bunnyList.Remove(removeAtEnd[i]);
                }
            }
        }
    }
}
