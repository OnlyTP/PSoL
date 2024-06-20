using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float minYPosition = 0f; // The minimum y position the camera can have
    public float minXPosition = 0f;
    public float maxXPosition = 100f;

    private void LateUpdate()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTransform == null)
        {
            Debug.LogWarning("CameraFollow2D: No player transform assigned.");
            return;
        }

        // Calculate the new camera position directly from the player's position with offsets
        float newXPosition = playerTransform.position.x + xOffset;
        float newYPosition = playerTransform.position.y + yOffset;

        // Ensure the camera's y position never goes below minYPosition
        newYPosition = Mathf.Max(newYPosition, minYPosition);

        // Set the camera position directly, removing any smooth damping
        transform.position = new Vector3(newXPosition, newYPosition, transform.position.z);
    }
}
