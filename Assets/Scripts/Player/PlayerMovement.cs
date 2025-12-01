using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    private InputActionMap inputActionMap;
    private InputAction inputAction;
    private Quaternion moveRotation;
    private Vector3 moveDirection;
    private float rotateSpeed = 5.0f;
    private float speed = 5.0f;
    private Rigidbody playerRigidbody;
    public Transform Camera;
    //public Transform PlayerObject;
    void Start()
    {
        inputActionMap = inputActionAsset.FindActionMap("Player");
        inputAction = inputActionMap.FindAction("Movement");
        inputAction.Enable();
        inputActionMap.Enable();
        inputActionAsset.Enable();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.instance.isActive && !GameManager.instance.isGameOver)
        {
            Vector2 input = inputAction.ReadValue<Vector2>();
            moveDirection.Set(input.x, 0.0f, input.y);

            Vector3 cameraForward = Camera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = Camera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();
            Vector3 moveForward = (moveDirection.x * cameraRight + moveDirection.z * cameraForward).normalized;
            playerRigidbody.MovePosition(playerRigidbody.position + (moveForward * speed * Time.deltaTime));
        }
    }


    private void FixedUpdate()
    {
        playerRigidbody.linearVelocity = Vector3.zero;
    }
}
