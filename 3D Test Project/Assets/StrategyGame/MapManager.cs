using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	static MapManager _instance;
	public static MapManager Instance { get => _instance; }

	void Awake() {
		
		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}
}
