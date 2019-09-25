using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentMat : MonoBehaviour
{
    public Material transparentMat;

    private Material originalMat;
    private PlayerControls wireframeHold;
    private bool lastMode;
    // Start is called before the first frame update
    void Start()
    {
        originalMat = GetComponent<Renderer>().material;
        wireframeHold = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

    }

    // Update is called once per frame
    void Update()
    {
        if(lastMode!=wireframeHold.wireframeVision)
        {
            //wireframe active
            if(wireframeHold.wireframeVision)
            {
                GetComponent<Renderer>().material = transparentMat;
            }
            else
            {
                GetComponent<Renderer>().material = originalMat;
            }
            lastMode = wireframeHold.wireframeVision;
        }
    }
}
