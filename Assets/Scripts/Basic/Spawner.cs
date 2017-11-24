using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public int m_themeIdx = 0;

    public Brick[] m_bricks;

    public Transform m_locationNextBrick;

    Brick m_brickNext = null;

    Brick GetRandomBrick()
    {
        int i = Random.Range(0, m_bricks.Length);
        if (m_bricks[i])
        {
            return m_bricks[i];
        }
        else
        {
            Debug.Log("WARNING! Invalid brick");
            return null;
        }
    }

    public void SetTheme(string theme)
    {
        for(int i = 0; i < m_bricks.Length; i++)
        {
            m_bricks[i].SetTheme(theme);
        }
    }

    public Brick SpawBrick()
    {
        Brick brick = null;
        //brick = Instantiate(GetRandomBrick(), transform.position, Quaternion.identity) as Brick;
        brick = GetBrickNext();
        brick.transform.position = transform.position;
        brick.transform.localScale = Vector3.one;

        if (brick)
        {
            return brick;
        }
        else
        {
            return null;
        }
    }

    public void InitBricksNext()
    {
        m_brickNext = null;
        FillBrickNext();
    }

    public void FillBrickNext()
    {
        //Debug.Log("Fill");
        if (!m_brickNext)
        {
            m_brickNext = Instantiate(GetRandomBrick(), transform.position, Quaternion.identity) as Brick;
            m_brickNext.transform.position = new Vector3(0.65f, 26.2f, -1f);
            m_brickNext.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
    }

    public Brick GetBrickNext()
    {
        //Debug.Log("Get next");
        Brick brick = null;
        if (m_brickNext)
        {
            brick = m_brickNext;
            m_brickNext = null;
        }

        FillBrickNext();

        return brick;
    }

    private void Awake()
    {
        //InitBricksNext();
        //Debug.Log("Start");
    }
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
