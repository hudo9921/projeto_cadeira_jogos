using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float newFieldOfView = 90f; // New field of view value (for perspective camera)
    public float newOrthographicSize = 10f; // New orthographic size value (for orthographic camera)

    void Start()
    {
        // Check if the mainCamera is assigned
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned!");
            return;
        }

        // Adjust the camera's field of view (for perspective camera)
        if (mainCamera.orthographic == false) // Check if the camera is not orthographic
        {
            mainCamera.fieldOfView = newFieldOfView; // Set new field of view
        }
        else // Adjust the camera's orthographic size (for orthographic camera)
        {
            mainCamera.orthographicSize = newOrthographicSize; // Set new orthographic size
        }
    }
}
