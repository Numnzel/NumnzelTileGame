using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Action/EndTurn")]
public class EndTurn : Action {

	public override void Execute(GameObject parent) {
		
		// Action to be done:
		BattleStateMachine.Instance.ExecuteState();
	}
}
