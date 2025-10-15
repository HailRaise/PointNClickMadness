using UnityEngine;

public class FlipAnim : MonoBehaviour
{
    public Rigidbody2D rb;
    private float lastDirX = 1f;

    void Start()
    {
        if (!rb)
        {
            rb = GetComponentInParent<Rigidbody2D>();
            if (!rb)
                Debug.LogWarning("⚠️ No Rigidbody2D assigned for PlayerFlipHandler.");
        }
    }

    void Update()
    {
        float moveX = rb.linearVelocity.x;  // ✅ replaced velocity.x with linearVelocity.x

        if (Mathf.Abs(moveX) > 0.01f)
            lastDirX = Mathf.Sign(moveX);

        // flip sprite scale.x
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * lastDirX;
        transform.localScale = scale;
    }
}
