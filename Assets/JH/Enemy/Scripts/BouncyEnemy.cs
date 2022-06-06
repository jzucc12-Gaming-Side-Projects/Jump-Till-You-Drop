using UnityEngine;

public class BouncyEnemy : MonoBehaviour
{
    [SerializeField] private GameObject bouncePad = null;
    [SerializeField] private BoxCollider2D bodyCollider = null;
    [SerializeField] private float colliderOffset = -0.25f;

    private void OnEnable() 
    {
        bouncePad.SetActive(true);
        Vector2 newOffset = bodyCollider.offset;
        newOffset.y += colliderOffset;
        bodyCollider.offset = newOffset;

        Vector2 newSize = bodyCollider.size;
        newSize.y += 2 * colliderOffset;
        bodyCollider.size = newSize;
    }
}
