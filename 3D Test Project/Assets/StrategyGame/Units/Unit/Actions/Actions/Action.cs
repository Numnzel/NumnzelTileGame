using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject {

    public string actionName;

	public abstract ActionState Execute(GameObject parent);
}
