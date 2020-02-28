using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Cassette : MonoBehaviour
{
    public AudioClip ac;

    public UnityEvent onMusicChange;

    public List<Cassette> autresCassettes;
    public ZoneManager zm;

    //the position of the tiroir to send the cassette
    public tiroir[] tiroirs;


    [SerializeField]
    private bool isSelected;
    
    public AudioSource asource;

    private bool hasBeenSelectedAFirstTime = false;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Walkman" && !isSelected)
        {
            isSelected = true;
            if (hasBeenSelectedAFirstTime)
            {
                for (int i = 0; i < autresCassettes.Count; i++)
                {
                    autresCassettes[i].changeCassette();
                }
            }
            else
            {
                hasBeenSelectedAFirstTime = true;
            }
            asource.clip = ac;
            asource.Play();
            onMusicChange.Invoke();
        }
    }

    public void changeCassette()
    {
        foreach (tiroir t in tiroirs)
        {
            if (t.zoneTiroir == zm.zoneActuelle)
            {
                transform.position = t.transform.position;
            }
        }
    }
}
