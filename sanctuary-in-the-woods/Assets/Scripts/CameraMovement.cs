using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform followTransform;
    
    void Start()
    {
        
    }

    void FixedUpdate() {
        transform.position = new Vector3(followTransform.position.x, followTransform.position.y, transform.position.z);
    }
    
    void Update()
    {
        
    }
}
