using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTranslator {

    public enum ArrowDirection {

        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4,
        TopRight = 5,
        BottomRight = 6,
        TopLeft = 7,
        BottomLeft = 8,
        UpFinished = 9,
        DownFinished = 10,
        LeftFinished = 11,
        RightFinished = 12
    }

    public static ArrowDirection TranslateDirection(CellTerrain previousCell, CellTerrain currentCell, CellTerrain futureCell) {

        bool isFinal = futureCell == null;

        Vector3 pastDir = previousCell != null ? currentCell.transform.position - previousCell.transform.position : new Vector3(0, 0);
        Vector3 futureDir = futureCell != null ? futureCell.transform.position - currentCell.transform.position : new Vector3(0, 0);
        Vector3 direction = pastDir != futureDir ? pastDir + futureDir : futureDir;

        if (direction.x == 0 && direction.z == 1 && !isFinal)
            return ArrowDirection.Up;

        if (direction.x == 0 && direction.z == -1 && !isFinal)
            return ArrowDirection.Down;

        if (direction.x == 1 && direction.z == 0 && !isFinal)
            return ArrowDirection.Right;

        if (direction.x == -1 && direction.z == 0 && !isFinal)
            return ArrowDirection.Left;

        if (direction.x == 1 && direction.z == 1)
            if (pastDir.z < futureDir.z)
                return ArrowDirection.BottomLeft;
            else
                return ArrowDirection.TopRight;

        if (direction.x == -1 && direction.z == 1)
            if (pastDir.z < futureDir.z)
                return ArrowDirection.BottomRight;
            else
                return ArrowDirection.TopLeft;

        if (direction.x == 1 && direction.z == -1)
            if (pastDir.z > futureDir.z)
                return ArrowDirection.TopLeft;
            else
                return ArrowDirection.BottomRight;

        if (direction.x == -1 && direction.z == -1)
            if (pastDir.z > futureDir.z)
                return ArrowDirection.TopRight;
            else
                return ArrowDirection.BottomLeft;

        if (direction.x == 0 && direction.z == 1 && isFinal)
            return ArrowDirection.UpFinished;

        if (direction.x == 0 && direction.z == -1 && isFinal)
            return ArrowDirection.DownFinished;

        if (direction.x == 1 && direction.z == 0 && isFinal)
            return ArrowDirection.RightFinished;

        if (direction.x == -1 && direction.z == 0 && isFinal)
            return ArrowDirection.LeftFinished;

        return ArrowDirection.None;
    }
}
