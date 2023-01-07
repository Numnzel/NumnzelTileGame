using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitUI : MonoBehaviour {

    [SerializeField] TMP_Text unitNameText;
    Unit unit;
	string unitName;

	public Unit Unit { get => unit; set => unit = value; }

	void Start() {

        PopulateInfo();
    }

	public void UpdateInfo() {

        ResetInfo();
        AssignInfo();
        PopulateInfo();
    }

    void ResetInfo() {

        unitName = null;
	}

    void AssignInfo() {

        if (unit != null)
            unitName = unit.uName;
    }

    void PopulateInfo() {

        unitNameText.text = (unitName == null) ? "-" : unitName;
    }
}
