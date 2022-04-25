[System.Serializable]
public class DropSwipeCheck
{
    private DropType dropType;

    public DropSwipeCheck(DropType dropType)
    {
        this.dropType = dropType;
    }

    public bool CanSwipeToDestination(Tile destinationTile, SwipeDirection swipeDirection)
    {
        //  If destination tile or its drop is not null and there is a match
        if (destinationTile != null && destinationTile.GetDropPiece().GetDrop() != null && CheckMatch(destinationTile, swipeDirection))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //  Checks whether swipe action is permitted to destination tile
    public bool CheckMatch(Tile destinationTile, SwipeDirection swipeDirection)
    {
        //  Check count of adjacent same type drops in column
        int columnCount = destinationTile.GetTileHelper().CheckConsecutiveColumns(dropType,swipeDirection);
        //  Check count of adjacent same type drops in row
        int rowCount = destinationTile.GetTileHelper().CheckConsecutiveRows(dropType, swipeDirection);

        Drop tmp = destinationTile.GetDropPiece().GetDrop();
        //  If there are more than or equal to 2 adjacent same drops, there is a match!!
        if (tmp != null && columnCount >= 2 || rowCount >= 2)
        {
            return true;
        }

        return false;
    }
}
