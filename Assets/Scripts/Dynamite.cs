using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
	public ParticleSystem fuseEffect;
	public string explodeOnTag;
	public GameObject explosionEffect;
	private bool fuseOn = false;
	private Vector3 spawnLocation;

	private void Start()
	{
		spawnLocation = transform.position;
	}

	/// <summary>
	/// A appeler quand le joueur saisit l'objet
	/// </summary>
	public void Grab()
	{
		fuseEffect.Play();
		fuseOn = true;
	}

	public void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag(explodeOnTag))
		{
			Explode();
			Fracturable fracturable = collision.transform.parent.GetComponent<Fracturable>();
			if (fracturable != null)
			{
				fracturable.Fracture();
			}
		}
	}

	public void Explode()
	{
		Debug.Log("BOOM !");
		GameObject exp = Instantiate(explosionEffect, transform.position, Quaternion.identity);
		Destroy(exp, 5);
		Instantiate(gameObject, spawnLocation, Quaternion.identity);
		Destroy(gameObject);
	}
}
