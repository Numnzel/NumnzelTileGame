using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State {

	public Begin(BattleStateMachine battleSystem) : base(battleSystem) {}

	public override IEnumerator Start()	{
		
		//BattleStateMachine.Interface.SetDialogText($"A wild {BattleStateMachine.Enemy.Name} appeared!");
		Debug.Log("A wild " + battleSystem.Enemy.playerName + " appeared!");

		yield return new WaitForSeconds(2f);

		battleSystem.SetState(new PlayerTurn(battleSystem));
	}
}
