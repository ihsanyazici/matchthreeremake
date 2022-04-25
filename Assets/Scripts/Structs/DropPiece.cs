[System.Serializable]
public struct DropPiece
{
    public Drop drop;
    public DropType dropType;

    public void SetDropPiece(Drop drop,DropType dropType)
    {
        this.drop = drop;
        this.dropType = dropType;
    }
    public Drop GetDrop()
    {
        return drop;
    }
    public DropType GetDropType()
    {
        return dropType;
    }
    public void ResetDropPiece()
    {
        drop = null;
        dropType = DropType.None;
    }
}
