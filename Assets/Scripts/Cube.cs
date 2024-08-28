using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Color _inputColor;
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private bool _isTouchPlatform;
    private int _lifeTime;
    private WaitForSecondsRealtime _wait;

    public event System.Action<Cube> LifeTimeFinished;

    private void Awake()
    {
        SetComponents();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouchPlatform == false && collision.gameObject.GetComponent<Platform>())
        {
            ChangeColor();
            StartCoroutine(CountdownLifeTime());
        }
    }

    public void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        _renderer.material.color = _inputColor;
        _rigidbody.velocity = Vector3.zero;
        _isTouchPlatform = false;
        _lifeTime = Random.Range(_minLifetime, _maxLifetime + 1);
        _wait = new WaitForSecondsRealtime(_lifeTime);
    }

    private void SetComponents()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void ChangeColor()
    {
        _isTouchPlatform = true;
        _renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private IEnumerator CountdownLifeTime()
    {
        yield return _wait;

        LifeTimeFinished?.Invoke(this);
    }
}