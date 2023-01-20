using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Faction))]
public abstract class Controller : MonoBehaviour {

	public UnityEvent OnClick;
	public UnityEvent OnHover;
	public CellTerrain CurrentCell;
	public CellTerrain HoverCell;
}
