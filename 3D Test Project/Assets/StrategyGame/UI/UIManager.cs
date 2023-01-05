using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    static UIManager _instance;
    public static UIManager Instance { get => _instance; }

    public Canvas canvas;
    [SerializeField] private CommandUI commandUI;
    [SerializeField] private UnitUI unitUI;
    [SerializeField] private TerrainUI terrainUI;

    void Awake() {

        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void UpdateUnitCommandUI(Unit unit) {

        if (commandUI == null)
            return;

        commandUI.Unit = unit;
        commandUI.UpdateInfo();
	}

    public void UpdateUnitSelectionUI(Unit unit) {
        
        if (unitUI == null)
            return;

        unitUI.Unit = unit;
        unitUI.UpdateInfo();
    }

    public void UpdateTerrainUI(CellTerrain cellTerrain) {

        if (terrainUI == null)
            return;

        terrainUI.CellTerrain = cellTerrain;
        terrainUI.UpdateInfo();
    }
}
