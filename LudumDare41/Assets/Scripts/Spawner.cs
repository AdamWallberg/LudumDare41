using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
	public float _spawnTime = 2.0f;
	float _timer = 0.0f;

	public Transform[] _prefabs;
	public Transform _top;
	public Transform _bottom;

	string[] _rows =
	{
		" , ,0, , ",
		" , , , , ",
		" , ,0, , ",
		" , , , , ",
		" ,0, ,0, ",
		" , ,1, , ",
		" , , , , ",
		" ,0, , , ",
		" , , ,0, ",
		" , ,0,0, ",
		" , , ,1, ",
		" , , , , ",
		"0, , , , ",
		" ,0, , , ",
		" , ,0, , ",
		" , , ,0, ",
		"0, ,0, ,0",
		" , ,1, , ",
		" ,1, ,1, ",
		" , , , , ",
		"0,0,0,0,0",
		" , , , , ",
	};

	void Start ()
	{
		StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn()
	{
		for(int i = 0; i < _rows.Length; i++)
		{
			yield return new WaitForSeconds(_spawnTime);
			
			for(int j = 0; j <= 8; j += 2)
			{
				char c = _rows[i][j];
				if (c == ' ')
					continue;
				int id = (int)char.GetNumericValue(c);
				// Pos
				float t = j / (float)8;
				Vector3 pos = Vector3.Lerp(_bottom.position, _top.position, t);

				Instantiate(_prefabs[id], pos, Quaternion.identity);
			}
		}

		yield return new WaitForSeconds(7.0f);

		SceneManager.LoadScene("WinScreen");

		yield return null;
	}
}
