using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;
    [SerializeField] private float swipeSensitivity;

    private void Update()
    {
        swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion

        //  Calculate the distance (is it outside or inside dead zone)
        swipeDelta = Vector2.zero;

        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // Did we crass the deadzone?
        if (swipeDelta.magnitude > swipeSensitivity)
        {
            //  Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            //  We are in x-axis
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //  Left or Right
                if (x < 0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }

            else
            {
                // Up or Down
                if (y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }
            Reset();
        }
    }

    public bool IsSwipeOccured()
    {
        if (swipeLeft || swipeRight || swipeUp || swipeDown)
        {
            return true;
        }
        return false;
    }
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    #region Getters and Setters
    public Vector2 SwipeDelta { get { return SwipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    #endregion


}
