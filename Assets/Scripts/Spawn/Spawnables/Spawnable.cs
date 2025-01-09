using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public abstract class Spawnable : MonoBehaviour
{
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;
    [SerializeField] private Color _inputColor;

    protected Renderer Renderer;
    protected float LifeTime;

    private Rigidbody _rigidbody;
    private WaitForSecondsRealtime _wait;

    public event System.Action<Spawnable> LifeTimeFinished;

    public virtual void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        Renderer.material.color = _inputColor;
        LifeTime = Random.Range(_minLifetime, _maxLifetime + 1);
        _rigidbody.velocity = Vector3.zero;
        _wait = new WaitForSecondsRealtime(LifeTime);
    }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected virtual IEnumerator StartCountdownLifeTime()
    {
        yield return _wait;
        LifeTimeFinished?.Invoke(this);
    }

    protected void OnLifeTimeFinished()
    {
        LifeTimeFinished?.Invoke(this);
    }
}