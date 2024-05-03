using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the transform of the object to follow
    public float smoothSpeed = 0.125f; // Smoothing speed of camera movement

    public Vector3 offset; // Offset from the target's position (adjust if needed)

    void LateUpdate()
    {
        if (target != null)
        {
            
            Vector3 desiredPosition = target.position + offset;

            desiredPosition.z=transform.position.z;

        
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            
            transform.position = smoothedPosition;
        }
    }
}
