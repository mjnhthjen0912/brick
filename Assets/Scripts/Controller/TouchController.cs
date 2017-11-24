using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour {

    public delegate void TouchEventHandler(Vector2 swipe);

    public static TouchEventHandler SwipeEvent;
    public static TouchEventHandler SwipeEndEvent;

    Vector2 m_touchMovement;

    int m_minSwipeDistance = 20;

    public void OnSwip()
    {
        if(SwipeEvent != null)
        {
            SwipeEvent(m_touchMovement);
        }
    }

    void OnSwipeEnd()
    {
        if (SwipeEndEvent != null)
        {
            SwipeEndEvent(m_touchMovement);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if(touch.phase == TouchPhase.Began)
            {
                m_touchMovement = Vector2.zero;
            }
            else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                m_touchMovement += touch.deltaPosition;
                
                if (m_touchMovement.magnitude > m_minSwipeDistance)
                {
                    OnSwip();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                OnSwipeEnd();
            }
        }
	}
}
