using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDown : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;

    [Header("Phone Settings")]
    // ИЗМЕНЕНО: Ссылка на Animator телефона вместо GameObject
    public Animator phoneAnimator; 
    private bool isPhoneOpen = false; 

    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private Vector2 lastMoveDir = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ИЗМЕНЕНО: Строка phoneUI.SetActive(false) больше не нужна,
        // так как аниматор сам управляет положением телефона
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        // --- ЛОГИКА ТЕЛЕФОНА ---
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TogglePhone();
        }

        // --- БЛОКИРУЕМ ДВИЖЕНИЕ, ЕСЛИ ТЕЛЕФОН ОТКРЫТ ---
        if (isPhoneOpen)
        {
            moveInput = Vector2.zero; 
            if (animator) animator.SetBool("IsMoving", false);
            return;
        }

        // --- Get movement input ---
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // --- Determine if moving ---
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        // --- Store last move direction (so idle faces last way moved) ---
        // ВАША ЛОГИКА, КОТОРАЯ БЫЛА СОХРАНЕНА
        if (isMoving)
            lastMoveDir = moveInput;

        // --- Update Animator parameters ---
        if (animator)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
    }

    void TogglePhone()
    {
        isPhoneOpen = !isPhoneOpen;

        // ИЗМЕНЕНО: Управляем параметром в Аниматоре
        if (phoneAnimator != null)
        {
            phoneAnimator.SetBool("IsOpen", isPhoneOpen);
        }

        // Показываем или прячем курсор мыши
        Cursor.visible = isPhoneOpen;
        
        // Разблокируем или блокируем курсор
        Cursor.lockState = isPhoneOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        // Двигаем персонажа только если телефон НЕ открыт
        if (isPhoneOpen)
        {
            // ВАША ЛОГИКА ОСТАНОВКИ, КОТОРАЯ БЫЛА СОХРАНЕНА
            rb.linearVelocity = Vector2.zero; // Заменено на velocity для большей совместимости
            return;
        }

        // --- Move the player ---
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        
        if (moveInput.x > 0.0f && isFacingRight == false)
        {
            Flip();
        }
        else if (moveInput.x < 0.0f && isFacingRight == true)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
}