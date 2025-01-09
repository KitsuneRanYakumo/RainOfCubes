using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private Spawnable _prefabSpawnable;
    [SerializeField, Min(1)] private int _poolCapacity = 1;
    [SerializeField, Min(1)] private int _poolMaxSize = 1;

    protected ObjectPool<Spawnable> PoolSpawnables;
    protected int AmountActive;
    protected int AmountCreated;
    protected int AmountSpawned;

    private InfoAboutAmountObjects _infoAboutAmountObjects;

    public event Action<InfoAboutAmountObjects> InfoAboutAmountChanged;

    public int PoolMaxSize => _poolMaxSize;

    private void Initialize()
    {
        AmountActive = 0;
        AmountCreated = 0;
        AmountSpawned = 0;
    }

    private void OnValidate()
    {
        if (_poolCapacity > _poolMaxSize)
        {
            _poolCapacity = _poolMaxSize;
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
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        Initialize();
    }

    protected void RecordInfoAboutAmount()
    {
        _infoAboutAmountObjects.SetInfo(AmountActive, AmountCreated, AmountSpawned);
    }

    protected void OnInfoAboutAmountChanged()
    {
        InfoAboutAmountChanged?.Invoke(_infoAboutAmountObjects);
    }

    protected virtual void TakeSpawnable(Spawnable spawnable)
    {
        PoolSpawnables.Release(spawnable);
    }

    protected abstract void OnGetFromPool(Spawnable spawnable);

    private Spawnable CreateSpawnableForPool()
    {
        AmountCreated++;
        RecordInfoAboutAmount();
        OnInfoAboutAmountChanged();
        return Instantiate(_prefabSpawnable);
    }

    private void OnReleaseInPool(Spawnable spawnable)
    {
        AmountActive--;
        RecordInfoAboutAmount();
        OnInfoAboutAmountChanged();
        spawnable.gameObject.SetActive(false);
        spawnable.LifeTimeFinished -= TakeSpawnable;
    }

    private void OnDestroyForPool(Spawnable spawnable)
    {
        Destroy(spawnable.gameObject);
    }
}