using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Pool objectPool;
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
    public Pool GetObjectPool()
    {
        return objectPool;
    }

    public bool IsGameStarted()
    { 
        return isGameStarted;
    }
}
