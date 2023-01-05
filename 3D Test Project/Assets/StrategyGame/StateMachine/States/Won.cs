using System.Collections;
using UnityEngine;

public class Won : State {
    public Won(BattleSystem battleSystem) : base(battleSystem) {}

	public override IEnumerator Start() {

		//BattleSystem.Interface.SetDialogText("You won the battle!");
		Debug.Log("You won the battle!");
		yield break;
	}
}
