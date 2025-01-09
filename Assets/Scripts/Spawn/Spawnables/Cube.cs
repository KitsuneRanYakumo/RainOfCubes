using System.Collections;
using UnityEngine;

public class Cube : Spawnable
{
    private bool _isTouchPlatform;

    public override void Initialize(Vector3 position)
    {
        base.Initialize(position);
        _isTouchPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouchPlatform == false && collision.gameObject.GetComponent<Platform>())
        {
            ChangeColor();
            StartCoroutine(StartCountdownLifeTime());
        }
    }

    protected override IEnumerator StartCountdownLifeTime()
    {
        yield return Wait;
        OnLifeTimeFinished();
    }

    private void ChangeColor()
    {
        _isTouchPlatform = true;
        Renderer.material.color = new Color(Random.value, Random.value, Random.value);
    }
}