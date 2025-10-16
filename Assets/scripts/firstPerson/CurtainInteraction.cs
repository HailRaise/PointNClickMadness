using UnityEngine;

public class CurtainInteraction : MonoBehaviour
{
    public float slideDuration = 0.5f; // how fast it slides
    public float targetScaleX = 0.2f;  // how thin it becomes when open
    public float slideOffsetX = 0.5f;  // how far to move sideways

    private bool isOpen = false;
    private Vector3 closedScale;
    private Vector3 closedPos;
    private Vector3 openScale;
    private Vector3 openPos;
    private bool isAnimating = false;

    void Start()
    {
        closedScale = transform.localScale;
        closedPos = transform.position;

        // pre-calculate target open position/scale
        openScale = new Vector3(targetScaleX, closedScale.y, closedScale.z);
        openPos = new Vector3(closedPos.x + slideOffsetX, closedPos.y, closedPos.z);
    }

    public void Toggle()
    {
        if (!isAnimating)
            StartCoroutine(SlideCurtain());
    }

    private System.Collections.IEnumerator SlideCurtain()
    {
        isAnimating = true;

        Vector3 startScale = isOpen ? openScale : closedScale;
        Vector3 endScale = isOpen ? closedScale : openScale;

        Vector3 startPos = isOpen ? openPos : closedPos;
        Vector3 endPos = isOpen ? closedPos : openPos;

        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / slideDuration);

            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            transform.position = Vector3.Lerp(startPos, endPos, t);

            yield return null;
        }

        transform.localScale = endScale;
        transform.position = endPos;

        isOpen = !isOpen;
        isAnimating = false;
    }
}
