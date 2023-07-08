using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private GameObject cameraGameObject;

    private CollectBar collectBar;

    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 10.0f;

    private Vector3 movementForward;
    private Vector3 sideMovement;

    private KeyCode forwardKey;
    private KeyCode backKey;
    private KeyCode rightKey;
    private KeyCode leftKey;

    private bool isInitialized = false;

    private void Awake()
    {
        forwardKey = KeyCode.Z;
        backKey = KeyCode.S;
        rightKey = KeyCode.D;
        leftKey = KeyCode.Q;
    }

    void Start()
    {
        collectBar = FindObjectOfType<CollectBar>();

        SettingsManager.Instance.OnKeyChanged += UpdateKeys;
        isInitialized = true;
    }

    void Update()
    {
        Move();
    }

    private void UpdateKeys(KeyCode newForwardKey, KeyCode newBackKey, KeyCode newRightKey, KeyCode newLeftKey, KeyCode newInventoryKey)
    {
        forwardKey = newForwardKey;
        backKey = newBackKey;
        rightKey = newRightKey;
        leftKey = newLeftKey;
    }

    private void Move()
    {
        if (!isInitialized)
            return;

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(forwardKey))
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(backKey))
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(rightKey))
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(leftKey))
        {
            horizontalInput = -1f;
        }

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