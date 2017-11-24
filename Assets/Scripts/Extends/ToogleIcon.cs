using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToogleIcon : MonoBehaviour {
    public Sprite m_spriteIconOn;
    public Sprite m_spriteIconOf;
    public bool m_isOn = true;

    private Image m_image;
	// Use this for initialization
	void Start () {
        m_image = GetComponent<Image>();
        if (m_isOn)
        {
            m_image.sprite = m_spriteIconOn;
        }
        else
        {
            m_image.sprite = m_spriteIconOf;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToogleOnIcon(bool isOn)
    {
        if(!m_spriteIconOf || !m_spriteIconOn || !m_image)
        {
            Debug.Log("WARNING image or sprite icon of or sprite icon on not found!");
            return;
        }
        if (isOn)
        {
            m_image.sprite = m_spriteIconOn;
        }
        else
        {
            m_image.sprite = m_spriteIconOf;
        }
    }
}
