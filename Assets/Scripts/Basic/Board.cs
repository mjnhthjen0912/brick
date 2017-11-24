using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public Transform m_transformClone;
    public int m_hight = 28;
    public int m_width = 15;
    public int m_header = 4;
    public bool m_showBoard;
    
    private Transform[,] m_board;

    public int m_complatedRow = 0;

    public ParticlePlayer[] m_rowGlowFx = new ParticlePlayer[4];

    public void Awake()
    {
        m_board= new Transform[m_width, m_hight];
    }

    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < m_width && y >= 0);
    }

    bool IsOccupied(int x, int y, Brick brick)
    {
        return (m_board[x, y] != null && m_board[x, y].parent != brick.transform);
    }

    public bool IsInvalidPosition(Brick brick)
    {
        foreach(Transform child in brick.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            if(!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }
            if(IsOccupied((int)pos.x, (int)pos.y, brick))
            {
                return false;
            }
        }
        return true;
    }

    public void DrawBoard()
    {
        //Debug.Log("drawboard");
        
        if (m_transformClone != null) {
            Transform clone;
            for (int i = 0; i < m_hight - m_header; i++) {
                for (int j = 0; j < m_width; j++) {
                    clone = Instantiate(m_transformClone, new Vector3(j, i, 0), Quaternion.identity) as Transform;
                    clone.name = "EmptySquare (x:" + j.ToString() + ", y:" + i.ToString() + ")";
                    clone.transform.parent = transform;
                    clone.gameObject.SetActive(m_showBoard);
                }
            }
        }
        else {
            Debug.Log("TransformClone not assign");
        }
    }

    public void StoreBrickInGrid(Brick brick)
    {
        if (brick == null)
        {
            return;
        }
        foreach(Transform child in brick.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            m_board[(int)pos.x, (int)pos.y] = child;
        }
    }

    bool IsFullRow(int y)
    {
        for(int i = 0; i < m_width; i++)
        {
            if(m_board[i, y] == null)
            {
                return false;
            }
        }
        return true;
    }

    void RemoveOneRow(int y)
    {
        for(int i = 0; i < m_width; i++)
        {
            if(m_board[i, y] != null)
            {
                Destroy(m_board[i, y].gameObject);
                m_board[i, y] = null;
            }
        }
    }

    void ShiftOneRow(int y)
    {
        for(int i = 0; i < m_width; i++)
        {
            if(m_board[i, y] != null)
            {
                m_board[i, y - 1] = m_board[i, y];
                m_board[i, y] = null;
                m_board[i, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    void ShiftRows(int startY)
    {
        for(int i = startY; i < m_hight; i++)
        {
            ShiftOneRow(i);
        }
    }

    public IEnumerator RemoveAllRows()
    {
        m_complatedRow = 0;

        for (int i = 0; i < m_hight; i++)
        {
            if (IsFullRow(i))
            {
                ClearRowFx(m_complatedRow, i);
                m_complatedRow++;
            }
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < m_hight; i++)
        {
            if (IsFullRow(i))
            {
                RemoveOneRow(i);
                ShiftRows(i+1);
                yield return new WaitForSeconds(0.2f);
                i--;
            }
        }
    }

    public bool IsOverLimit(Brick brick)
    {
        foreach(Transform child in brick.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            if(pos.y >= m_hight - m_header - 1)
            {
                return true;
            }
        }
        return false;
    }

    private void ClearRowFx(int idx, int y)
    {
        if (m_rowGlowFx[idx])
        {
            m_rowGlowFx[idx].transform.position = new Vector3(0, y, -1);
            m_rowGlowFx[idx].Play();
        }
    }

	// Use this for initialization
	void Start () {
        DrawBoard();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
