using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCell : MonoBehaviour {

    void Start() {

        SetVisibility(true);
    }

    public void SetVisibility(bool set) {

        gameObject.GetComponent<Renderer>().enabled = set;
	}
}
