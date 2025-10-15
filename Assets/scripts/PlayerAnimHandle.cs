using UnityEngine;

public class PlayerAnimationSwitcher : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb; // assign player's Rigidbody2D in inspector
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (!rb)
        {
            rb = GetComponentInParent<Rigidbody2D>();
            if (!rb)
                Debug.LogWarning("⚠️ No Rigidbody2D assigned to PlayerAnimationSwitcher.");
        }
    }

    void Update()
    {
        // use linearVelocity for Unity 2023+
        Vector2 velocity = rb.linearVelocity;

        // determine if player is moving
        bool isMoving = velocity.sqrMagnitude > 0.01f;

        // update animator parameter
        animator.SetBool("IsMoving", isMoving);

        // optional: direction parameters for blend trees
        animator.SetFloat("MoveX", velocity.x);
        animator.SetFloat("MoveY", velocity.y);
    }
}
