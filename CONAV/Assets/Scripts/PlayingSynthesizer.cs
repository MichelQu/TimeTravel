using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSynthesizer : MonoBehaviour {
    public Collider RightHand;
    public Collider LeftHand;
    private float timer = 0.0f;
    private float timerMusic = 0.0f;
    public float TimeLimit = 2.0f;
    private bool TimerIsRunning = false;
    public AudioClip FirstNote;
    public AudioClip Melody;
    private bool musicIsPlaying = false;
    public Cassette cassette;
    public AudioSource walkman;

    private AudioSource asr;

    private void Start()
    {
        cassette.gameObject.SetActive(false);
        asr = GetComponent<AudioSource>();
        asr.clip = FirstNote;
    }

    // Update is called once per frame
    void Update () {
        if (TimerIsRunning)
        {
            timer += Time.deltaTime;
            timerMusic += Time.deltaTime;
        }
        if (timer > TimeLimit)
        {
            asr.Stop();
            walkman.Play();
            timerMusic = 0;
            asr.clip = FirstNote;
            TimerIsRunning = false;
            musicIsPlaying = false;
            timer = 0.0f;
        }
        if (asr.clip==Melody && timerMusic > asr.clip.length)
        {
            //Faire poper la cassette
            cassette.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == RightHand || other == LeftHand)
        {
            if (!TimerIsRunning && !musicIsPlaying)
            {
                TimerIsRunning = true;
                asr.Play();
            }

            else if (TimerIsRunning && timer < TimeLimit)
            {
                if (!musicIsPlaying)
                {
                    asr.clip = Melody;
                    timerMusic = 0;
                    asr.Play();
                    musicIsPlaying = true;
                    walkman.Stop();
                }
                timer = 0.0f;
            }
            
        }
       
    }
}
