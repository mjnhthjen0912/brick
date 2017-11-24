using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeController : MonoBehaviour {

    GameObject m_swipeTheme;

    bool m_isOpen = false;

    public void ToggleTheme()
    {
        m_isOpen = !m_isOpen;

        if (m_swipeTheme)
        {
            m_swipeTheme.SetActive(m_isOpen);
        }
    }

	// Use this for initialization
	void Start () {
        m_swipeTheme = GameObject.FindGameObjectWithTag("swipetheme");

        if (m_swipeTheme)
        {
            m_swipeTheme.SetActive(false);
        }
    }
}
