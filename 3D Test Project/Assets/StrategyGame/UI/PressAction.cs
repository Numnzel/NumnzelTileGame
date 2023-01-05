using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	[SerializeField] private Image _img;
	[SerializeField] private Sprite _default, _pressed;
	[SerializeField] private AudioClip _compressClip, _uncompressClip;
	[SerializeField] private AudioSource _source;
	private Action action;

	public void OnPointerDown(PointerEventData eventData) {
		
	}

	public void OnPointerUp(PointerEventData eventData) {
		
	}

	public void SetAction(Action action) {

		this.action = action;
	}

	public void IWasClicked() {

		BattleSystem.Instance.AttackButton(action); // Player.DoAction(action);
		Debug.Log("CLICKED");
	}
}
