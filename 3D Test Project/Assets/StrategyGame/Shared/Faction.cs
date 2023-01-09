using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour {

	int originalFactionNumber;
	public int factionNumber;

	void Start() {

		originalFactionNumber = factionNumber;
	}

	public void RestoreFaction() {

		factionNumber = originalFactionNumber;
	}
}
