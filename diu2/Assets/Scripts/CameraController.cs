using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float newFieldOfView = 90f; 
    public float newOrthographicSize = 10f; 

    void Start()
    {
        
        if (mainCamera == null)
        {
            Debug.LogWarning("Main camera is not assigned!");
            return;
        }

        
        if (mainCamera.orthographic == false) 
        {
            mainCamera.fieldOfView = newFieldOfView; 
        }
        else 
        {
            mainCamera.orthographicSize = newOrthographicSize; 
        }
    }
}
