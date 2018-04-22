using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
	ParticleSystem _ps;

	void Start ()
	{
		_ps = GetComponent<ParticleSystem>();
		float totalDuration = _ps.main.duration + _ps.main.startLifetime.constant;
		Destroy(gameObject, totalDuration);
	}
}
