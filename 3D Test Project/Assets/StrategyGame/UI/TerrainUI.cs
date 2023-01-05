using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerrainUI : MonoBehaviour {

    [SerializeField] TMP_Text terrainNameText;
    [SerializeField] TMP_Text terrainMoveCostText;
    [SerializeField] TMP_Text terrainViewCostText;
    CellTerrain cellTerrain;
	string terrainName;
    int terrainMoveCost;
    int terrainViewCost;

	public CellTerrain CellTerrain { get => cellTerrain; set => cellTerrain = value; }

	void Start() {

        PopulateInfo();
    }

	public void UpdateInfo() {

        ResetInfo();
        AssignInfo();
        PopulateInfo();
    }

    void ResetInfo() {

        terrainName = null;
        terrainMoveCost = 0;
        terrainViewCost = 0;
    }

    void AssignInfo() {

        if (cellTerrain == null)
            return;

        terrainName = cellTerrain.TName;
        terrainMoveCost = cellTerrain.MoveCost;
        terrainViewCost = cellTerrain.ViewCost;
    }

    void PopulateInfo() {

        terrainNameText.text = (terrainName == null) ? "-" : terrainName;
        terrainMoveCostText.text = terrainMoveCost.ToString();
        terrainViewCostText.text = terrainViewCost.ToString();
    }
}
