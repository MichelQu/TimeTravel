using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - Ce script gère le moment où le joueur joue sur le synthé.

public class PlayingSynthesizer : MonoBehaviour {
    // 2 - Collider des controllers droites et gauches
    public Collider RightHand;
    public Collider LeftHand;

    // 2 - Les Timers et booléen associé
    private float timer = 0.0f;
    private float timerMusic = 0.0f;
    public float TimeLimit = 2.0f;
    private bool TimerIsRunning = false;

    // 2 - Les AudioClips (Clip)
    // TODO : On peut essayer de les récupérer de façon automatique dans le Start
    public AudioClip FirstNote; // 2 - La première note à jouer.
    public AudioClip Melody; // 2 - La mélodie qui sera jouée.

    private bool musicIsPlaying = false; // 2 - Booléen
    public Cassette cassette; // 2 - Une cassette (classe Cassette)
    public AudioSource walkman; // 2 - AudioSource associé au Walkman

    private AudioSource asr; // 2 - Une AudioSource associé à la casette.



    private void Start()
    {
        // 2 - On désactive la cassette (le GameObject)
        cassette.gameObject.SetActive(false);
        // 2 - On récupère l'AudioSource du GameObject associé au script
        asr = GetComponent<AudioSource>();
        // 2 - On remplace son son
        asr.clip = FirstNote;
    }



    void Update () {

        if (TimerIsRunning)
        {
            // 2 - Si le timer est lancé alors on incrémente les timers.
            timer += Time.deltaTime;
            timerMusic += Time.deltaTime;
        }

        if (timer > TimeLimit)
        {
            // 2 - Si les timers dépassent le seuil pré-défini.
            asr.Stop();
            walkman.Play();
            // 2 - On reset les variables
            timerMusic = 0;
            asr.clip = FirstNote;
            TimerIsRunning = false;
            musicIsPlaying = false;
            timer = 0.0f;
        }

        if (asr.clip == Melody && timerMusic > asr.clip.length)
        {
            // Faire poper la cassette
            cassette.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 2 - S'il y a collision entre le synthé et les mains du joueur.
        if(other == RightHand || other == LeftHand)
        {
            // 2 - Si aucune note n'est jouée
            if (!TimerIsRunning && !musicIsPlaying)
            {
                TimerIsRunning = true;
                asr.Play();
            }

            // 2 - Si on joue déjà et que le timer n'est pas terminé.
            else if (TimerIsRunning && timer < TimeLimit)
            {
                // 2 - Si aucune musique n'est jouée
                if (!musicIsPlaying)
                {
                    // 2 - On lance la mélodie
                    asr.clip = Melody;
                    timerMusic = 0;
                    asr.Play();
                    musicIsPlaying = true;
                    walkman.Stop();
                }
                timer = 0.0f; // 2 - On reset les varibles
            }
            
        }  
    }
}
