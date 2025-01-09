using UnityEngine;

public class SpawnerBombs : Spawner
{
    public void SpawnBomb(Vector3 position)
    {
        SpawnPosition = position;
        PoolSpawnables.Get();
    }
}