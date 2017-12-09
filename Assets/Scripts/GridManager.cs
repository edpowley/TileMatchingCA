using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GridCell m_gridCellPrefab;
    public int m_gridWidth = 5, m_gridHeight = 5;

    private GridCell[,] m_grid;

    public Color[] m_palette;

    private BoardState m_boardState;

    private int m_nextMouseDragIndex;

    // Use this for initialization
    void Start()
    {
        m_boardState = new BoardState(m_gridWidth, m_gridHeight);

        // Create the array
        m_grid = new GridCell[m_gridWidth, m_gridHeight];

        // Create and position the cells
        for (int x = 0; x < m_gridWidth; x++)
        {
            for (int y = 0; y < m_gridHeight; y++)
            {
                GridCell cell = GameObject.Instantiate(m_gridCellPrefab);
                cell.m_gridManager = this;
                cell.gameObject.name = string.Format("Cell {0},{1}", x, y);
                cell.m_x = x;
                cell.m_y = y;
                m_grid[x, y] = cell;
                cell.transform.SetParent(transform, false);
                cell.transform.localPosition = new Vector3(x, y, 0);
            }
        }

        updateCellsFromBoardState();
    }

    void updateCellsFromBoardState()
    {
        for (int x = 0; x < m_gridWidth; x++)
        {
            for (int y = 0; y < m_gridHeight; y++)
            {
                m_grid[x, y].setState(m_boardState.m_currentCellStates[x, y]);
            }
        }
    }

    void Update()
    {
        // Reset m_nextMouseDragIndex when the mouse button is first pressed
        if (Input.GetMouseButtonDown(0))
        {
            m_nextMouseDragIndex = 0;

            for (int x = 0; x < m_gridWidth; x++)
            {
                for (int y = 0; y < m_gridHeight; y++)
                {
                    m_boardState.m_currentCellStates[x, y].m_mouseDragIndex = -1;
                }
            }

            updateCellsFromBoardState();
        }
    }

    internal void mouseDragged(int cellX, int cellY)
    {
        if (m_boardState.m_currentCellStates[cellX, cellY].m_mouseDragIndex < 0)
        {
            m_boardState.m_currentCellStates[cellX, cellY].m_mouseDragIndex = m_nextMouseDragIndex;
            m_nextMouseDragIndex++;
            updateCellsFromBoardState();
        }
    }
}
