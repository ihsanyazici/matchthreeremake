using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    //  List to hold drop prefabs
    [SerializeField] private List<Drop> dropPrefabs;
    //  List to hold spawned drops
    private List<Drop> drops;
    //  Tiles array
    private Tile[] tiles;

    public void InitializeSpawnManager()
    {
        //  Initialize drops list
        drops = new List<Drop>();
        //  Get tiles from board manager
        tiles = GetComponent<BoardManager>().GetTiles();
        //  Spawn the drops
        SpawnDrops();
    }

    void SpawnDrops()
    {
        //  Spawn 64 drops for 8x8 Board
        for (int i = 0, counter = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++, counter++)
            {
                Drop dropToAdd = Instantiate(ReturnRandomDrop(tiles[counter]));
                //  Add to drops list
                drops.Add(dropToAdd);
                //  Set drop piece of tile
                tiles[counter].SetDropPiece(dropToAdd, dropToAdd.GetDropType());
                //  Set tile of drop
                dropToAdd.SetTile(tiles[counter]);
                //  Set GameManager of Drop
                dropToAdd.SetGameManager(gameManager);
                //  Reset drop local position
                dropToAdd.transform.localPosition = Vector3.zero;
            }
        }
    }

    public void SpawnSingleDrop(Tile tile)
    {
        //Drop dropToAdd = Instantiate(ReturnRandomDrop(tile.GetTileHelper().FindMostSouthernEmptyTile(tile.GetNeighbors().GetSouthNeighbor())));
        Drop dropToAdd = Instantiate(ReturnRandomDrop(tile));
        //  Add to drops list
        drops.Add(dropToAdd);
        //  Set drop piece of tile
        tile.SetDropPiece(dropToAdd, dropToAdd.GetDropType());
        //  Set tile of drop
        dropToAdd.SetTile(tile);
        //  Set GameManager of Drop
        dropToAdd.SetGameManager(gameManager);
        //  Reset drop local position
        dropToAdd.transform.localPosition = Vector3.zero;
    }

    Drop ReturnRandomDrop(Tile targetTile)
    {
        List<Drop> possibleDrops = new List<Drop>(RemoveDropTypeFromList(targetTile,dropPrefabs));

        int randomIndex = Random.Range(0, possibleDrops.Count);
        return possibleDrops[randomIndex];
    }

    List<Drop> RemoveDropTypeFromList(Tile targetTile, List<Drop> dropsToCheck)
    {
        //  copy given list
        List<Drop> possibleDrops = new List<Drop>(dropsToCheck);

        DropType dropType = targetTile.CheckWestNeighborTypes();

        for (int i = 0; i < possibleDrops.Count; i++)
        {
            if (possibleDrops[i].GetDropType() == dropType)
            {
                possibleDrops.Remove(possibleDrops[i]);
            }
        }

        dropType = targetTile.CheckNorthNeighborTypes();

        for (int i = 0; i < possibleDrops.Count; i++)
        {
            if (possibleDrops[i].GetDropType() == dropType)
            {
                possibleDrops.Remove(possibleDrops[i]);
            }
        }
        
        return possibleDrops;
    }

    public void RemoveFromDrops(Drop drop)
    {
        drops.Remove(drop);
    }
}
