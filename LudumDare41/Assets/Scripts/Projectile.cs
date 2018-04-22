using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Vector2 _direction;
	public float _speed = 3.0f;
	public bool _randomRotation = true;

	private void Start()
	{
		if(_randomRotation)
		{
			int rotation = Random.Range(0, 4);
			transform.Rotate(new Vector3(0.0f, 0.0f, rotation * 90.0f));
		}
	}

	void Update ()
	{
		transform.position += (Vector3)_direction * _speed * Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Bounds"))
		{
			Destroy(gameObject);
		}
	}
}
