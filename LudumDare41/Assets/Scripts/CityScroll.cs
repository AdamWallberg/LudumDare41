using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScroll : MonoBehaviour
{
	public Transform[] _cities;

	public float _scrollSpeed = 2.0f;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		Vector3 movement = Vector3.left * _scrollSpeed * Time.deltaTime;
		foreach(Transform city in _cities)
		{
			city.position += movement;
			if (city.position.x < -20.0f)
				city.position += Vector3.right * 40.0f;
		}

		
	}
}
