using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public abstract class Spawnable : MonoBehaviour
{
    [SerializeField] private int _minLifetime = 2;
    [SerializeField] private int _maxLifetime = 5;
    [SerializeField] private Color _colorModel;

    protected Renderer Renderer;
    protected float LifeTime;
    protected WaitForSecondsRealtime Wait;

    private Rigidbody _rigidbody;

    public event System.Action<Spawnable> LifeTimeFinished;

    public virtual void Initialize(Vector3 position)
    {
        transform.position = position;
        transform.rotation = Quaternion.identity;
        Renderer.material.color = _colorModel;
        LifeTime = Random.Range(_minLifetime, _maxLifetime + 1);
        _rigidbody.velocity = Vector3.zero;
        Wait = new WaitForSecondsRealtime(LifeTime);
    }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected abstract IEnumerator StartCountdownLifeTime();

    protected void OnLifeTimeFinished()
    {
        LifeTimeFinished?.Invoke(this);
    }
}