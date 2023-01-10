using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	[SerializeField] Image _img;
	[SerializeField] Sprite _default;
	[SerializeField] Sprite _pressed;
	[SerializeField] AudioClip _compressClip;
	[SerializeField] AudioClip _uncompressClip;
	[SerializeField] AudioSource _source;
	Action action;
	Unit unit;

	public void OnPointerDown(PointerEventData eventData) {
		
	}

	public void OnPointerUp(PointerEventData eventData) {
		
	}

	public void SetAction(Action action) {

		this.action = action;
	}

	public void SetUnit(Unit unit) {

		this.unit = unit;
	}

	public void IWasClicked() {

		unit.DoAction(action);
		//BattleStateMachine.Instance.ExecuteAction(action); // Player.DoAction(action);
		Debug.Log("CLICKED");
	}
}
