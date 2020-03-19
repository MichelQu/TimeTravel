using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabou : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("logging start");
    }

    void OnTriggerEnter()
    {
        Debug.Log("hello trigger");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
