using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Transform[] _buttons;
	public Transform _selector;
	int _selection = 0;
	bool _pressed = false;

	public AudioSource _moveSound;
	public AudioSource _selectSound;

	void Start ()
	{
		
	}
	
	void Update ()
	{
		int dir = (int)Input.GetAxisRaw("Vertical");
		if(dir != 0)
		{
			if(!_pressed)
			{
				_pressed = true;
				_selection -= dir;
				if (_selection < 0)
					_selection = 0;
				else if (_selection > _buttons.Length - 1)
					_selection = _buttons.Length - 1;

				_selector.position = _buttons[_selection].position - Vector3.right * 0.5f;
				_moveSound.Play();
			}
		}
		else
			_pressed = false;

		if(Input.GetButtonDown("Submit"))
		{
			_selectSound.Play();
			if(_selection == 0)
			{
				SceneManager.LoadScene("Main");
			}
			else if(_selection == 1)
			{
				Application.Quit();
			}
		}
	}
}
