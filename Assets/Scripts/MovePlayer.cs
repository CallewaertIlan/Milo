using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject cameraGameObject;
    
    private CollectBar collectBar;

    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 10.0f;
    
    private Vector3 movementForward;
    private Vector3 sideMovement;

    void Start()
    {
        collectBar = FindObjectOfType<CollectBar>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = cameraGameObject.transform.forward;
        cameraForward.y = 0;

        Vector3 cameraRight = cameraGameObject.transform.right;

        movementForward = cameraForward * verticalInput;
        sideMovement = cameraRight * horizontalInput;

        if (movementForward != Vector3.zero || sideMovement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementForward + sideMovement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
            collectBar.PlayerIsMoving(true);
        }
        else
        {
            collectBar.PlayerIsMoving(false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Courir vers l'avant ou l'arrière
            transform.Translate((movementForward + sideMovement) * runSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            // Marcher vers l'avant ou l'arrière
            transform.Translate((movementForward + sideMovement) * walkSpeed * Time.deltaTime, Space.World);
        }
    }

    public bool IsMoving()
    {
        if (movementForward != Vector3.zero || sideMovement != Vector3.zero)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}