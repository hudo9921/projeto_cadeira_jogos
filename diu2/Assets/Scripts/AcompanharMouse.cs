using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{
    public Transform player;  
    public float radius = 2f; 

    private void Update()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; 

        
        Vector3 directionToMouse = mousePosition - player.position;

        
        directionToMouse.Normalize();

        
        Vector3 firePosition = player.position + (directionToMouse * radius);

        
        transform.position = firePosition;

        
        Vector3 lookDirection = mousePosition - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
