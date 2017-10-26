using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		//var hit = collision.gameObject;
		//var hitPlayer = hit.GetComponent<PlayerMove>();

		var hit = collision.gameObject;
		var hitCombat = hit.GetComponent<Combat>();
		if (hitCombat != null)
		{
			hitCombat.TakeDamage(10);
			Destroy(gameObject);
		}

		/*var target = collision.gameObject;
		var testMovementScript = target.GetComponent<TestMovement>();
		if (testMovementScript != null)
		{
			var combatScript = target.GetComponent<Combat> ();
			combatScript.TakeDamage (10);

			Destroy(gameObject);
		}*/
	}
}
