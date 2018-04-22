using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
	void Update ()
	{
		if(Input.GetButtonDown("Submit"))
		{
			SceneManager.LoadScene("Main");
		}
		else if(Input.GetButtonDown("Cancel"))
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
