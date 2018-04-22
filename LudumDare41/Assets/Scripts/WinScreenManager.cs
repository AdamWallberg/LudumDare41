using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour {

	float _timer = 0.0f;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		_timer += Time.deltaTime;
		if(Input.GetButtonDown("Submit") && _timer >= 1.0f)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
