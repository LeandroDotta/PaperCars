using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
	public float maxHealth;
	[SerializeField] private Image _bar;

	public void SetHealth(float health)
	{
		if(health > maxHealth)
			health = maxHealth;
		else if(health < 0)
			health = 0;

		float fill = health / maxHealth;
		_bar.fillAmount = fill;
	}
}
