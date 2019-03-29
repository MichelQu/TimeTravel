using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkman : MonoBehaviour {

    private ZoneManager zoneManager;
    public Transform tapeIn;

    public GameObject[] tapes;
    public GameObject objetAFaireApparaitre;

    private Zone zoneActuelle;


    private AudioSource asource;
    private bool hasAppeared = false;

    private GameObject currentTape;
    private GameObject recentlyPlayedTape = null;

	void Start ()
    {
        asource = objetAFaireApparaitre.GetComponent<AudioSource>();
        objetAFaireApparaitre.SetActive(false);
        zoneManager = GetComponent<ZoneManager>();
        zoneActuelle = zoneManager.zoneActuelle;
	}

    private void Update()
    {
    }

    void OnCollisionEnter (Collision collision) {
        

        //Si il n'y a pas de cassette dans le walkman, alors on met celle avec laquelle on est en collision
        if (currentTape == null && collision.gameObject != recentlyPlayedTape)
        {
            foreach (GameObject tape in tapes)
            {
                if (collision.gameObject.name == tape.name)
                {
                    currentTape = collision.gameObject;
                    currentTape.GetComponent<Rigidbody>().isKinematic = true;

                    currentTape.transform.parent = tapeIn;
                    currentTape.transform.localPosition = Vector3.zero;
                    currentTape.transform.localRotation = Quaternion.identity;
                    

 //                   StartCoroutine("PlaySongCoroutine");
                }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Controller" && !hasAppeared) 
        {
            hasAppeared = true;
            objetAFaireApparaitre.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject == recentlyPlayedTape)
        {
            recentlyPlayedTape = null;
        }
    }

 /*   IEnumerator PlaySongCoroutine()
    {
        Debug.Log("Song started !");
        switch (currentTape.name)
        {
            case "K7Beatles":
                //TODO
                //Jouer les Beatles, ect...
                break;

        }

        yield return new WaitForSeconds(5); //On joue la musique 5 secondes
        //TODO : Arrêter la musique
        Debug.Log("Song ended !");
        //TODO : Si on joue la bonne cassette, on se TP
        switch (currentTape.name)
        {
            
        }
        //On éjecte la cassette
        currentTape.transform.parent = null;
        currentTape.GetComponent<Rigidbody>().isKinematic = false; 
        recentlyPlayedTape = currentTape;
        currentTape = null;
    } */
}
