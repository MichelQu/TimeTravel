using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - Ce script permet d'attribuer un entier au différent élément dans cette liste.
// Temporalité Western = A --> 0
// Temporalité Année 1980 = B --> 1
// Temporalité Année 2050 = C --> 2
public enum Zone { A, B, C }

// 2 - Ce script sert à gérer les différentes temporalités et les transitions entre les différentes temporalités.
public class ZoneManager : MonoBehaviour
{
    public Zone zoneActuelle = Zone.B; // 2 - La temporalité des années 80, la temporalité de base

    //on se place dans le cas ou les plateaux sont disposés dans l ordre A B C selon x
    public Transform posA, posB, posC;

    // 2 - Listes des objets qui se déplaceront lors des changements de temporalité.
    // TODO :
    // On pourrait utiliser un tag pour identifier tous les objets pour automatiser le code
    // qui se déplaceront ou en instanciant les objets en tant que fils d'un autre gameObject.
    public GameObject[] obj;

    // Distance entre les 2 plateaux A et B (positive)
    Vector3 translationAB;
    // Distance entre les 2 plateaux A et C (positive)
    Vector3 translationAC;
    // Distance entre les 2 plateaux B et C (positive)
    Vector3 translationBC;

    public void Start()
    {
        // On calcule certaines distances de base.
        // Distance entre les 2 plateaux A et B (positive)
        translationAB = posB.position - posA.position;
        // Distance entre les 2 plateaux A et C (positive)
        translationAC = posC.position - posA.position;
        // Distance entre les 2 plateaux B et C (positive)
        translationBC = posC.position - posB.position;
    }

    public void GoToA()
    {
        // 2 - On se déplace dans la zone A selon les différentes zones 
        foreach (GameObject objet in obj)
        {
            if (zoneActuelle == Zone.B)
            {
                objet.transform.position -= translationAB;
            }
            else if (zoneActuelle == Zone.C)
            {
                objet.transform.position -= translationAC;
            }
        }
        zoneActuelle = Zone.A;
        Debug.Log("Going to A !");
    }

    public void GoToB()
    {
        // 2 - On se déplace dans la zone B selon les différentes zones 
        foreach (GameObject objet in obj)
        {
            if (zoneActuelle == Zone.A)
            {
                objet.transform.position += translationAB;
                Vector3 hauteur = new Vector3(0, 0.2f, 0);
                objet.transform.position += hauteur;
            }
            else if (zoneActuelle == Zone.C)
            {
                objet.transform.position -= translationBC;
                Vector3 hauteur = new Vector3(0, 0.2f, 0);
                objet.transform.position += hauteur;
            }
        }
        zoneActuelle = Zone.B;
        Debug.Log("Going to B !");
    }

    public void GoToC()
    {
        // 2 - On se déplace dans la zone C selon les différentes zones 
        foreach (GameObject objet in obj)
        {
            if (zoneActuelle == Zone.A)
            {
                objet.transform.position += translationAC;
            }
            else if (zoneActuelle == Zone.B)
            {
                objet.transform.position += translationBC;
            }
        }
        zoneActuelle = Zone.C;
        Debug.Log("Going to C !");
    }

}