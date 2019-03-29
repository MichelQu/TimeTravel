using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Zone
{
    A,
    B,
    C
}

public class ZoneManager : MonoBehaviour
{
    public Zone zoneActuelle = Zone.B;

    //on se place dans le cas ou les plateaux sont disposes dans l ordre A B C selon x
    public Transform posA, posB, posC;
    public GameObject[] obj; //objet a deplacer, par exemple une sphere

    //distance entre les 2 plateaux A et B (positive)
    Vector3 translationAB;
    //distance entre les 2 plateaux A et C (positive)
    Vector3 translationAC;
    //distance entre les 2 plateaux B et C (positive)
    Vector3 translationBC;

    public void Start()
    {
        //distance entre les 2 plateaux A et B (positive)
        translationAB = posB.position - posA.position;
        //distance entre les 2 plateaux A et C (positive)
        translationAC = posC.position - posA.position;
        //distance entre les 2 plateaux B et C (positive)
        translationBC = posC.position - posB.position;
    }

    public void GoToA()
    {
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