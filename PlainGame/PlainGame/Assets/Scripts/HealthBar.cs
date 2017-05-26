using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public float MaxHealth = 100;
    public Transform HealthBarImage;

    private float _currentHealth;

	void Start ()
    {
        _currentHealth = MaxHealth;
	}
	
	void Update ()
    {
        HealthBarImage.localScale = new Vector3(_currentHealth / MaxHealth, HealthBarImage.localScale.y);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            _currentHealth -= 10;
        }
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            GameController.GetInstance().GameOver();
        }
    }
}
