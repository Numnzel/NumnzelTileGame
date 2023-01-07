using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int currentHealth = 0;
	public int maxHealth;

	private void Start() {
		
		if (currentHealth == 0)
			currentHealth = maxHealth;
	}
}
