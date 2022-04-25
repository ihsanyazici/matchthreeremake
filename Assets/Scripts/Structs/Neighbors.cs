[System.Serializable]
public struct Neighbors
{
    public Tile east;
    public Tile west;
    public Tile north;
    public Tile south;

    //  Getters
    public Tile GetEastNeighbor()
    {
        return east;
    }
    public Tile GetWestNeighbor()
    {
        return west;
    }
    public Tile GetNorthNeighbor()
    {
        return north;
    }
    public Tile GetSouthNeighbor()
    {
        return south;
    }

    public Tile GetDirectionNeighbor(SwipeDirection swipeDirection)
    {
        switch (swipeDirection)
        {
            case SwipeDirection.West:
                return west;
            case SwipeDirection.East:
                return east;
            case SwipeDirection.North:
                return north;
            case SwipeDirection.South:
                return south;
            case SwipeDirection.Null:
                return null;
            default:
                return null;
        }
    }
    //  Setters
    public void SetEastNeighbor(Tile east)
    {      
           this.east = east;
    }      
    public void SetWestNeighbor(Tile west)
    {      
          this.west = west;
    }      
    public void SetNorthNeighbor(Tile north)
    {      
           this.north = north;
    }      
    public void SetSouthNeighbor(Tile south)
    {
        this.south = south;
    }


    
}