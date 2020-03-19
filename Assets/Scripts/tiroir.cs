using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - Ce script gère l'utilité du tiroir
// Il suit le joueur à travers le dimension et garde les cassettes qui dans les différents tiroirs.

public class tiroir : MonoBehaviour
{
    // 2 - Liste des autres tiroirs 
    public tiroir[] autresTiroirs;
    // 2 - Variables qui contrôle la zone dans laquelle on est.
    public ZoneManager zm;
    public Zone zoneTiroir;

    public void registerChildren(Transform tr)
    {
        // 2 - Cette fonction permet de créer le déplacement entre les temporalités du tiroir.
        if (zm.zoneActuelle == zoneTiroir)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                // 2 - On prend l'un des gameObject fils du tiroir
                GameObject child = transform.GetChild(i).gameObject;
                // 2 - On positionne le fils sur la position du tiroir
                child.transform.position = tr.position;
                // 2 - On redéfinit child en tant que fils du tiroir
                child.transform.SetParent(tr);          
            }
        }
    }
}
