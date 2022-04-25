using UnityEngine;

public class Drop : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private DropType dropType;
    [SerializeField] private Animator anim;

    [SerializeField] private DropMovement dropMovement;
    private DropSwipeCheck swipeCheck;

    private Tile selfTile;
    private bool isMatch;

    private void Start()
    {
        swipeCheck = new DropSwipeCheck(dropType);
    }
    public void StartMatch()
    {
        //  Add yourself to matching drops
        if (!isMatch)
        {
            //  Play Match Anim
            PlayAnim("MatchAnim");
            gameManager.GetBoardManager().AddToMatchingDrops(this);
        }

        isMatch = true;
    }

    //  Match anim will call this function
    public void MatchDrop()
    {
        //  Empty Self Tile's Drop Piece
        selfTile.ResetDropPiece();
        //  Empty Self Tile
        selfTile = null;
        //  Remove yourself from parent
        transform.parent = null;
        //  Go back to idle
        PlayAnim("Idle");
        //  Disable gameobject
        gameObject.SetActive(false);
        //  Remove yourself from drops
        gameManager.GetSpawnManager().RemoveFromDrops(this);
        transform.position = Vector3.up * 20f;
        //  Inform board manager to check below of all tiles
        gameManager.GetBoardManager().RemoveFromMatchingDrops(this);
        isMatch = false;
        Destroy(gameObject);
    }

    public void PlayAnim(string animName)
    {
        anim.CrossFadeInFixedTime(animName, 0f);
    }

    //  Getters & Setters
    public Tile GetTile()
    {
        return selfTile;
    }
    public void SetTile(Tile tile)
    {
        selfTile = tile;
    }
    public DropType GetDropType()
    {
        return dropType;
    }
    public bool IsMatch()
    {
        return isMatch;
    }
    public void SetMatch()
    {
        isMatch = true;
    }
    public DropSwipeCheck GetSwipeCheck()
    {
        return swipeCheck;
    }
    public DropMovement GetDropMovement()
    {
        return dropMovement;
    }
    public void SetGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public GameManager GetGameManager()
    {
        return gameManager;
    }
}
