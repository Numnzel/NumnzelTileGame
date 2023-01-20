using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	static GameManager _instance;
	public static GameManager Instance { get { return _instance; } }

	public Controller[] Controllers;

	void Awake() {

		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}

	public void Start() {

		for (int i = 0; i < Controllers.Length; i++)
			Controllers[i].GetComponent<Faction>().factionNumber = i;
	}
}
