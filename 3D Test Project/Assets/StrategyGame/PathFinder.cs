using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder {

	public GameGrid gameGrid;
	/*
    public List<CellHighlight> FindPath(CellHighlight start, CellHighlight end) {

		List<CellHighlight> openList = new List<CellHighlight>();
		List<CellHighlight> closedList = new List<CellHighlight>();

		openList.Add(start);

		end.transform.position = new Vector3(end.transform.position.x, end.transform.position.y+0.4f, end.transform.position.z);

		int i = 0;

		while (openList.Count > 0) {

			CellHighlight currentHighlightCell = openList.OrderBy(x => x.F).First();

			openList.Remove(currentHighlightCell);
			closedList.Add(currentHighlightCell);

			if (currentHighlightCell.transform.position == end.transform.position || ++i > 10) // el problema está en que currenthighlight nunca sera end pq getneightbours no devolvera end!
				return GetFinishedList(start, end);

			List<CellHighlight> neighbourCells = GetNeightbours(currentHighlightCell);

			foreach (CellHighlight neighbour in neighbourCells) {

				CellHighlight neighbourCell = neighbour;

				if (neighbourCell.isBlocked || closedList.Contains(neighbourCell))
					continue;

				neighbourCell.G = GetManhattanDistance(start, neighbourCell);
				neighbourCell.H = GetManhattanDistance(end, neighbourCell);

				neighbourCell.previous = currentHighlightCell;

				if (!openList.Contains(neighbourCell))
					openList.Add(neighbourCell);
			}
		}

		return openList;
	}

	List<CellHighlight> GetFinishedList(CellHighlight start, CellHighlight end) {
		
		List<CellHighlight> finishedList = new List<CellHighlight>();

		CellHighlight currentCell = end;

		while (currentCell != start) {

			finishedList.Add(currentCell);
			currentCell = currentCell.previous; // PROBLEM
		}

		finishedList.Reverse();

		return finishedList;
	}

	int GetManhattanDistance(CellHighlight start, CellHighlight end) {

		Vector2 startPos = start.transform.position;
		Vector2 endPos = end.transform.position;

		return Mathf.RoundToInt(Mathf.Abs(startPos.x - endPos.x) + Mathf.Abs(startPos.y - endPos.y));
	}
	
	List<CellHighlight> GetNeightbours(CellHighlight currentHighlightCell) {

		CellTerrain currentCell = gameGrid.ReadCell(currentHighlightCell.transform.position, true);
		List<CellTerrain> neighbours = gameGrid.ReadCellNeighbour(currentCell);
		List<CellHighlight> neighboursHCells = new List<CellHighlight>();

		foreach (CellTerrain cell in neighbours)
			neighboursHCells.Add(gameGrid.SpawnOnCell(gameGrid.cellHighlight.gameObject, currentCell).GetComponent<CellHighlight>());

		return neighboursHCells;
	}^*/
}
