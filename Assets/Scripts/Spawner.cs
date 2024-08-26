using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabCube;
    [SerializeField, Min(-4)] private float _minForPointSpawn = -4;
    [SerializeField, Range(-4, 4)] private float _maxForPointSpawn = 4;
    [SerializeField, Min(1)] private int _poolCapacity;
    [SerializeField, Min(1)] private int _poolMaxSize;

    private ObjectPool<GameObject> _poolCubes;

    private void Awake()
    {
        _poolCubes = new ObjectPool<GameObject>(
            createFunc: () => CreateFunc(),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => ActionOnRelease(cube),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
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

    public void TakeCube(GameObject cube)
    {
        _poolCubes.Release(cube);
    }

    private GameObject CreateFunc()
    {
        GameObject cube = Instantiate(_prefabCube);
        cube.GetComponent<Cube>().SetSpawner(gameObject.GetComponent<Spawner>());
        return cube;
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = GetPointSpawn();
        cube.SetActive(true);
    }

    private void ActionOnRelease(GameObject cube)
    {
        cube.GetComponent<Cube>().Reset();
        cube.transform.rotation = Quaternion.identity;
        cube.SetActive(false);
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