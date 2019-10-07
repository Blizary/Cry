using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryWheel : MonoBehaviour
{
    public GameObject Wheel;
    public GameObject Cry1;
    public GameObject Cry2;
    public GameObject Cry3;
    public GameObject Cry4;

    // Start is called before the first frame update
    void Start()
    {
        Wheel.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        ActivateCryWheel();
    }

    void ActivateCryWheel()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Wheel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            Wheel.SetActive(false);
        }
    }
}
