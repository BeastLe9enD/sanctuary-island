using UnityEngine;

public sealed class CameraMovement : MonoBehaviour
{
    public Transform FollowTransform;
    
    void FixedUpdate() {
        transform.position = new Vector3(FollowTransform.position.x, FollowTransform.position.y, transform.position.z);
    }
}
