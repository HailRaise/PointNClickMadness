using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraFollowRoom : MonoBehaviour
{
    public Transform target;
    public Tilemap map;
    [Range(0.01f, 1f)] public float smoothSpeed = 0.15f;
    public Vector3 offset;

    private Vector3 minBounds;
    private Vector3 maxBounds;
    private float halfHeight;
    private float halfWidth;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Get map bounds in world space
        Bounds bounds = map.localBounds;
        minBounds = map.CellToWorld(map.origin);
        maxBounds = map.CellToWorld(map.origin + map.size);

        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = -10f;

        // Clamp inside room bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        Vector3 clampedPos = new Vector3(clampedX, clampedY, desiredPosition.z);
        transform.position = Vector3.Lerp(transform.position, clampedPos, smoothSpeed);
    }
}
