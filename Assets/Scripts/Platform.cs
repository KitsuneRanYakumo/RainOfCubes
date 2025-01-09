using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Platform : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        GenerateColor();
    }

    private void GenerateColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }
}