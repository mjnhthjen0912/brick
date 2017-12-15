using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    GameObject m_backGround;
    GameObject m_backGroundFader;
    GameObject m_panelBlackCover;

    private void Awake()
    {
        m_backGround = GameObject.FindGameObjectWithTag("background");

        m_backGroundFader = GameObject.FindGameObjectWithTag("backgroundfader");

        m_panelBlackCover = GameObject.FindGameObjectWithTag("panelblackcover");

        if (!m_backGround)
        {
            Debug.Log("WARNING! There is no background defined!");
        }

        if (!m_backGroundFader)
        {
            Debug.Log("WARNING! There is no backgroundfader defined!");
        }

        if (!m_panelBlackCover)
        {
            Debug.Log("WARNING! There is no panelBlackCover defined!");
        }
    }

    public void SetBackground(string theme)
    {
        if (theme != null && theme != "" && m_backGround && m_backGroundFader)
        {
            m_backGround.GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + "background").ToString());
            m_backGroundFader.GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + "background").ToString());

            Image image = m_panelBlackCover.GetComponent<Image>();

            if (theme == ConstBrick.DEFAULT)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.7f);
            }
            //if (theme != ConstBrick.DEFAULT)
            //{
            //    m_backGround.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
            //    m_backGroundFader.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
            //}

            //m_backGround.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
            //m_backGroundFader.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
        }
        else
        {
            Debug.Log("WARNING! There is no background defined!");
        }
    }
    
}
