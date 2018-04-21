using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public int _health = 3;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
			collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
		{
			_health--;
		}
	}
}
