using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCubes : Spawner
{
    [SerializeField] private SpawnerBombs _spawnerBombs;
    [SerializeField, Min(0.1f)] private float _spawnRadius = 1;

    private void Update()
    {
        if (PoolSpawnables.CountActive < PoolMaxSize)
        {
            GeneratePointSpawn();
            PoolSpawnables.Get();
        }
    }

    protected override void TakeSpawnable(Spawnable spawnable)
    {
        base.TakeSpawnable(spawnable);
        _spawnerBombs.SpawnBomb(spawnable.transform.position);
    }

    private void GeneratePointSpawn()
    {
        Vector2 point = Random.insideUnitCircle * _spawnRadius;

        float positionX = transform.position.x + point.x;
        float positionY = transform.position.y;
        float positionZ = transform.position.z + point.y;

        SpawnPosition = new Vector3(positionX, positionY, positionZ);
    }
}