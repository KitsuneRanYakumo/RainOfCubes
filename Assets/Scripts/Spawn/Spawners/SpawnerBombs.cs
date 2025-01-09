using UnityEngine;

public class SpawnerBombs : Spawner
{
    [SerializeField] private SpawnerCubes _spawner;

    private Vector3 _pointSpawn;

    private void OnEnable()
    {
        _spawner.CubeTaken += SpawnBomb;
    }

    private void OnDisable()
    {
        _spawner.CubeTaken -= SpawnBomb;
    }

    protected override void OnGetFromPool(Spawnable spawnable)
    {
        AmountActive++;
        AmountSpawned++;
        RecordInfoAboutAmount();
        OnInfoAboutAmountChanged();
        spawnable.gameObject.SetActive(true);
        spawnable.Initialize(_pointSpawn);
        spawnable.LifeTimeFinished += TakeSpawnable;
    }

    private void SpawnBomb(Vector3 position)
    {
        _pointSpawn = position;
        PoolSpawnables.Get();
    }
}