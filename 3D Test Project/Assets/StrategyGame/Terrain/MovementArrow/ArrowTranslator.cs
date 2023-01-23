using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTranslator {

    public enum ArrowType {

        None = 0,
        Base = 1,
        Body = 2,
        Corner = 3,
        End = 4
	}

    public struct ArrowInfo {

        public Quaternion dir;
        public ArrowType type;
	}

    public static ArrowInfo TranslateDirection(CellTerrain pastCell, CellTerrain currentCell, CellTerrain futureCell) {
        
        ArrowInfo arrowInfo = new ArrowInfo();
        Vector3 currentCellPos = currentCell.transform.position;

        if (futureCell == null && pastCell != null)
            arrowInfo.type = ArrowType.End;
        else if (pastCell == null && futureCell != null)
            arrowInfo.type = ArrowType.Base;

        Vector3 pastDir = pastCell != null ? currentCellPos - pastCell.transform.position : new Vector3(0, 0, 0);
        Vector3 futureDir = futureCell != null ? futureCell.transform.position - currentCellPos : new Vector3(0, 0, 0);

        if (arrowInfo.type == ArrowType.None && futureCell != null && pastCell != null)
		    switch (Vector3.Dot(pastDir, futureDir)) {
                case 1:
                    arrowInfo.type = ArrowType.Body;
                    break;
                case 0:
                    arrowInfo.type = ArrowType.Corner;
                    break;
                default:
                    arrowInfo.type = ArrowType.None;
                    break;
		    }

        if (arrowInfo.type == ArrowType.Corner) {
            Quaternion newRotation = Quaternion.LookRotation(pastDir, Vector3.down);
            Vector3 cross = Vector3.Cross(pastDir, futureDir);
            arrowInfo.dir = cross.y > 0 ? newRotation * Quaternion.Euler(0, 90, 0) : newRotation * Quaternion.Euler(0, 0, 0);
        }
        else
            arrowInfo.dir = Quaternion.LookRotation(futureDir + pastDir, Vector3.down);

        arrowInfo.dir.eulerAngles = new Vector3 (arrowInfo.dir.eulerAngles.x, arrowInfo.dir.eulerAngles.z, arrowInfo.dir.eulerAngles.y);
        
        return arrowInfo;
    }
}
