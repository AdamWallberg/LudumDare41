using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
	public Transform _target;
	public float _enterTime = 2.0f;
	public float _exitSpeed = 4.0f;
	Vector3 _targetPosition;

	public Projectile _projectilePrefab;

	void Start ()
	{
		_targetPosition = _target.position;
		StartCoroutine(Behaviour());
	}

	
	IEnumerator Behaviour()
	{
		// Appear
		Vector3 pos = transform.position;
		for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / _enterTime)
		{

			transform.position = Vector3.Lerp(pos, _targetPosition, t);
			yield return null;
		}
		transform.position = _targetPosition;

		// Fire
		const float startAngle = 120;
		const float endAngle = 240;
		const int numProjectiles = 30;
		for(int i = 0; i < numProjectiles; i++)
		{
			float angle = (i / (float)numProjectiles) * (endAngle - startAngle) + startAngle;
			Vector2 direction = new Vector2(
				Mathf.Cos(Mathf.Deg2Rad * angle),
				Mathf.Sin(Mathf.Deg2Rad * angle));

			Projectile p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
			p._direction = direction;
			yield return new WaitForSeconds(1.0f / (float)numProjectiles);
		}

		// Leave
		for (float t = 0.0f; t < 15.0f; t += Time.deltaTime)
		{
			transform.position += Vector3.left * _exitSpeed * Time.deltaTime;
			yield return null;
		}

			yield return null;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Bounds"))
		{
			Destroy(gameObject);
		}
	}
}
