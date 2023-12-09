using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    public float sensitivity = 1;
    private bool isZooming = false;
    private float currentZoom = 0f;

    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
        else
        {
            Debug.LogError("No Camera component found on this GameObject.");
        }
    }

    void Update()
    {
        // Check if the right mouse button is pressed down
        if (Input.GetMouseButtonDown(1))
        {
            isZooming = true;
        }

        // Check if the right mouse button is released
        if (Input.GetMouseButtonUp(1))
        {
            isZooming = false;
        }

        // Update the currentZoom and the camera's fieldOfView.
        if (isZooming)
        {
            currentZoom += sensitivity * 0.5f * Time.deltaTime; // Ajusta la velocidad de zoom aquí
            currentZoom = Mathf.Clamp01(currentZoom);
            camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
        }
        else
        {
            // Vuelve al estado original cuando no se está haciendo zoom
            currentZoom -= sensitivity * 0.5f * Time.deltaTime; // Ajusta la velocidad de retorno aquí
            currentZoom = Mathf.Clamp01(currentZoom);
            camera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
        }
    }
}
