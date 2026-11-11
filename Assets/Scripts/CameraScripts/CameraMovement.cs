using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Ustawienia Ruchu")]
    public float dragSpeed = 20f;
    private Vector3 dragOrigin;

    [Header("Granice Mapy (Limity)")]
    public float minX = -40f;
    public float maxX = 6.8f;
    public float minZ = -40f;
    public float maxZ = 8f;

    private float startY; 

    void Start()
    {
        
        startY = transform.position.y;
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

       
        if (!Input.GetMouseButton(0)) return;

       
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);

       
        transform.Translate(move, Space.Self);

      
        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, minX, maxX);
        clampedPos.z = Mathf.Clamp(clampedPos.z, minZ, maxZ);
        clampedPos.y = startY;
        transform.position = clampedPos;

        
        
        dragOrigin = Input.mousePosition;
    }
}