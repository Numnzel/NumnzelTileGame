using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCircle : MonoBehaviour {

    [SerializeField] Material matHovered;
    [SerializeField] Material matSelected;
    MeshRenderer meshRenderer;
    bool selected = false;

	public bool Selected { get => selected; }

	void Start() {

        meshRenderer = GetComponent<MeshRenderer>();
        SetVisibility(false);
    }

    public void SetSelected(bool set) {

        selected = set;
        meshRenderer.material = (selected) ? matSelected : matHovered;
    }

    public void SetVisibility(bool set) {

        meshRenderer.forceRenderingOff = !set;
    }
}
