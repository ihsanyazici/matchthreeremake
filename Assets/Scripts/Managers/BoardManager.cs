using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BoardCreator boardCreator;
    private Tile[] tiles;
    private List<Drop> matchingDrops;

    public void InitializeBoardManager()
    {
        matchingDrops = new List<Drop>();
        tiles = boardCreator.InitBoardCreator();
    }

    public void MatchDropsOnGivenTile(Tile tile)
    {
        List<Drop> matchedDrops = new List<Drop>(tile.GetTileHelper().GetMatchedDropsList());

        if (matchedDrops.Count >= 3)
        {
            for (int i = 0; i < matchedDrops.Count; i++)
            {
                matchedDrops[i].StartMatch();
            }
        }
        
    }

    public void AllTilesCheckBelow()
    {

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].GetNeighbors().GetSouthNeighbor() != null && tiles[i].GetNeighbors().GetSouthNeighbor().GetDropPiece().GetDropType() == DropType.None)
            {
                Tile destinationTile = tiles[i].GetTileHelper().FindMostSouthernEmptyTile(tiles[i].GetNeighbors().GetSouthNeighbor());
                Drop dropInTile = tiles[i].GetDropPiece().GetDrop();

                if (dropInTile != null)
                {
                    dropInTile.GetDropMovement().SetSlideDestination(destinationTile);
                }
            }
        }
    }

    public void AddToMatchingDrops(Drop drop)
    {
        matchingDrops.Add(drop);
    }
    public void RemoveFromMatchingDrops(Drop drop)
    {
        matchingDrops.Remove(drop);

        if (matchingDrops.Count == 0)
        {
            AllTilesCheckBelow();
        }
    }
    //  Getters & Setters
    public Tile[] GetTiles()
    {
        return tiles;
    }
}
