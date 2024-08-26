using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private Spawner _spawnerWithPool;
    private bool _isTouchPlatform;
    private int _lifeTime;
    private WaitForSecondsRealtime _wait;

    public bool IsTouchPlatform => _isTouchPlatform;

    public void Reset()
    {
        Start();
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (_isTouchPlatform)
        {
            StartCoroutine(CountdownLifeTime());
        }
    }

    public void SetSpawner(Spawner spawner)
    {
        _spawnerWithPool = spawner;
    }

    public void ChangeColor()
    {
        _isTouchPlatform = true;
        GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Initialize()
    {
        GetComponent<Renderer>().material.color = _color;
        _isTouchPlatform = false;
        _lifeTime = Random.Range(_minLifetime, _maxLifetime + 1);
        _wait = new WaitForSecondsRealtime(_lifeTime);
    }

    private IEnumerator CountdownLifeTime()
    {
        yield return _wait;

        _spawnerWithPool.TakeCube(gameObject);
    }
}