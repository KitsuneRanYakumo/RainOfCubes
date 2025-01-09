using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Spawnable
{
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _forceExplosion;

    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);
        StartCoroutine(StartCountdownLifeTime());
    }

    protected override IEnumerator StartCountdownLifeTime()
    {
        float disappearingStep = Renderer.material.color.a / LifeTime;

        while (LifeTime > 0)
        {
            LifeTime -= Time.deltaTime;
            DecreaseAlphaChannelColor(disappearingStep);
            yield return null;
        }

        Explode();
        OnLifeTimeFinished();
    }

    private void DecreaseAlphaChannelColor(float disappearingStep)
    {
        Color color = Renderer.material.color;
        color.a -= disappearingStep * Time.deltaTime;
        Renderer.material.color = color;
    }

    private void Explode()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
            explodableObject.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radiusExplosion);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                rigidbodies.Add(hit.attachedRigidbody);

        return rigidbodies;
    }
}