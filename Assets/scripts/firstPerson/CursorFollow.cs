using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }
}
