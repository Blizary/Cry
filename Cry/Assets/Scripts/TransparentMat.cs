using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentMat : MonoBehaviour
{
    public Material transparentMat;

    public List<Material> originalMat;
    public List<Material> transparentMatList;
    private PlayerControls wireframeHold;
    private bool lastMode;
    private int materialsNum;
    // Start is called before the first frame update
    void Start()
    {
        originalMat = new List<Material>();
        materialsNum = GetComponent<Renderer>().materials.Length;

        for (int i = 0; i < materialsNum; i++)
        {
            originalMat.Add(GetComponent<Renderer>().materials[i]);
            transparentMatList.Add(transparentMat);
        }
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
                GetComponent<Renderer>().materials = transparentMatList.ToArray();

            }
            else
            {
                GetComponent<Renderer>().materials= originalMat.ToArray();

            }
            lastMode = wireframeHold.wireframeVision;
        }
    }
}
