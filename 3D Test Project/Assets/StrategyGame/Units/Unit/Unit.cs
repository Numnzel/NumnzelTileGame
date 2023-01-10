using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]

public class Unit : MonoBehaviour, IActor {

	public string uName;
	public List<Action> actions;
	public GameObject circle;
	public UnitIndicator indicator;
	bool available = true;

	void Update() {

		indicator.SetVisibility(available);
	}
	
	public void DoAction(Action action) {

		action.Execute(gameObject);
		//BattleStateMachine.Instance.ExecuteAction(action);
	}
}