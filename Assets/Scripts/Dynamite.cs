using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2 - Ce script gère la classe Dynamite et les méthodes liées à cette classe.

public class Dynamite : MonoBehaviour
{
	public ParticleSystem fuseEffect; // 2 - Variable de l'effet de particule
	public string explodeOnTag; // 2 - Le tag de l'objet sur lequel la dynamite va exploser
    public GameObject explosionEffect;
    private bool fuseOn = false; // 2 - Si l'effect Particule est activé
    private Vector3 spawnLocation; // 2 - Le lieu de l'apparition de la dynamite

    private void Start()
	{
		spawnLocation = transform.position;
	}

	/// <summary>
	/// À appeler quand le joueur saisit l'objet
	/// </summary>

    // 2 - Cette fonction va lancer l'apparition de particule lorsqu'on attrape la dynamite 
	public void Grab()
    {
		fuseEffect.Play();
		fuseOn = true;
	}

    public void OnTriggerEnter(Collider other)
	{
		// 2 - Si collision avec le bon objet
		if (other.gameObject.CompareTag(explodeOnTag))
		{
            // 2 - Ça explose
			Explode();
            // 2 - On récupère la composante Fracturable du parent de l'objet qui collisionne
			Fracturable fracturable = other.GetComponent<Fracturable>();

			if (fracturable != null)
			{
                // 2 - On lance la méthode qui lance la fracture.
				fracturable.Fracture();
			}
		}
	}

	public void Explode()
	{
        // 2 - Fonction crée l'explosion
        Debug.Log("BOOM !");
        // 2 - Crée l'effet d'explosion
        GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        // 2 - On enlève cet effet au bout de 3s
        Destroy(exp, 3);
        // 2 - On respawn la dynamite
		Instantiate(gameObject, spawnLocation, Quaternion.identity);
		Destroy(gameObject);
	}
}
