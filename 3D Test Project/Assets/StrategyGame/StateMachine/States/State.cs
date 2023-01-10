using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

	protected BattleStateMachine battleSystem;

	public State(BattleStateMachine battleSystem) {

		this.battleSystem = battleSystem;
	}

    public virtual IEnumerator Start() {
		yield break;
	}

	public virtual IEnumerator Execute() {
		yield break;
	}
}
