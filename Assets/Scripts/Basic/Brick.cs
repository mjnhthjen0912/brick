using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    
    public bool m_canRotate = true;

    GameObject[] m_unitBrickFx = new GameObject[4];
    public string strUnitFxTag = "unitfx";
    
    // general move method
    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }
    
    //public methods for moving left, right, up and down, respectively
    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }

    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }

    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }


    //public methods for rotating right and left
    public void RotateRight()
    {
        if (m_canRotate)
            transform.Rotate(0, 0, -90);
    }
    public void RotateLeft()
    {
        if (m_canRotate)
            transform.Rotate(0, 0, 90);
    }

    public void RotateClockwise(bool clockwise)
    {
        if (clockwise) {
            RotateRight();
        }
        else {
            RotateLeft();
        }
    }

    public void LandBrickFx()
    {
        int i = 0;
        foreach(Transform child in transform)
        {
            if (m_unitBrickFx[i])
            {
                m_unitBrickFx[i].transform.position = new Vector3(child.position.x, child.position.y, -2);
                ParticlePlayer particlePlayer = m_unitBrickFx[i].GetComponent<ParticlePlayer>();
                if (particlePlayer)
                {
                    particlePlayer.Play();
                }
            }
            i++;
        }
    }

    public void SetTheme(string theme)
    {
        
        SpriteRenderer[] unit = GetComponentsInChildren<SpriteRenderer>();

        if (unit != null)
        {
            //Debug.Log(unit.Length);
            AddSprite(Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + transform.tag).ToString()), unit);

            //if (theme == ConstBrick.DEFAULT)
            //{
            //    AddSprite(Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + "roundedBlock").ToString()), unit);
            //}
            //else
            //{
            //    AddSprite(Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + transform.tag).ToString()), unit);

            //}
        }
    }

    void AddSprite(Sprite sprite, SpriteRenderer[] unit)
    {
        for(int i = 0; i < unit.Length; i++)
        {
            unit[i].sprite = sprite;
            unit[i].color = Color.white;
        }
    }

    // Use this for initialization
    void Start () {
        //InvokeRepeating("MoveDown", 0, 0.5f);
        //InvokeRepeating("RotateRight", 0, 0.5f);
        strUnitFxTag = "unitfx";
        if (strUnitFxTag != null && strUnitFxTag != "")
        {
            m_unitBrickFx = GameObject.FindGameObjectsWithTag(strUnitFxTag);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
