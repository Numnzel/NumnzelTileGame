using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine {

	[SerializeField] Canvas ui;
	[SerializeField] Player player;
	[SerializeField] Player enemy;

	public Player Player => player;
	public Player Enemy => enemy;
	public Canvas Interface => ui;

	static BattleStateMachine _instance;
	public static BattleStateMachine Instance { get { return _instance; } }

	void Awake() {

		if (_instance != null && _instance != this)
			Destroy(gameObject);
		else
			_instance = this;
	}

	void Start() {

		//Interface.Initialize(player, enemy);
		SetState(new Begin(this));
	}

	public void ExecuteState() {

		StartCoroutine(State.Execute());
	}
}