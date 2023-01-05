using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : State {

	public PlayerTurn(BattleSystem battleSystem) : base(battleSystem) {}

	public override IEnumerator Start() {

		//BattleSystem.Interface.SetDialogText("Choose an action.");
		Debug.Log("Choose an action");
		yield break;
	}

	public override IEnumerator Attack(Action action) {

		var isDead = false; // BattleSystem.Enemy.Damage(BattleSystem.Player.Attack);
		Debug.Log(battleSystem.Player.cName + "Attacked.");
		battleSystem.Player.DoAction(action);

		yield return new WaitForSeconds(1f);

		if (isDead)
			battleSystem.SetState(new Won(battleSystem));
		else
			battleSystem.SetState(new EnemyTurn(battleSystem));
	}

	public override IEnumerator Move(Action action) {

		//BattleSystem.Interface.SetDialogText($"{BattleSystem.Player.Name} feels renewed strength!");
		Debug.Log(battleSystem.Player.cName + "Moved.");
		//battleSystem.Player.Heal(5);

		yield return new WaitForSeconds(1f);

		battleSystem.SetState(new EnemyTurn(battleSystem));
	}
}