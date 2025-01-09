public struct InfoAboutAmountObjects
{
    public int AmountActive { get; private set; }

    public int AmountCreated { get; private set; }

    public int AmountSpawned { get; private set; }

    public void SetInfo(int active, int created, int spawned)
    {
        AmountActive = active;
        AmountCreated = created;
        AmountSpawned = spawned;
    }
}