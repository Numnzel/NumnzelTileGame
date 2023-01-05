using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellHighlight : MonoBehaviour {

    public bool isBlocked;
    public int G;
    public int H;
    public int F { get => G + H; }
    public CellHighlight previous;

	void Start() {

        SetVisibility(true);
    }

    public void SetVisibility(bool set) {

        gameObject.GetComponent<Renderer>().enabled = set;
	}
}
