using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	public SpriteRenderer[] _renderers;
	PlayerController _controller;

	public float _hitDamage = 0.1f;
	public float _knockbackForce = 100.0f;
	public float _decreaseSpeed = 30.0f;
	public float _invincibilityTime = 2.0f;
	float _invincibilityTimer = 0.0f;

	float _food = 1.0f;
	float _play = 1.0f;
	float _clean = 1.0f;

	public Image _foodImage;
	public Image _playImage;
	public Image _cleanImage;

	public ParticleSystem _hitParticleSystem;

	void Start ()
	{
		_renderers = GetComponentsInChildren<SpriteRenderer>();
		_controller = GetComponent<PlayerController>();
	}
	
	void Update ()
	{
		_invincibilityTimer -= Time.deltaTime;

		_food -= Time.deltaTime * (1.0f / _decreaseSpeed);
		_play -= Time.deltaTime * (1.0f / _decreaseSpeed);
		_clean -= Time.deltaTime * (1.0f / _decreaseSpeed);

		_food = Mathf.Clamp01(_food);
		_play = Mathf.Clamp01(_play);
		_clean = Mathf.Clamp01(_clean);
	}

	private void OnGUI()
	{
		_foodImage.fillAmount = _food;
		_playImage.fillAmount = _play;
		_cleanImage.fillAmount = _clean;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_invincibilityTimer > 0.0f)
			return;
		if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
			collision.gameObject.layer == LayerMask.NameToLayer("EnemyProjectile"))
		{
			_food -= _hitDamage;
			_play -= _hitDamage;
			_clean -= _hitDamage;
			_invincibilityTimer = _invincibilityTime;
			_controller._rb.AddForce(Vector2.left * _knockbackForce);
			Instantiate(_hitParticleSystem, transform.position, Quaternion.identity);
			StartCoroutine(Blink());
		}
	}

	IEnumerator Blink()
	{
		while(_invincibilityTimer > 0.0f)
		{
			foreach(SpriteRenderer renderer in _renderers)
			{
				renderer.enabled = !renderer.enabled;
			}
			yield return new WaitForSeconds(0.1f);
		}
		foreach (SpriteRenderer renderer in _renderers)
		{
			renderer.enabled = true;
		}
		yield return null;
	}
}
