using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBrick : MonoBehaviour {
    Brick m_ghostBrick = null;
    bool m_hitBottom = false;
    public Color m_color = new Color(1f, 1f, 1f, 0.2f);

    public void DrawGhost(Brick originalBrick, Board gameBoard)
    {
        if (!m_ghostBrick)
        {
            m_ghostBrick = Instantiate(originalBrick, originalBrick.transform.position, originalBrick.transform.rotation) as Brick;
            m_ghostBrick.gameObject.name = "GhostBrick";

            SpriteRenderer[] allRenderers = m_ghostBrick.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer r in allRenderers)
            {
                r.color = m_color;
            }

        }
        else
        {
            m_ghostBrick.transform.position = originalBrick.transform.position;
            m_ghostBrick.transform.rotation = originalBrick.transform.rotation;
            m_ghostBrick.transform.localScale = Vector3.one;

        }

        m_hitBottom = false;

        while (!m_hitBottom)
        {
            m_ghostBrick.MoveDown();
            if (!gameBoard.IsInvalidPosition(m_ghostBrick))
            {
                m_ghostBrick.MoveUp();
                m_hitBottom = true;
            }
        }

    }

    public void Reset()
    {
        if(m_ghostBrick)
            Destroy(m_ghostBrick.gameObject);
    }
}
