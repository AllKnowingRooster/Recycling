using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{

    float rotationX = 0;
    float rotationY = 0;
    float sensitivity = 5.0f;
    public Transform gameobj;
    Vector3 meshRotation;
    // Update is called once per frame
    void Update()
    {
        rotationX += Input.GetAxis("Mouse X") * sensitivity;
        rotationY += Input.GetAxis("Mouse Y") * sensitivity * -1;
        transform.localEulerAngles = new Vector3(Mathf.Clamp(rotationY,-50,50),rotationX, 0.0f);
        meshRotation = Vector3.RotateTowards(gameobj.forward, transform.forward, 1.0f, 0.0f);
        gameobj.rotation=Quaternion.LookRotation(meshRotation);
    }
}
