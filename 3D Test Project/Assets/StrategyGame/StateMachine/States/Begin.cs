using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State {

	public Begin(BattleSystem battleSystem) : base(battleSystem) {}

	public override IEnumerator Start()	{
		
		//BattleSystem.Interface.SetDialogText($"A wild {BattleSystem.Enemy.Name} appeared!");
		Debug.Log("A wild " + battleSystem.Enemy.cName + " appeared!");

		yield return new WaitForSeconds(2f);

		battleSystem.SetState(new PlayerTurn(battleSystem));
	}
}
