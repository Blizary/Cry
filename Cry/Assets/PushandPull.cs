using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushandPull : MonoBehaviour
{
    private GameObject player;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (transform.parent != null)        
            {
                rb.isKinematic = true;
                transform.SetParent(null);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        { 
            if (other.CompareTag("Player"));
            {
                rb.isKinematic = false;
                transform.SetParent(player.transform);
            }
        }

    }
}
