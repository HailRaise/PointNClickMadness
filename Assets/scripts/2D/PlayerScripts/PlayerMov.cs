using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name.Contains("HallWay"))
        Debug.Log($"lockState={Cursor.lockState}, visible={Cursor.visible}");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ИЗМЕНЕНО: Строка phoneUI.SetActive(false) больше не нужна,
        // так как аниматор сам управляет положением телефона
        
        Cursor.visible = false;
        string sceneName = SceneManager.GetActiveScene().name;
        // bool isFirstPerson = sceneName.Contains("HallWay") || sceneName.Contains("FP");
        // if (isFirstPerson)
        // {
        //     Debug.Log("Cursor Unlocked");
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        // }
        // else
        // {
        //     Debug.Log("Cursor locked");
        //     Cursor.lockState = CursorLockMode.Locked;
        //     Cursor.visible = false;
        // }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("HallWay"))
        Debug.Log($"[{Time.frameCount}] lockState={Cursor.lockState}");

        // --- ЛОГИКА ТЕЛЕФОНА ---
        // if (Input.GetKeyDown(KeyCode.UpArrow))
        // {
        //     TogglePhone();
        // }

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

    // void TogglePhone()
    // {
    //    isPhoneOpen = !isPhoneOpen;

    // if (phoneAnimator != null)
    //     phoneAnimator.SetBool("IsOpen", isPhoneOpen);

    // // Only touch the cursor in top-down scenes
    // if (!SceneManager.GetActiveScene().name.Contains("HallWay") &&
    //     !SceneManager.GetActiveScene().name.Contains("FP"))
    // {
    //     Cursor.visible = isPhoneOpen;
    //     Cursor.lockState = isPhoneOpen ? CursorLockMode.None : CursorLockMode.Locked;
    // }
    // }

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
