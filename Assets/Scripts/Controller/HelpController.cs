using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour {
    GameObject m_swipeHelp;

    bool m_isOpen = false;

    public void ToggleHelp()
    {
        m_isOpen = !m_isOpen;

        if (m_swipeHelp)
        {
            m_swipeHelp.SetActive(m_isOpen);
        }
    }

    // Use this for initialization
    void Start()
    {
        m_swipeHelp = GameObject.FindGameObjectWithTag("swipehelp");

        if (m_swipeHelp)
        {
            m_swipeHelp.SetActive(false);
        }
    }
}
