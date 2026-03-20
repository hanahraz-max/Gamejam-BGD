using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private PlayerInput input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = new PlayerInput();
    }

    private void OnEnable()
    {
        input.Contoller.Enable();
    }

    private void OnDisable()
    {
        input.Contoller.Disable();
    }

    private void Update()
    {
        moveInput = input.Contoller.Movement.ReadValue<Vector2>();
    }

    public bool IsActionPressedThisFrame()
    {
        return input != null && input.Contoller.Action.WasPressedThisFrame();
    }

    private void FixedUpdate()
    {
        if (rb == null)
        {
            return;
        }

        rb.linearVelocity = moveInput * moveSpeed;
    }
}
