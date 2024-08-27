using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField, Min(-4)] private float _minForPointSpawn = -4;
    [SerializeField, Range(-4, 4)] private float _maxForPointSpawn = 4;
    [SerializeField, Min(1)] private int _poolCapacity;
    [SerializeField, Min(1)] private int _poolMaxSize;

    private ObjectPool<Cube> _poolCubes;

    private void Awake()
    {
        _poolCubes = new ObjectPool<Cube>(
            createFunc: CreateCubeForPool,
            actionOnGet: GetFromPool,
            actionOnRelease: ReleaseInPool,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Update()
    {
        if (_poolCubes.CountActive < _poolMaxSize)
        {
            _poolCubes.Get();
        }
    }

    private void OnValidate()
    {
        if (_minForPointSpawn > _maxForPointSpawn)
        {
            _minForPointSpawn = _maxForPointSpawn;
        }

        if (_poolCapacity > _poolMaxSize)
        {
            _poolCapacity = _poolMaxSize;
        }
    }

    public void TakeCube(Cube cube)
    {
        _poolCubes.Release(cube);
    }

    private Cube CreateCubeForPool()
    {
        Cube cube = Instantiate(_prefabCube);
        cube.SetSpawner(this);
        return cube;
    }

    private void GetFromPool(Cube cube)
    {
        cube.transform.position = GetPointSpawn();
        cube.gameObject.SetActive(true);
    }

    private void ReleaseInPool(Cube cube)
    {
        cube.Reset();
        cube.gameObject.SetActive(false);
    }

    private Vector3 GetPointSpawn()
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