using UnityEngine;

public class BoardState
{
    private readonly int m_width, m_height;
    internal CellState[,] m_currentCellStates;

    public BoardState(int width, int height)
    {
        m_width = width;
        m_height = height;

        m_currentCellStates = new CellState[m_width, m_height];

        do
        {
            for (int x = 0; x < m_width; x++)
            {
                for (int y = 0; y < m_height; y++)
                {
                    m_currentCellStates[x, y] = new CellState(Random.Range(1, 5));
                }
            }
        } while (hasThreeInARow());
    }

    private bool hasThreeInARow()
    {
        for (int x = 0; x < m_width; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                int c = m_currentCellStates[x, y].m_colour;
                if (x > 0 && x < m_width - 1
                    && c == m_currentCellStates[x - 1, y].m_colour
                    && c == m_currentCellStates[x + 1, y].m_colour)
                    return true;
                if (y > 0 && y < m_height - 1
                    && c == m_currentCellStates[x, y - 1].m_colour
                    && c == m_currentCellStates[x, y + 1].m_colour)
                    return true;
            }
        }

        return false;
    }
}

