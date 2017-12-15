using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSprite : MonoBehaviour {

    private Image m_spriteLeft;
    public float m_prioNext = 0.04f;
    private float m_timeToNext = 0f;
    public string m_strFolderSprite = "TutorialLeft";
    public string m_strSprite = "tut1-";
    private int m_idxSpriteCurrent = 0;
    public bool isStart = false;
    private Sprite[] m_sprite = new Sprite[12];

    private void Awake()
    {
        m_spriteLeft = GetComponent<Image>();
    }

    // Use this for initialization
    void Start () {
        m_prioNext = 0.04f;
        m_timeToNext = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (isStart == true)
        {
            if (m_timeToNext + m_prioNext < Time.time)
            {
                m_timeToNext = Time.time;
                if(m_idxSpriteCurrent <= 11)
                {
                    if (m_sprite[m_idxSpriteCurrent] == null)
                    {
                        m_spriteLeft.sprite = Resources.Load<Sprite>(string.Format("Sprites/" + m_strFolderSprite + "/" + m_strSprite + m_idxSpriteCurrent.ToString()).ToString());
                    }
                    else
                    {
                        m_spriteLeft.sprite = m_sprite[m_idxSpriteCurrent];
                    }
                }
                
                m_idxSpriteCurrent++;
                if (m_idxSpriteCurrent > 20)
                {
                    m_idxSpriteCurrent = 0;
                }
            }
        }
    }
}
