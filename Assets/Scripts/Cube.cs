using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _inputColor;
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private Renderer _renderer;
    private bool _isTouchPlatform;
    private int _lifeTime;
    private WaitForSecondsRealtime _wait;
    private Spawner _spawnerWithPool;

    public bool IsTouchPlatform => _isTouchPlatform;

    public void Reset()
    {
        Start();
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
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
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Initialize()
    {
        gameObject.transform.rotation = Quaternion.identity;
        _renderer.material.color = _inputColor;
        _isTouchPlatform = false;
        _lifeTime = Random.Range(_minLifetime, _maxLifetime + 1);
        _wait = new WaitForSecondsRealtime(_lifeTime);
    }

    private IEnumerator CountdownLifeTime()
    {
        yield return _wait;

        _spawnerWithPool.TakeCube(this);
    }
}