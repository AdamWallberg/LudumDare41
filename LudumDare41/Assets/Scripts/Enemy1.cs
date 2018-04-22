using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
	public int _behaviour = 0;

	public Transform _target;
	public float _enterTime = 2.0f;
	public float _exitSpeed = 4.0f;
	Vector3 _targetPosition;

	public Projectile _projectilePrefab;
	public Projectile _gunPrefab;

	Animator _animator;

	void Start ()
	{
		_animator = GetComponent<Animator>();
		_targetPosition = _target.position;
		if (_behaviour == 0)
			StartCoroutine(Behaviour());
		else if (_behaviour == 1)
			StartCoroutine(Behaviour2());
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
		const int numProjectiles = 10;
		for(int i = 0; i < numProjectiles; i++)
		{
			float angle = (i / (float)numProjectiles) * (endAngle - startAngle) + startAngle;
			Vector2 direction = new Vector2(
				Mathf.Cos(Mathf.Deg2Rad * angle),
				Mathf.Sin(Mathf.Deg2Rad * angle));

			float r = Random.value;
			Projectile p;
			if (r < 0.1f)
			{
				p = Instantiate(_gunPrefab, transform.position, Quaternion.identity);
			}
			else
			{
				p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
			}
			p._direction = direction;
			yield return new WaitForSeconds(1.0f / (float)numProjectiles);
			if (gameObject.tag == "Dead")
				break;
		}

		// Leave
		for (float t = 0.0f; t < 15.0f; t += Time.deltaTime)
		{
			transform.position += Vector3.left * _exitSpeed * Time.deltaTime;
			yield return null;
		}

			yield return null;
	}

	IEnumerator Behaviour2()
	{
		// Appear
		Vector3 pos = transform.position;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / _enterTime)
		{

			transform.position = Vector3.Lerp(pos, _targetPosition, t);
			yield return null;
		}
		transform.position = _targetPosition;

		// Fire
		for (int i = 0; i < 4; i++)
		{
			yield return new WaitForSeconds(0.4f);
			if (gameObject.tag == "Dead")
				break;

			const float startAngle = 120;
			const float endAngle = 240;
			const int numProjectiles = 6;
			for (int j = 0; j < numProjectiles; j++)
			{
				float angle = (j / (float)numProjectiles) * (endAngle - startAngle) + startAngle;
				Vector2 direction = new Vector2(
					Mathf.Cos(Mathf.Deg2Rad * angle),
					Mathf.Sin(Mathf.Deg2Rad * angle));
				Projectile p = Shoot();
				p._direction = direction;
			}
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

	public void Die()
	{
		_animator.SetBool("Dead", true);
		gameObject.tag = "Dead";
	}

	Projectile Shoot()
	{
		float r = Random.value;
		Projectile p;
		if (r < 0.1f)
		{
			p = Instantiate(_gunPrefab, transform.position, Quaternion.identity);
		}
		else
		{
			p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
		}
		return p;
	}
}
