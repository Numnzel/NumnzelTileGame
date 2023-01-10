using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyTurn : State {

	public EnemyTurn(BattleStateMachine battleSystem) : base(battleSystem) {}

	public override IEnumerator Start()	{
		
		//BattleStateMachine.Interface.SetDialogText($"{BattleStateMachine.Enemy.Name} attacks!");
		Debug.Log(battleSystem.Enemy.name + " (Enemy) attacks!");

		var isDead = false; // battleSystem.Player.Damage(battleSystem.Enemy.Execute);

		yield return new WaitForSeconds(1f);

		if (isDead)
			battleSystem.SetState(new Lost(battleSystem));
		else
			battleSystem.SetState(new PlayerTurn(battleSystem));
	}
}