using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform playerTransform;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float smoothTimeX = 0.5f;
    public float smoothTimeY = 0.5f;
    public float minYPosition = 0f; // The minimum y position the camera can have

    private Vector2 velocity;

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, playerTransform.position.x + xOffset, ref velocity.x, smoothTimeX);

        // Use Mathf.Max to ensure that posY is never less than minYPosition
        float posY = Mathf.Max(Mathf.SmoothDamp(transform.position.y, playerTransform.position.y + yOffset, ref velocity.y, smoothTimeY), minYPosition);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
