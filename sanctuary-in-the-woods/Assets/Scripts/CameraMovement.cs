using UnityEngine;

public sealed class CameraMovement : MonoBehaviour
{
    public Transform FollowTransform;
    
    void FixedUpdate() {
        transform.position = new Vector3(FollowTransform.position.x, FollowTransform.position.y, transform.position.z);
    }

    private void Update() {
        var camera = Camera.main;
        
        if(Input.mouseScrollDelta.y < 0)
        {
            camera.orthographicSize += 20 * Time.deltaTime;
            if(camera.orthographicSize > 10)
            {
                camera.orthographicSize = 10;
            }
        }
 
 
        if(Input.mouseScrollDelta.y > 0)
        {
            camera.orthographicSize -= 20 * Time.deltaTime;
            if(camera.orthographicSize < 4) {
                camera.orthographicSize = 4;
            }
        }
    }
}