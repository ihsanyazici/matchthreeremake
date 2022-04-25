using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Swipe swipe;
    private Drop selectedDrop;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        swipe = GetComponent<Swipe>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRayToObject();
        }

        if (swipe.IsSwipeOccured())
        {
            CheckSwipe();
        }
    }

    void CheckSwipe()
    {
        if (selectedDrop != null)
        {
            //  Get Swipe Direction
            SwipeDirection swipeDirection = GetSwipeDirection();
            //  Get tile of selected drop
            Tile selectedDropTile = selectedDrop.GetTile();
            //  Get the destination tile
            Tile destinationTile = selectedDropTile.GetNeighbors().GetDirectionNeighbor(swipeDirection);

            //  Check for a valid swipe and a valid neighbor
            if (swipeDirection != SwipeDirection.Null && destinationTile != null)
            {
                if (selectedDrop.GetSwipeCheck().CanSwipeToDestination(destinationTile,swipeDirection))
                {
                    //  Set destination of the drop
                    selectedDrop.GetDropMovement().SetDestination(destinationTile, swipeDirection);
                    //  Set destination of the drop to swap with                   
                    destinationTile.GetDropPiece().GetDrop().GetDropMovement().SetDestination(selectedDropTile, swipeDirection);
                }
            }
            ResetSelectedDrop();
        }
    }
    //  Casts ray under mouse position and selects a drop if ray hits a drop collider
    void CastRayToObject()
    {
        //  Get Mouse Position
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);

        RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(mousePos), Vector2.zero);

        
        if (hit.collider != null && hit.collider.GetComponent<Drop>() != null)
        {
            Drop tmp = hit.collider.GetComponent<Drop>();
            selectedDrop = tmp;
        }
        
    }
    void ResetSelectedDrop()
    {
        selectedDrop = null;
    }

    //  Returns the swipe direction
    SwipeDirection GetSwipeDirection()
    {
        if (swipe.SwipeUp)
        {
            return SwipeDirection.North;
        }
        else if (swipe.SwipeDown)
        {
            return SwipeDirection.South;
        }
        else if (swipe.SwipeLeft)
        {
            return SwipeDirection.West;
        }
        else if (swipe.SwipeRight)
        {
            return SwipeDirection.East;
        }
        else
        {
            return SwipeDirection.Null;
        }
    }
}
