using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerCubes : Spawner
{
    [SerializeField, Min(-9)] private float _minForPointSpawn = -9;
    [SerializeField, Range(-9, 9)] private float _maxForPointSpawn = 9;

    public event Action<Vector3> CubeTaken;

    private void OnValidate()
    {
        if (_minForPointSpawn > _maxForPointSpawn)
        {
            _minForPointSpawn = _maxForPointSpawn;
        }
    }

    private void Update()
    {
        if (PoolSpawnables.CountActive < PoolMaxSize)
        {
            PoolSpawnables.Get();
        }
    }

    protected override void OnGetFromPool(Spawnable spawnable)
    {
        AmountActive++;
        AmountSpawned++;
        RecordInfoAboutAmount();
        OnInfoAboutAmountChanged();
        spawnable.gameObject.SetActive(true);
        spawnable.Initialize(GeneratePointSpawn());
        spawnable.LifeTimeFinished += TakeSpawnable;
    }

    protected override void TakeSpawnable(Spawnable spawnable)
    {
        base.TakeSpawnable(spawnable);
        CubeTaken?.Invoke(spawnable.transform.position);
    }

    private Vector3 GeneratePointSpawn()
    {
        Vector3 positionSpawner = transform.position;

        float minX = positionSpawner.x + _minForPointSpawn;
        float maxX = positionSpawner.x + _maxForPointSpawn;

        float minZ = positionSpawner.z + _minForPointSpawn;
        float maxZ = positionSpawner.z + _maxForPointSpawn;

        float positionX = Random.Range(minX, maxX);
        float positionY = transform.position.y;
        float positionZ = Random.Range(minZ, maxZ);

        return new Vector3(positionX, positionY, positionZ);
    }
}