using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyOther : MonoBehaviour
{

    public Light copyMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Light>().color = copyMe.color;
        GetComponent<Light>().intensity = copyMe.intensity;
    }
}
