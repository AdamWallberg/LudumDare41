﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	void Start ()
	{
		Projectile._recentlyPlayedSound = false;
	}
	
	void Update ()
	{
		if(Input.GetButtonDown("Cancel"))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
