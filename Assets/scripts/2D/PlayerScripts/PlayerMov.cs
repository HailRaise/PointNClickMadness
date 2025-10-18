using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDown : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    [Header("Phone Settings")]
    public Animator phoneAnimator;
    // ДОБАВЛЕНО: Ссылка на "паспорт" телефона для проверки в инвентаре
    public ItemData phoneItemData; 
    
    private bool isPhoneOpen = false;
    // ДОБАВЛЕНО: Ссылка на менеджер инвентаря
    private InventoryManager inventoryManager;

    private bool isFacingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private Vector2 lastMoveDir = Vector2.right;

    void Start()
    {
        if (SceneManager.GetActiveScene().name.Contains("HallWay"))
        Debug.Log($"lockState={Cursor.lockState}, visible={Cursor.visible}");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // ДОБАВЛЕНО: Получаем доступ к инвентарю при старте
        inventoryManager = InventoryManager.instance; 

        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ИЗМЕНЕНО: Используем Tab, как договаривались ранее
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePhone();
        }

        if (isPhoneOpen)
        {
            moveInput = Vector2.zero;
            if (animator) animator.SetBool("IsMoving", false);
            return;
        }

        // ... (остальной код движения без изменений) ...
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        bool isMoving = moveInput.sqrMagnitude > 0.01f;
        if (isMoving) lastMoveDir = moveInput;
        if (animator)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
        }
    }

    void TogglePhone()
    {
        // ГЛАВНАЯ ПРОВЕРКА: есть ли у нас телефон?
        if (inventoryManager != null && inventoryManager.HasItem(phoneItemData))
        {
            // Если да, выполняем всю логику анимации и блокировки курсора
            isPhoneOpen = !isPhoneOpen;

            if (phoneAnimator != null)
            {
                phoneAnimator.SetBool("IsOpen", isPhoneOpen);
            }

            //Cursor.visible = isPhoneOpen;
            //Cursor.lockState = isPhoneOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }
        else
        {
            // Если телефона нет, ничего не делаем
            Debug.Log("У меня нет телефона!");
        }
    }

    void FixedUpdate()
    {
        if (isPhoneOpen)
        {
            rb.linearVelocity = Vector2.zero; // Используем velocity для надежной остановки
            return;
        }

        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        
        if (moveInput.x > 0.0f && !isFacingRight) Flip();
        else if (moveInput.x < 0.0f && isFacingRight) Flip();
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }
    private static bool playerExists = false;

    void Awake()
    {
        if (playerExists)
        {
            Destroy(gameObject);  // destroy any extra player copies
            return;
        }

        playerExists = true;
        DontDestroyOnLoad(gameObject);
    }


}
