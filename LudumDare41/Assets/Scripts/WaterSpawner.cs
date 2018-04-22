using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{

	public Projectile _waterPrefab;

	void Start ()
	{
		StartCoroutine(Sweep());
	}
	
	IEnumerator Sweep()
	{
		// Fire
		const int numProjectiles = 10;
		for (int i = 0; i < numProjectiles; i++)
		{
			float angle = 180.0f + Mathf.Sin(Mathf.Deg2Rad * (i / (float)numProjectiles) * 360.0f) * 10.0f;
			Vector2 direction = new Vector2(
				Mathf.Cos(Mathf.Deg2Rad * angle),
				Mathf.Sin(Mathf.Deg2Rad * angle));

			Projectile p = Instantiate(_waterPrefab, transform.position, Quaternion.identity);
			
			p._direction = direction * 2.0f;
			yield return new WaitForSeconds(0.2f);
		}

		yield return null;
	}
}
