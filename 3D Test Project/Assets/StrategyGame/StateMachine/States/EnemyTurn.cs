using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnemyTurn : State {

	public EnemyTurn(BattleSystem battleSystem) : base(battleSystem) {}

	public override IEnumerator Start()	{
		
		//BattleSystem.Interface.SetDialogText($"{BattleSystem.Enemy.Name} attacks!");
		Debug.Log(battleSystem.Enemy.name + "attacks!");

		var isDead = false; // battleSystem.Player.Damage(battleSystem.Enemy.Attack);

		yield return new WaitForSeconds(1f);

		if (isDead)
			battleSystem.SetState(new Lost(battleSystem));
		else
			battleSystem.SetState(new PlayerTurn(battleSystem));
	}
}