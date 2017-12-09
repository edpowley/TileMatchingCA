using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    public GridManager m_gridManager;
    public SpriteRenderer m_tileSprite;
    public SpriteRenderer m_dragSprite;
    public int m_x, m_y;
    public int m_mouseDragIndex;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(m_tileSprite != null);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (m_mouseDragIndex >= 0)
        {
            float phase = (Time.time - m_mouseDragIndex / 10.0f) * Mathf.PI * 4;
            m_dragSprite.enabled = true;
            m_dragSprite.color = new Color(1, 1, 1, Mathf.Sin(phase) * 0.5f + 0.5f);
        }
        else
        {
            m_dragSprite.enabled = false;
        }
    }

    public void setState(CellState state)
    {
        int colourIndex = state.m_colour;
        if (colourIndex >= 0 && colourIndex < m_gridManager.m_palette.Length)
            m_tileSprite.color = m_gridManager.m_palette[colourIndex];
        else
            m_tileSprite.color = Color.magenta;

        m_mouseDragIndex = state.m_mouseDragIndex;
    }

    void OnMouseDrag()
    {
        m_gridManager.mouseDragged(m_x, m_y);
    }

    void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            m_gridManager.mouseDragged(m_x, m_y);
        }
    }
}
