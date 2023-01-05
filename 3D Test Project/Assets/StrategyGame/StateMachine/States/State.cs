using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

	protected BattleSystem battleSystem;

	public State(BattleSystem battleSystem) {

		this.battleSystem = battleSystem;
	}

    public virtual IEnumerator Start() {
		yield break;
	}

	public virtual IEnumerator Attack(Action action) {
		yield break;
	}

	public virtual IEnumerator Move(Action action) {
		yield break;
	}
}
