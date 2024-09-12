using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmunitionBarBehavior : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;

	public void SetMaxAmmunition(float ammunition)
	{
		slider.maxValue = ammunition;
		slider.value = PlayerPrefs.GetFloat("Ammo",ammunition);

		fill.color = gradient.Evaluate(1f);
	}

    public void SetAmmunition(float ammunition)
	{
		slider.value = ammunition;

		fill.color = gradient.Evaluate(slider.normalizedValue);
	}
}
