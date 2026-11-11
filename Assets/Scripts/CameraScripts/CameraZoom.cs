
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Ustawienia Zoomu")]
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    private float currentZoom;
    void Start()
    {
       currentZoom = GetComponent<Camera>().orthographicSize;
    }   

   
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentZoom -= scroll * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize = currentZoom;
        }
    }
}
