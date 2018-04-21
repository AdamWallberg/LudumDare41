using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public float _spawnTime = 5.0f;
	float _timer = 0.0f;

	public Transform _prefab;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		_timer -= Time.deltaTime;
		if(_timer <= 0.0f)
		{
			_timer = _spawnTime;
			Instantiate(_prefab, transform.position, Quaternion.identity);
		}
	}
}
