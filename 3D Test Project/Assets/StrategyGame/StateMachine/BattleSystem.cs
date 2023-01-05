using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine {

	[SerializeField] private Canvas ui;
	[SerializeField] private Unit player;
	[SerializeField] private Unit enemy;

	public Unit Player => player;
	public Unit Enemy => enemy;
	public Canvas Interface => ui;

	static BattleSystem _instance;
	public static BattleSystem Instance { get { return _instance; } }

	private void Awake() {

		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}

	private void Start() {

		//Interface.Initialize(player, enemy);
		SetState(new Begin(this));
	}

	public void AttackButton(Action action) {

		StartCoroutine(State.Attack(action));
	}

	public void MoveButton(Action action) {

		StartCoroutine(State.Move(action));
	}
}