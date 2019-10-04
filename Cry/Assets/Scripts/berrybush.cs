using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berrybush : MonoBehaviour
{
    public bool WasCollected;
    
    public float StartTime;
    public float EndTime;

    int Berrys;
    int TimeIncrement;

    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        CollectedBerrys(WasCollected);
        SpawnBerrys();
    }

    public void CollectedBerrys(bool Collected)
    {
        Collected = WasCollected;

         

        if (Collected == true)
        {
            for (Berrys = 0; Berrys < transform.childCount; Berrys++)
            {
                transform.GetChild(Berrys).gameObject.SetActive(false);
            }
        }
        else if (Collected == false)
        {
            for (Berrys = 0; Berrys < transform.childCount; Berrys++)
            {
                transform.GetChild(Berrys).gameObject.SetActive(true);
            }
        }
       
    }

    public void SpawnBerrys()
    {
        if (WasCollected == true)
        {
            TimeIncrement = 1;

            StartTime += TimeIncrement * Time.deltaTime;
            if (StartTime >= EndTime)
            {
                for (Berrys = 0; Berrys < transform.childCount; Berrys++)
                {
                    transform.GetChild(Berrys).gameObject.SetActive(true);
                    WasCollected = false;
                }

            }
        }
        else if (WasCollected == false)
        {
            TimeIncrement = 0;
            StartTime = 0.0f;

        }
        
    }
}
