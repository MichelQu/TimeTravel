using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class tiroir : MonoBehaviour
{
    public tiroir[] autresTiroirs;
    public ZoneManager zm;
    public Zone zoneTiroir;

    public void registerChildren(Transform tr)
    {
        if (zm.zoneActuelle == zoneTiroir)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                child.transform.position = tr.position;
                child.transform.SetParent(tr.transform);
                
            }
        }
    }
}
