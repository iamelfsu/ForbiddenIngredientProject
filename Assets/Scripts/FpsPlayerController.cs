using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FpsPlayerController : MonoBehaviour
{
    [Header("Refs")]
    public CharacterController charC;
    public Camera pCam;

    [Header("Look Clamp")]
    public float minViewAngle = -80f;
    public float maxViewAngle = 80f;

    [Header("UI")]
    public Slider healthBar;
    public Slider staminaBar;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    public float health = 100f;
    public float stamina = 100f;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference attackAction;
    public InputActionReference dropAction;
    public InputActionReference interactAction;

    [Header("Movement")]
    public float moveSpeed = 4f;
    public float lookSpeed = 150f;

    private Vector3 currentMovement;
    private Vector2 rotStore;

    private void Awake()
    {
        if (charC == null) charC = GetComponent<CharacterController>();
        if (pCam == null) pCam = GetComponentInChildren<Camera>();

        // UI init (varsa)
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }
        if (staminaBar != null)
        {
            staminaBar.maxValue = maxStamina;
            staminaBar.value = stamina;
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Hareket
        if (moveAction != null && moveAction.action != null && charC != null)
        {
            Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

            Vector3 moveForward = transform.forward * moveInput.y;
            Vector3 moveSideways = transform.right * moveInput.x;

            currentMovement = (moveForward + moveSideways) * moveSpeed;
            charC.Move(currentMovement * Time.deltaTime);
        }

        // Bakýþ
        if (lookAction != null && lookAction.action != null && pCam != null)
        {
            Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
            lookInput.y = -lookInput.y;

            rotStore += lookInput * lookSpeed * Time.deltaTime;
            rotStore.y = Mathf.Clamp(rotStore.y, minViewAngle, maxViewAngle);

            transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
            pCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);
        }

        // UI güncelle (health/stamina deðiþirse bar da güncellensin)
        if (healthBar != null) healthBar.value = Mathf.Clamp(health, 0f, maxHealth);
        if (staminaBar != null) staminaBar.value = Mathf.Clamp(stamina, 0f, maxStamina);
    }
}
