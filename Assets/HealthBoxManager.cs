using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(".") && transform.childCount > 0)
        {
            transform.GetChild(transform.childCount-1).GetComponent<HealthBox>().blowUp();
        }
    }
}
