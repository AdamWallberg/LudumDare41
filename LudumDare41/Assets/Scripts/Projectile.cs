using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Vector2 _direction;
	public float _speed = 3.0f;
	public bool _randomRotation = true;

	public static bool _recentlyPlayedSound = false;
	AudioSource _as;

	private void Start()
	{
		_as = GetComponent<AudioSource>();
		if(!_recentlyPlayedSound)
		{
			_recentlyPlayedSound = true;
			StartCoroutine(CountDown());
			_as.Play();
		}

		if(_randomRotation)
		{
			int rotation = Random.Range(0, 4);
			transform.Rotate(new Vector3(0.0f, 0.0f, rotation * 90.0f));
		}
	}

	IEnumerator CountDown()
	{
		yield return new WaitForSeconds(0.05f);
		_recentlyPlayedSound = false;
		yield return null;
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
