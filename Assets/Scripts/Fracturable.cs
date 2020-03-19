using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 2 - Ce script gère la classe Fracturable.
// Cette classe fracturable gère la fracture du GameObject

public class Fracturable : MonoBehaviour
{
	private HashSet<Rigidbody> rbs = new HashSet<Rigidbody>();  // 2 - Liste des RigidBody
    public Vector3 impulseOnFracture = Vector3.zero;
	public UnityEvent onFracture; // Évènement de fracture
	public float timeBeforeDebrisDisappear; // Temps de fracture

	private void Start()
	{
        // 2 - Pour tous les fils du GameObject collisionné,
        // On récupère leur RigidBody, on active les effets physiques sur le RigidBody
        // Et on ajoute tous ces RigidBody à la liste 'rbs'.
        foreach (Transform child in transform)
		{
            Rigidbody rb = child.GetComponent<Rigidbody>();
			rb.isKinematic = true;
			rbs.Add(rb);		
		}
	}

    // 2 - Fonction qui lance la fracture de l'objet.
	public void Fracture()
	{
        // 2 - On lance la coroutine.
		StartCoroutine("DisappearCoroutine");

        foreach (Rigidbody rb in rbs)
		{
            // 2 - Pour tous les RigidBody, on désactive les effets physiques.
            rb.isKinematic = false;
            // 2 - On définit la force aux rigidbody et on ajoute une impulsion de force instantanée au corps rigide, en utilisant sa masse.
            rb.AddForce(impulseOnFracture,ForceMode.Impulse);
		}
        // 2 - Déclenche l'évènement attaché sur le script et sur le code.
        onFracture.Invoke();
	}

	private IEnumerator DisappearCoroutine()
	{
        // 2 - On attend autant de temps qu'on l'a indiqué en variable 
		yield return new WaitForSeconds(timeBeforeDebrisDisappear);

        // 2 - On active les effets physiques sur tous les rigidbody dans 'rbs'
        foreach (Rigidbody rb in rbs)
		{
			rb.isKinematic = true;
		}

        // 2 - On fait tomber le GameObject.
        while (transform.position.y > 0)
		{
            // 2 - Tant qu'il ait en l'air, on le fait descendre
            transform.position -= 0.02f*Vector3.up;
            // 2 - On attend que la frame est rendu tous les GUI et Texture, juste avant que la frame soit affiché.
            yield return new WaitForEndOfFrame();
		}

        // On détruit le GameObject associé au Script.
	    Destroy(gameObject);
	}
}
