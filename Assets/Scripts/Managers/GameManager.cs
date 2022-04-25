using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BoardManager boardManager;
    private SpawnManager spawnManager;

    private bool isGameStarted;

    private void Awake()
    {
        boardManager = GetComponent<BoardManager>();
        spawnManager = GetComponent<SpawnManager>();
    }

    private void Start()
    {
        boardManager.InitializeBoardManager();
        spawnManager.InitializeSpawnManager();
        isGameStarted = true;
    }

    public BoardManager GetBoardManager()
    {
        return boardManager;
    }
    public SpawnManager GetSpawnManager()
    { 
        return spawnManager;
    }

    public bool IsGameStarted()
    { 
        return isGameStarted;
    }
}
