using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour {

    GameObject m_backGround;
    GameObject m_backGroundFader;

    // Use this for initialization
    void Start () {
        m_backGround = GameObject.FindGameObjectWithTag("background");

        m_backGroundFader = GameObject.FindGameObjectWithTag("backgroundfader");

        if (!m_backGround)
        {
            Debug.Log("WARNING! There is no background defined!");
        }

        if (!m_backGroundFader)
        {
            Debug.Log("WARNING! There is no backgroundfader defined!");
        }
    }

    public void SetBackground(string theme)
    {
        if (theme != null && theme != "" && m_backGround && m_backGroundFader)
        {
            m_backGround.GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + "background").ToString());
            m_backGroundFader.GetComponent<Image>().sprite = Resources.Load<Sprite>(string.Format("Sprites/" + theme + "/" + "background").ToString());

            //if (theme != ConstBrick.DEFAULT)
            //{
            //    m_backGround.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
            //    m_backGroundFader.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
            //}

            //m_backGround.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
            //m_backGroundFader.GetComponent<RectTransform>().sizeDelta = new Vector2(310, 500);
        }
    }
    
}
