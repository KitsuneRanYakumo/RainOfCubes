using UnityEngine;

public class AroundRotater : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        RotateAround();
    }

    private void RotateAround()
    {
        _camera.transform.RotateAround(_target.transform.position, Vector3.up, _rotateSpeed);
    }
}