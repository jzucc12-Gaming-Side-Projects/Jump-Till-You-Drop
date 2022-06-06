using JH.LEVEL;
using UnityEngine;

namespace JH.CAMERA
{
    public class ParallaxBackground : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxModifer = Vector3.zero;
        private Transform cameraTransform = null;
        private Vector3 lastPosition = Vector3.zero;
        private float textureUnitSizeX = 0f;
        private float textureUnitSizeY = 0f;


        private void Start()
        {
            cameraTransform = FindObjectOfType<Camera>().transform;
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
        }

        private void LateUpdate()
        {
            Vector3 newPosition = cameraTransform.position;
            Vector3 movementVector = newPosition - lastPosition;
            lastPosition = newPosition;
            ParallaxMove(movementVector);
            InfiniteShift();
        }

        private void ParallaxMove(Vector3 _movementVector, bool _ignoreThreshold = false)
        {
            transform.position += new Vector3(_movementVector.x * parallaxModifer.x, 0, 0);
            transform.position += new Vector3(0, _movementVector.y * parallaxModifer.y, 0);
        }

        private void InfiniteShift()
        {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
            {
                float offset = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y);
            }

            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
            {
                float offset = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offset);
            }
        }
    }
}
