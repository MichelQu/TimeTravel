using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 2 - Ce script crée la classe Cassette qui regroupe toutes les informations nécessaires de la cassette.

public class Cassette : MonoBehaviour
{
    // 2 - L'audioClip
    public AudioClip ac;

    // 2 - Création d'un Event
    public UnityEvent onMusicChange;

    // 2 - Liste des autres cassettes
    public List<Cassette> autresCassettes;

    // 2 - Les informations sur la zone de jeu
    public ZoneManager zm;

    // the position of the tiroir to send the cassette
    public tiroir[] tiroirs;

    // 2 - Sauvegarde du booléen qui donne l'information sur la sélection de la cassette
    [SerializeField] private bool isSelected;

    // 2 - L'audioSource de la musique
    public AudioSource asource;

    // 2 - Cette variable servira à savoir si c'est la première fois qu'on joue la cassette
    private bool hasBeenSelectedAFirstTime = false;


    private void OnCollisionEnter(Collision collision)
    {
        // Si la cassette collisionne le Walkman et si la casette n'est pas sélectionné
        if (collision.gameObject.tag == "Walkman" && !isSelected)
        {
            isSelected = true;
            // 2 - On teste si c'est la premier fois que la cassette est sélectionné
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
            // 2 - On attribue l'audioclip à l'audioSource, on joue le son
            asource.clip = ac;
            asource.Play();
            // 2 - Déclenche l'évènement attaché sur la cassette et le code.
            onMusicChange.Invoke();
        }
    }

    public void changeCassette()
    {
        // 2 - Si la zone du tiroir correspond à la zone actuelle alors on téléporte le joueur vers le tiroir
        foreach (tiroir t in tiroirs)
        {
            if (t.zoneTiroir == zm.zoneActuelle)
            {
                transform.position = t.transform.position;
            }
        }
    }
}
