using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : State {

	public PlayerTurn(BattleStateMachine battleSystem) : base(battleSystem) {}

	public override IEnumerator Start() {

		//BattleStateMachine.Interface.SetDialogText("Choose an action.");
		Debug.Log("Choose an action");
		yield break;
	}

	public override IEnumerator Execute() {

		Validator validator = new ValidatorCompare<double>(5f, 5, Operators.equal);
		Debug.Log($"Hola validator: {validator.IsValid()}");

		var isDead = false;
		Debug.Log(battleSystem.Player.playerName + "Attacked.");

		yield return new WaitForSeconds(1f);

		if (isDead)
			battleSystem.SetState(new Won(battleSystem));
		else
			battleSystem.SetState(new EnemyTurn(battleSystem));
	}
}