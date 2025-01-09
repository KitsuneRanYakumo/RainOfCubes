using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField, Min(1)] protected int PoolMaxSize = 1;

    [SerializeField, Min(1)] private int _poolCapacity = 1;
    [SerializeField] private Spawnable _prefabSpawnable;

    protected ObjectPool<Spawnable> PoolSpawnables;
    protected Vector3 SpawnPosition;

    private InfoAboutAmountObjects _infoAboutAmountObjects;

    public event Action<InfoAboutAmountObjects> InfoAboutAmountChanged;

    private void Initialize()
    {
        _infoAboutAmountObjects.SetInfo(0, 0);
    }

    private void OnValidate()
    {
        if (_poolCapacity > PoolMaxSize)
        {
            _poolCapacity = PoolMaxSize;
        }
    }

    private void Awake()
    {
        PoolSpawnables = new ObjectPool<Spawnable>(
            createFunc: CreateSpawnableForPool,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseInPool,
            actionOnDestroy: OnDestroyForPool,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: PoolMaxSize);
    }

    private void Start()
    {
        Initialize();
    }

    protected virtual void TakeSpawnable(Spawnable spawnable)
    {
        PoolSpawnables.Release(spawnable);
    }

    private Spawnable CreateSpawnableForPool()
    {
        _infoAboutAmountObjects.SetInfo(PoolSpawnables.CountActive, PoolSpawnables.CountAll);
        InfoAboutAmountChanged?.Invoke(_infoAboutAmountObjects);
        return Instantiate(_prefabSpawnable);
    }

    private void OnGetFromPool(Spawnable spawnable)
    {
        _infoAboutAmountObjects.IncreaseAmountSpawned();
        _infoAboutAmountObjects.SetInfo(PoolSpawnables.CountActive, PoolSpawnables.CountAll);
        InfoAboutAmountChanged?.Invoke(_infoAboutAmountObjects);
        spawnable.gameObject.SetActive(true);
        spawnable.Initialize(SpawnPosition);
        spawnable.LifeTimeFinished += TakeSpawnable;
    }

    private void OnReleaseInPool(Spawnable spawnable)
    {
        _infoAboutAmountObjects.SetInfo(PoolSpawnables.CountActive, PoolSpawnables.CountAll);
        InfoAboutAmountChanged?.Invoke(_infoAboutAmountObjects);
        spawnable.gameObject.SetActive(false);
        spawnable.LifeTimeFinished -= TakeSpawnable;
    }

    private void OnDestroyForPool(Spawnable spawnable)
    {
        Destroy(spawnable.gameObject);
    }
}