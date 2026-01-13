using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private Vector2 startTouch;
    private Vector2 swipeDelta;
    private bool isSwiping;

    public float minSwipeDistance = 50f;

    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeUp { get; private set; }
    public bool SwipeDown { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        ResetSwipes();

#if UNITY_EDITOR || UNITY_STANDALONE
        SwipeLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.DownArrow);
#endif

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isSwiping = true;
                startTouch = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                swipeDelta = touch.position - startTouch;

                if (swipeDelta.magnitude > minSwipeDistance)
                {
                    float x = swipeDelta.x;
                    float y = swipeDelta.y;

                    if (Mathf.Abs(x) > Mathf.Abs(y))
                    {
                        if (x < 0) SwipeLeft = true;
                        else SwipeRight = true;
                    }
                    else
                    {
                        if (y < 0) SwipeDown = true;
                        else SwipeUp = true;
                    }

                    isSwiping = false;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isSwiping = false;
            }
        }
    }

    void ResetSwipes()
    {
        SwipeLeft = SwipeRight = SwipeUp = SwipeDown = false;
    }
}
