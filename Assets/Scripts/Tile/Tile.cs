using UnityEngine;

public class Tile : MonoBehaviour
{
    private GameManager gameManager;
    private Neighbors neighbors;
    private DropPiece dropPiece;
    private TileHelper tileHelper;

    [SerializeField] private bool isSpawner;

    private void Awake()
    {
        tileHelper = new TileHelper(this);   
    }

    #region SetNeighbors
    public void SetEastWestNeighbors(Tile east, Tile west)
    {
        west.neighbors.SetEastNeighbor(east);
        east.neighbors.SetWestNeighbor(west);
    }
    public void SetNorthSouthNeighbors(Tile north, Tile south)
    {
        north.neighbors.SetSouthNeighbor(south);
        south.neighbors.SetNorthNeighbor(north);
    }
    #endregion

    #region CheckWestNeighbors
    public DropType CheckWestNeighborTypes()
    {
        //  If tile has west and west of west neighbors
        if (neighbors.GetWestNeighbor() != null && neighbors.GetWestNeighbor().GetNeighbors().GetWestNeighbor())
        {
            Tile westNeighbor = neighbors.GetWestNeighbor();
            Tile westOfWestNeighbor = westNeighbor.GetNeighbors().GetWestNeighbor();

            if (westNeighbor.GetDropPiece().GetDropType() == westOfWestNeighbor.GetDropPiece().GetDropType())
            {
                return westNeighbor.GetDropPiece().GetDropType();
            }
        }

        return DropType.None;
    }
    #endregion

    #region CheckNorthNeighbors
    public DropType CheckNorthNeighborTypes()
    {
        //  If tile has north and north of north neighbors
        if (neighbors.GetNorthNeighbor() != null && neighbors.GetNorthNeighbor().GetNeighbors().GetNorthNeighbor())
        {
            Tile northNeighbor = neighbors.GetNorthNeighbor();
            Tile northOfNorthNeighbor = northNeighbor.GetNeighbors().GetNorthNeighbor();

            if (northNeighbor.GetDropPiece().GetDropType() == northOfNorthNeighbor.GetDropPiece().GetDropType())
            {
                return northNeighbor.GetDropPiece().GetDropType();
            }
        }      

        return DropType.None;
    }
    #endregion

    //  Getters & Setters
    public Neighbors GetNeighbors()
    {
        return neighbors;
    }
    public DropPiece GetDropPiece()
    {
        return dropPiece;
    }
    public void SetDropPiece(Drop drop,DropType dropType)
    {
        dropPiece.SetDropPiece(drop,dropType);
        //  Parent the drop
        drop.transform.parent = transform;
    }
    public void ResetDropPiece()
    {
        dropPiece.ResetDropPiece();

        if (isSpawner)
        {
            gameManager.GetSpawnManager().SpawnSingleDrop(this);
        }
    }

    public bool IsSpawner()
    {
        return isSpawner;
    }

    public TileHelper GetTileHelper()
    {
        return tileHelper;
    }
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
