using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned!");
        }
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
