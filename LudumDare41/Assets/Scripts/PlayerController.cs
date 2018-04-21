using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D _rb;

	public float _movementSpeed = 3.0f;
	public float _accelerationTime = 0.01f;
	Vector2 _velocityVel;

	public float _bobSpeed = 0.5f;
	public float _bobAmount = 0.1f;

	void Start ()
	{
		_rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		// Handle movement
		Vector2 velocity = _rb.velocity;
		Vector2 movement = HandleInput();
		movement *= _movementSpeed;
		velocity = Vector2.SmoothDamp(
			velocity, 
			movement, 
			ref _velocityVel, 
			_accelerationTime, 
			1000.0f, 
			Time.deltaTime);

		velocity += Vector2.up * Mathf.Sin(
			(Mathf.Deg2Rad * Time.time) * 360.0f * _bobSpeed) * _bobAmount;

		_rb.velocity = velocity;
	}

	Vector2 HandleInput()
	{
		Vector2 movement;
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");
		if(movement.magnitude > 1.0f)
			movement.Normalize();
		return movement;
	}
}
