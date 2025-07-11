using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign your player's transform here
    public float followSpeed = 5f; // Speed of camera movement
    public Vector3 offset; // Offset from the player's position

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player transform is not assigned to CameraFollow script.");
            return;
        }

        // Calculate the target position for the camera with offset
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z) + offset;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed);
    }
}
