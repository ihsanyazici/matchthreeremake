using System.Collections.Generic;

[System.Serializable]
public class TileHelper
{
    //  This class is responsible for handling neighbor relations
    private Tile tile;

    private List<Tile> matchedRow;
    private List<Tile> matchedColumn;

    public TileHelper(Tile tile)
    {
        this.tile = tile;
    }


    #region Recursively Count Neighbors
    public int CheckConsecutiveRows(DropType dropType,SwipeDirection swipeDirection)
    {
        matchedRow = new List<Tile>();
        matchedRow.Clear();

        //  Get the first neighbor in direction
        Tile directionNeighbor = tile.GetNeighbors().GetDirectionNeighbor(swipeDirection);
        int total = 0;

        //  If swiped in row direction
        if (swipeDirection == SwipeDirection.West || swipeDirection == SwipeDirection.East)
        {
            if (directionNeighbor != null && directionNeighbor.GetDropPiece().GetDropType() == dropType)
            {
                total += CountNeighborsInDirection(0,directionNeighbor,swipeDirection, dropType);
            }
        }
        //  Else check both sides
        else
        {
            Tile westNeighbor = tile.GetNeighbors().GetDirectionNeighbor(SwipeDirection.West);
            Tile eastNeighbor = tile.GetNeighbors().GetDirectionNeighbor(SwipeDirection.East);

            if (westNeighbor != null && westNeighbor.GetDropPiece().GetDropType() == dropType)
            {
                total += CountNeighborsInDirection(0, westNeighbor, SwipeDirection.West, dropType);
            }
            if (eastNeighbor != null && eastNeighbor.GetDropPiece().GetDropType() == dropType)
            {
                total += CountNeighborsInDirection(0, eastNeighbor, SwipeDirection.East, dropType);
            }
        }
        return total;
    }

    public int CheckConsecutiveColumns(DropType dropType,SwipeDirection swipeDirection)
    {
        matchedColumn = new List<Tile>();
        matchedColumn.Clear();

        //  Get the first neighbor in direction
        Tile directionNeighbor = tile.GetNeighbors().GetDirectionNeighbor(swipeDirection);
        int total = 0;

        //  If swiped in column direction
        if (swipeDirection == SwipeDirection.North || swipeDirection == SwipeDirection.South)
        {
            if (directionNeighbor != null && directionNeighbor.GetDropPiece().GetDropType() == dropType)
            {
                total += CountNeighborsInDirection(0, directionNeighbor, swipeDirection, dropType);
            }
        }
        //  Else check both sides
        else
        {
            Tile northNeighbor = tile.GetNeighbors().GetDirectionNeighbor(SwipeDirection.North);
            Tile southNeighbor = tile.GetNeighbors().GetDirectionNeighbor(SwipeDirection.South);

            if (northNeighbor != null && northNeighbor.GetDropPiece().GetDropType() == dropType)
            {
                total += CountNeighborsInDirection(0, northNeighbor, SwipeDirection.North, dropType);
            }
            if (southNeighbor != null && southNeighbor.GetDropPiece().GetDropType() == dropType)
            {
                total += CountNeighborsInDirection(0, southNeighbor, SwipeDirection.South, dropType);
            }
        }
        return total;
    }

    private int CountNeighborsInDirection(int adjacentCount,Tile directionNeighbor,SwipeDirection swipeDirection,DropType dropType)
    {
        //  Base Case
        if (directionNeighbor == null || directionNeighbor.GetDropPiece().GetDropType() != dropType)
        {
            //  If drop piece types do not match or neighbor is null, stop and return yourself
            return adjacentCount;
        }

        if (swipeDirection == SwipeDirection.West || swipeDirection == SwipeDirection.East)
        {
            if (!directionNeighbor.GetDropPiece().GetDrop().GetDropMovement().IsInAction())
            {
                matchedRow.Add(directionNeighbor);
            }
        }
        else if (swipeDirection == SwipeDirection.North || swipeDirection == SwipeDirection.South)
        {
            if (!directionNeighbor.GetDropPiece().GetDrop().GetDropMovement().IsInAction())
            {
                matchedColumn.Add(directionNeighbor);
            }
        }

        //  Increment count
        adjacentCount++;
        //  Get next neighbor in direction
        Tile nextNeighbor = directionNeighbor.GetNeighbors().GetDirectionNeighbor(swipeDirection);
        return CountNeighborsInDirection(adjacentCount, nextNeighbor, swipeDirection,dropType);
    }
    #endregion

    //  Getters & Setters
    public List<Drop> GetMatchedDropsList()
    {
        List<Drop> matchedDrops = new List<Drop>();

        if (matchedRow.Count >= 2)
        {
            for (int i = 0; i < matchedRow.Count; i++)
            {
                matchedDrops.Add(matchedRow[i].GetDropPiece().GetDrop());
            }
        }
        if (matchedColumn.Count >= 2)
        {
            for (int i = 0; i < matchedColumn.Count; i++)
            {
                matchedDrops.Add(matchedColumn[i].GetDropPiece().GetDrop());
            }
        }

        if (matchedRow.Count >= 2 || matchedColumn.Count >= 2)
        {
            //  Also add self drop
            matchedDrops.Add(tile.GetDropPiece().GetDrop());
        }

        //  Clear matched lists
        matchedColumn.Clear();
        matchedRow.Clear();
        
        return matchedDrops;
    }

    //  Returns empty southern tile
    public Tile FindMostSouthernEmptyTile(Tile south)
    {
        //  Base Case

        //  If south neighbor of south is not null and doesn't contain a drop
        if (south.GetNeighbors().GetSouthNeighbor() == null || south.GetNeighbors().GetSouthNeighbor().GetDropPiece().GetDropType() != DropType.None)
        {
            return south;
        }

        return FindMostSouthernEmptyTile(south.GetNeighbors().GetSouthNeighbor());
    }

}
