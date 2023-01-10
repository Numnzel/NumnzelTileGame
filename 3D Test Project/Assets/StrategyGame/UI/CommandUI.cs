using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandUI : MonoBehaviour {

    [SerializeField] GameObject actionsPanel;
    [SerializeField] GameObject actionButton;
	Unit unit;
	List<Action> actions;
    List<GameObject> buttons;

	public Unit Unit { get => unit; set => unit = value; }

	void Start() {

        buttons = new List<GameObject>();
	}

	public void UpdateInfo() {

        ResetInfo();
        AssignInfo();
        PopulateInfo();
    }

    void ResetInfo() {

        actions = null;
    }

    void AssignInfo() {

        if (unit != null)
            actions = unit.actions;
    }

    void PopulateInfo() {

        TMP_Text TMPro;

        if (buttons == null)
            return;

        foreach (GameObject button in buttons)
            Destroy(button);

        if (actions == null)
            return;

        foreach (Action action in actions) {

            GameObject button = Instantiate(actionButton, actionsPanel.transform);
            ActionButton buttonAction = button.GetComponent<ActionButton>();
            buttonAction.SetAction(action);
            buttonAction.SetUnit(unit);

            TMPro = button.GetComponentInChildren<TMP_Text>();
            TMPro.text = action.actionName;
            buttons.Add(button);
        }
    }
}
