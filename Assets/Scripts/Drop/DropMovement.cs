using UnityEngine;

public class DropMovement : MonoBehaviour
{
    [SerializeField] private Drop drop;

    private Vector3 start;
    private Vector3 destination;
    private Tile destinationTile;

    #region Swap Variables
    [SerializeField] private float swapTime;
    private float swapTimer;
    private bool isSwapping;
    #endregion

    #region Slide Variables
    private float slideTime;
    private float slideTimer;
    private bool isSliding;
    #endregion

    private SwipeDirection swipeDirection;

    private void Update()
    {
        if (isSwapping)
        {
            MoveTowardsDestination();
        }
        if (isSliding)
        {
            SlideTowardsDestination();
        }
    }

    #region Swap
    public void SetDestination(Tile destinationTile, SwipeDirection swipeDirection)
    {
        //  Set swipe direction
        this.swipeDirection = swipeDirection;
        //  Set destination tile
        this.destinationTile = destinationTile;
        //  Set start and destination positions
        start = transform.position;
        destination = destinationTile.transform.position;
        //  Set isSwapping to true
        isSwapping = true;
    }

    void MoveTowardsDestination()
    {
        swapTimer += Time.deltaTime;

        if (swapTimer < swapTime)
        {
            transform.position = Vector3.Lerp(start, destination, swapTimer / swapTime);
        }

        else
        {
            //  Reset timer
            swapTimer = 0f;
            //  Finalize position
            transform.position = destination;
            //  Set new tile
            drop.SetTile(destinationTile);
            //  Inform tile to fill dropPiece
            destinationTile.SetDropPiece(drop, drop.GetDropType());
            //  Stop swapping
            isSwapping = false;
            //  If there is a match, inform board manager
            if (drop.GetSwipeCheck().CheckMatch(destinationTile, swipeDirection))
            {
                drop.GetGameManager().GetBoardManager().MatchDropsOnGivenTile(destinationTile);
            }
        }
    }
    #endregion

    #region Slide
    public void SetSlideDestination(Tile destinationTile)
    {
        //  Set swipe direction
        this.swipeDirection = SwipeDirection.South;
        //  Set destination tile
        this.destinationTile = destinationTile;
        //  Set slide time
        SetSlideTime();
        //  Set start and destination positions
        start = transform.position;
        destination = destinationTile.transform.position;
        //  Reset self tile
        drop.GetTile().ResetDropPiece();
        drop.SetTile(null);
        //  Set new tile
        drop.SetTile(destinationTile);
        //  Inform tile to fill dropPiece
        destinationTile.SetDropPiece(drop, drop.GetDropType());
        //  Set isSwapping to true
        isSliding = true;
        drop.GetGameManager().GetBoardManager().AllTilesCheckBelow();
    }
    void SetSlideTime()
    {
        int selfTileIndex = int.Parse(drop.GetTile().name);
        int destTileIndex = int.Parse(destinationTile.name);

        float tilesToDestination = (destTileIndex - selfTileIndex) / 8f;

        slideTime = (tilesToDestination * swapTime);
    }
    void SlideTowardsDestination()
    {
        slideTimer += Time.deltaTime;

        if (slideTimer <= slideTime)
        {
            transform.position = Vector3.Lerp(start, destination, slideTimer / slideTime);
        }
        else
        {
            //  Reset timer
            slideTimer = 0f;
            //  Finalize position
            transform.position = destination;
            //  Stop slide
            isSliding = false;
            //  If there is a match, inform board manager
            if (drop.GetSwipeCheck().CheckMatch(destinationTile, swipeDirection))
            {
                drop.GetGameManager().GetBoardManager().MatchDropsOnGivenTile(destinationTile);
            }
            else
            {
                drop.PlayAnim("DropAnim");
            }
        }
    }
    #endregion

}
