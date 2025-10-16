using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerTopDown : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    bool isFacingRight = true;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private Vector2 lastMoveDir = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Get movement input ---
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // --- Determine if moving ---
        bool isMoving = moveInput.sqrMagnitude > 0.01f;

        // --- Store last move direction (so idle faces last way moved) ---
        if (isMoving)
            lastMoveDir = moveInput;

        // --- Update Animator parameters ---
        if (animator)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("MoveX", moveInput.x);
            animator.SetFloat("MoveY", moveInput.y);
            // animator.SetFloat("LastMoveX", lastMoveDir.x);
            // animator.SetFloat("LastMoveY", lastMoveDir.y);
        }
    }

    void FixedUpdate()
    {
        // --- Move the player ---
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        //Debug.Log($"moveInput.x: {moveInput.x}, isFacingRight: {isFacingRight}");
        if (moveInput.x > 0.0f && isFacingRight == false)
        {
            Flip();
            //Debug.Log("trigger flip function");
        }
        else if (moveInput.x < 0.0f && isFacingRight == true)
        {
            Flip();
        }
        
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Debug.Log("Flip triggered! isFacingRight: " + isFacingRight);
        Debug.Log("Before flip, player scale: " + transform.localScale);
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        Debug.Log("After flip, player scale: " + transform.localScale);
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
