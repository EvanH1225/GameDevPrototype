using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 0.1f;

    // Mouse settings
    public float mouseSensitivity = 1f;
    public Transform playerCamera;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool jumpPressed;

    public float playerRange = 6f;

    private bool pickupPressed;
    public LayerMask pickupLayer;
    public InventoryManager inventoryManager;

    private bool attackPressed;
    public LayerMask attackLayer;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleLook();
        HandlePickup();
        HandleAttack();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }

        if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void OnPickup(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pickupPressed = true;
        }

        if (context.canceled)
        {
            pickupPressed = false;
        }
    }
    
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            attackPressed = true;
        }

        if (context.canceled)
        {
            attackPressed = false;
        }
    }


    void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        move = move * moveSpeed;

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (jumpPressed && controller.isGrounded)
        {
            Debug.DrawRay(playerCamera.position, playerCamera.forward * 20, Color.blue, 5);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move((move + velocity) * Time.deltaTime);
    }


    void HandleLook()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime * 100f;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime * 100f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandlePickup()
    {
        if (pickupPressed)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, playerRange, pickupLayer))
            {
                Item item = hit.collider.GetComponent<Item>();
                if (item != null)
                {
                    item.OnPickup(inventoryManager);
                }
            }

            pickupPressed = false;
        }
    }
    
    void HandleAttack()
    {
        if (attackPressed)
        {
            Ray ray = new Ray(playerCamera.position, playerCamera.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, playerRange, attackLayer))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy == null)
                {
                    enemy = hit.collider.GetComponentInParent<Enemy>();
                }
                if (enemy != null)
                {
                    Debug.Log("HIT ENEMY");
                    enemy.TakeDamage();
                    enemy.ApplyKnockback(playerCamera.position);
                }
            }

            attackPressed = false;
        }
    }
}
