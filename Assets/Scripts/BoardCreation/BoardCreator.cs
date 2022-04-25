using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    #region BoardGeneration
    [SerializeField] private Transform tileParent;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Vector3 startPos;
    #endregion
    [SerializeField] private GameManager gameManager;
    private Tile[] tiles;

    public Tile[] InitBoardCreator()
    {
        tiles = new Tile[64];
        //  Create the board using tile
        CreateBoard();
        //SetNeighborHandlers();
        return tiles;
    }


    #region Board-Creation

    void CreateBoard()
    {
        //  Get tile sprite size
        SpriteRenderer sr = tilePrefab.GetComponent<SpriteRenderer>();
        Vector2 tileDistance = new Vector2(sr.bounds.size.x, sr.bounds.size.y);

        for (int column = 0, counter = 0; column < 8; column++)
        {
            for (int row = 0; row < 8; row++, counter++)
            {
                //  Instantiate the tile and set parent
                Tile tile = Instantiate(tilePrefab, tileParent);
                //  Set Game Manager
                tile.SetGameManager(gameManager);
                //  Set position
                tile.transform.localPosition = new Vector3(startPos.x + tileDistance.x * row, startPos.y - tileDistance.y * column);
                //  Name the object
                tile.name = counter.ToString();
                //  Add to tiles array
                tiles[counter] = tile;

                //  Set the neighbors of tile
                #region Set Neighbors
                if (row > 0)
                {
                    tile.SetEastWestNeighbors(tile, tiles[counter - 1]);
                }
                if (column > 0)
                {
                    tile.SetNorthSouthNeighbors(tiles[counter - 8], tile);
                }
                #endregion
            }
        }
    }
    
    #endregion
    
}
