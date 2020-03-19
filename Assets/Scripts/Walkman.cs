using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - Ce script est à placer sur le Walkman, il sert à gérer les interactions du walkman avec
// les cassettes qui seront mises dedans grâce au Controller
// Dès que la cassette est mise dans le Walkman, on joue la musique qui est associé à la cassette
// Si la musique qui est jouée est la bonne il y a un changement de temporalité.

public class Walkman : MonoBehaviour
{
    private ZoneManager zoneManager; // 2 - Variable qui va gérer les déplacements entre les zones
    public Transform tapeIn;   // 2 - Position de la casette 

    public GameObject[] tapes; // 2 - Liste des casettes (Pourquoi GameObject et non pas une classe Cassette ?)
    public GameObject objetAFaireApparaitre; // 2 - C'est quoi l'objet à faire apparaitre ? 

    private Zone zoneActuelle;  // 2 - Numéro de la zone où l'on est (c'est un entier compris entre 0 et 2)

    private AudioSource asource; // 2 - AudioSource attaché à objetAFaireApparaitre
    private bool hasAppeared = false;

    private GameObject currentTape; // 2 - Cassette actuelle
    private GameObject recentlyPlayedTape = null; // 2 - Cassette qu'on vient de jouer

    void Start()
    {
        asource = objetAFaireApparaitre.GetComponent<AudioSource>(); // 2 - on récupère l'audioSource lié au gameObject.
        objetAFaireApparaitre.SetActive(false); // 2 - On désative les propriétés du gameObject.
        zoneManager = GetComponent<ZoneManager>();  // 2 - On récupère les infos de la classe.
        zoneActuelle = zoneManager.zoneActuelle; // 2 - On récupère le numéro de la zone.
    }

    void OnCollisionEnter(Collision collision)
    {
        // 2 - Si il n'y a pas de cassette dans le walkman, alors on met celle avec laquelle on est en collision.
        if (currentTape == null && collision.gameObject != recentlyPlayedTape)
        {
            foreach (GameObject tape in tapes)
            {
                if (collision.gameObject.name == tape.name)
                {
                    // 2 - On définit la nouvelle cassette et on récupère certaines informations.
                    currentTape = collision.gameObject;
                    currentTape.GetComponent<Rigidbody>().isKinematic = true;
                    currentTape.transform.parent = tapeIn;
                    currentTape.transform.localPosition = Vector3.zero;
                    currentTape.transform.localRotation = Quaternion.identity;
                    // 2 - On lance la fonction (Coroutine) lié à l'action de la cassette dans le Walkman
                    StartCoroutine("PlaySongCoroutine");
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 2 - On réinitialise certaines informations lorsqu'on quitte la collision.
        if (collision.gameObject == recentlyPlayedTape)
        {
            recentlyPlayedTape = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 2 - Quand on collisionne avec le controller.
        if (other.gameObject.tag == "Controller" && !hasAppeared)
        {
            hasAppeared = true;
            objetAFaireApparaitre.SetActive(true);
        }
    }

    IEnumerator PlaySongCoroutine()
    {
        Debug.Log("Song started !");
        switch (currentTape.name) // 2 - Cassette Actuelle  
        {
            // 2 - TODO :
            // En fonction du nom de la cassette, on joue la musique associé.
            // Le modèle est en dessous, il reste à le faire pour toutes les casettes.

            case "K7Beatles":
                // 2 - On charge la musique voulue
                Object obj = Resources.Load("Queen TheMiracle", typeof(AudioSource));
                AudioClip musique = (AudioClip)obj;
                // 2 - On le place sur le walkman
                // ObjetAFaireApparaitre ?? Peut etre le Walkman d'où :
                // Sinon il faut extraire l'AudioSource du Walkman
                asource.clip = musique;
                break;
        }

        yield return new WaitForSeconds(5); //On joue la musique 5 secondes
        // On arrête la musique
        asource.Stop();
        Debug.Log("Song ended !");

        // TODO : Si on joue la bonne cassette, on se TP d'une zone à l'autre.
        //    switch (currentTape.name)
        //    {
        //         case :
        //          // zoneManager.GoTo(); // Cette ligne de commande, il reste à savoir où lol
        //          break;
        //    }

        // On éjecte la cassette du walkman
        currentTape.transform.parent = null;
        currentTape.GetComponent<Rigidbody>().isKinematic = false; 
        recentlyPlayedTape = currentTape;
        currentTape = null;
    }
}
