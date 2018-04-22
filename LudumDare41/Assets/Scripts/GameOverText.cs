﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverText : MonoBehaviour
{
	TextMeshPro _text;

	string[] messages =
	{
		"This was not the optimal outcome.",
		"This is bad.",
		"We believed in you...",
		"Don't look at me.",
		"Your mother and I are very disappointed.",
		"Look what you did.",
		":(",
	};

	void Start ()
	{
		_text = GetComponent<TextMeshPro>();
		_text.text = messages[Random.Range(0, messages.Length)];
		
	}
}
