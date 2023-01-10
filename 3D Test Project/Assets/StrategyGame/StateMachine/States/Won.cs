using System.Collections;
using UnityEngine;

public class Won : State {
    public Won(BattleStateMachine battleSystem) : base(battleSystem) {}

	public override IEnumerator Start() {

		//BattleStateMachine.Interface.SetDialogText("You won the battle!");
		Debug.Log("You won the battle!");
		yield break;
	}
}
