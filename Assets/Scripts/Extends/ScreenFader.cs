using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(MaskableGraphic))]
public class ScreenFader : MonoBehaviour {

    public float m_startAlpha = 1f;
    public float m_targetAlpha = 0f;
    public float m_delay = 0f;
    public float m_timeToFade = 1f;

    float m_inc;
    float m_currentAlpha;
    MaskableGraphic m_graphic;
    Color m_originalColor;

	// Use this for initialization
	void Start () {
        // cache the Maskable Graphic
        m_graphic = GetComponent<MaskableGraphic>();

        // cache our graphic's original color
        m_originalColor = m_graphic.color;

        // set our current alpha to the starting value
        m_currentAlpha = m_startAlpha;

        // set the color of the graphic, based on the original rgb and current alpha value
        Color tempColor = new Color(m_originalColor.r, m_originalColor.g, m_originalColor.b, m_currentAlpha);
        m_graphic.color = tempColor;

        // calculate how much we increment the alpha based on our transition time; this rate is per FRAME, note Time.deltaTime
        m_inc = ((m_targetAlpha - m_startAlpha) / m_timeToFade) * Time.deltaTime;

        // start our coroutine
        StartCoroutine(FadeRoutine());
    }
	
	IEnumerator FadeRoutine()
    {
        // wait to begin the fade effect
        yield return new WaitForSeconds(m_delay);

        // 
        while (Mathf.Abs(m_targetAlpha - m_currentAlpha) > 0.01f)
        {
            yield return new WaitForEndOfFrame();

            // increment our alpha value
            m_currentAlpha = m_currentAlpha + m_inc;

            // set the color of the graphic, based on the original rgb and current alpha value
            Color tempColor = new Color(m_originalColor.r, m_originalColor.g, m_originalColor.b, m_currentAlpha);
            m_graphic.color = tempColor;


        }
    }
}
