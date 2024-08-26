using UnityEngine;

public class Platform : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Cube cube) == false)
            return;

        if (cube.IsTouchPlatform == false)
            cube.ChangeColor();
    }
}