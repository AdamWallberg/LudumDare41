using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
	BoxCollider2D _coll;
	public LayerMask _layerMask;
	public ParticleSystem _particleSystem;
	void Start ()
	{
		_coll = GetComponent<BoxCollider2D>();

		Collider2D[] results = Physics2D.OverlapBoxAll(
			(Vector2)transform.position + _coll.offset,
			_coll.size,
			0.0f,
			_layerMask);

		foreach(Collider2D c in results)
		{
			//Destroy(c.gameObject);
			c.gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
			FindObjectOfType<Player>().OnPlay();
			Instantiate(_particleSystem, c.transform.position, Quaternion.identity);
		}
	}
}
