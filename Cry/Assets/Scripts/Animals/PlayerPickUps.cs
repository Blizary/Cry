using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUps : MonoBehaviour
{
    public List<GameObject> food;
    public bool closeToWater;

    private berrybush BerryBushObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Food"))
        {
            if(!food.Contains(other.gameObject))
            {
                food.Add(other.gameObject);
            }
        }
        else if(other.gameObject.CompareTag("Water"))
        {
            closeToWater = true;
        }

        if (other.gameObject.CompareTag("BerryBush"))
        {
            if (!food.Contains(other.gameObject))
            {
                food.Add(other.gameObject);
                BerryBushObject.CollectedBerrys(true);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            if (food.Contains(other.gameObject))
            {
                food.Remove(other.gameObject);
            }
        }
        if (other.gameObject.CompareTag("BerryBush"))
        {
            if (food.Contains(other.gameObject))
            {
                food.Remove(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Water"))
        {
            closeToWater = false;
        }
    }
}

