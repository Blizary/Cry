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
}
