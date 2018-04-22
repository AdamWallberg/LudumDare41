using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

	public AudioSource _hurtSound;
	public AudioSource _deathSound;

	public AudioSource _pickupSound;
	public AudioSource _fireSound;
	public AudioSource _fireSound2;
	public Transform _gunPlacement;
	Transform _gun = null;

	public Transform _laserPrefab;

	bool _dead = false;

	void Start ()
	{
		_renderers = GetComponentsInChildren<SpriteRenderer>();
		_controller = GetComponent<PlayerController>();
	}
	
	void Update ()
	{
		if ((_food <= 0.0f || _play <= 0.0f || _clean <= 0.0f) && !_dead)
		{
			StartCoroutine(Die());
			_dead = true;
		}

		if (_dead)
			return;

		_invincibilityTimer -= Time.deltaTime;

		_food -= Time.deltaTime * (1.0f / _decreaseSpeed);
		_play -= Time.deltaTime * (1.0f / _decreaseSpeed);
		_clean -= Time.deltaTime * (1.0f / _decreaseSpeed);

		_food = Mathf.Clamp(_food, -1.0f, 1.0f);
		_play = Mathf.Clamp(_play, -1.0f, 1.0f);
		_clean = Mathf.Clamp(_clean, -1.0f, 1.0f);

		if(Input.GetButtonDown("Fire1") && _gun)
		{
			Transform t = Instantiate(_laserPrefab, _gun);
			t.position = _gun.position;
			_fireSound.Play();
			_fireSound2.Play();
			Destroy(_gun.gameObject, 0.2f);
			_gun = null;
		}
	}

	private void OnGUI()
	{
		_foodImage.fillAmount = _food;
		_playImage.fillAmount = _play;
		_cleanImage.fillAmount = _clean;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (_dead)
			return;

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
			_hurtSound.Play();
			StartCoroutine(Blink());
		}
		else if(collision.gameObject.layer == LayerMask.NameToLayer("Gun") && !_gun)
		{
			_pickupSound.Play();
			_gun = collision.gameObject.transform;
			_gun.transform.position = _gunPlacement.position;
			_gun.SetParent(_gunPlacement);
			_gun.GetComponent<Projectile>().enabled = false;
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

	IEnumerator Die()
	{
		yield return new WaitForSeconds(0.2f);
		_deathSound.Play();
		yield return new WaitForSeconds(0.2f);
		_deathSound.Play();
		yield return new WaitForSeconds(0.2f);

		SceneManager.LoadScene("GameOver");
		yield return null;
	}

	public void OnPlay()
	{
		_play += 0.33f;
	}
}
