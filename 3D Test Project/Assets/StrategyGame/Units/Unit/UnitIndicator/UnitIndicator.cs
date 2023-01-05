using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIndicator : MonoBehaviour {

    SpriteRenderer indicatorSprite;
    [SerializeField] Sprite sprIndicatorAvailable;
    [SerializeField] Sprite sprIndicatorHighlight;
    [SerializeField] Sprite sprIndicatorSelected;

    void Start() {

        indicatorSprite = GetComponent<SpriteRenderer>();
        SetSpriteAvailable();
        //transform.forward = CameraSystem.GetCameraTransform().forward;
    }

    public void SetVisibility(bool set) {

        if (indicatorSprite == null)
            return;

        Color color = indicatorSprite.color;
        color.a = (set) ? 255 : 0;
        indicatorSprite.color = color;
    }

    public void SetSpriteAvailable() {
        SetSprite(sprIndicatorAvailable);
	}
    public void SetSpriteHighlight() {
        SetSprite(sprIndicatorHighlight);
	}
    public void SetSpriteSelected() {
        SetSprite(sprIndicatorSelected);
	}

    void SetSprite(Sprite sprite) {

        if (indicatorSprite != null)
            indicatorSprite.sprite = sprite;
	}
}
