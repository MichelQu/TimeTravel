using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fracturable : MonoBehaviour
{
	private HashSet<Rigidbody> rbs = new HashSet<Rigidbody>();
	public Vector3 impulseOnFracture = Vector3.zero;
	public UnityEvent onFracture;
	public float timeBeforeDebrisDisappear;

	private void Start()
	{
		foreach(Transform child in transform)
		{
			Rigidbody rb = child.GetComponent<Rigidbody>();
			rb.isKinematic = true;
			rbs.Add(rb);
			
		}
	}

	public void Fracture()
	{
		StartCoroutine("DisappearCoroutine");
		foreach (Rigidbody rb in rbs)
		{
			rb.isKinematic = false;
			rb.AddForce(impulseOnFracture,ForceMode.Impulse);
		}
		onFracture.Invoke();
	}

	private IEnumerator DisappearCoroutine()
	{
		yield return new WaitForSeconds(timeBeforeDebrisDisappear);

		foreach (Rigidbody rb in rbs)
		{
			rb.isKinematic = true;
		}

		while (transform.position.y > 0)
		{
			transform.position -= 0.02f*Vector3.up;
			yield return new WaitForEndOfFrame();
		}

	    Destroy(gameObject);
	}
}
