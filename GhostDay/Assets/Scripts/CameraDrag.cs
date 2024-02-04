using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private Camera mainCamera;
    private float cameraHalfWidth;
    private SpriteRenderer backgroundRenderer;

    public GameObject background;
    public GameObject[] otherPanels;
    public float dragSpeed = 1.0f; // 드래그 속도를 조절할 변수

    void Start()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * Screen.width / Screen.height;
        backgroundRenderer = background.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!IsAnyOtherPanelActive())
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;

                // 드래그 속도 조절
                Vector3 newPosition = mainCamera.transform.position - new Vector3(delta.x, 0, 0) * Time.deltaTime * dragSpeed;

                float backgroundHalfWidth = backgroundRenderer.bounds.size.x * 0.5f;
                float backgroundLeftBoundary = background.transform.position.x - backgroundHalfWidth;
                float backgroundRightBoundary = background.transform.position.x + backgroundHalfWidth;

                bool anyPanelActive = IsAnyOtherPanelActive();

                if (newPosition.x >= backgroundLeftBoundary + cameraHalfWidth && newPosition.x <= backgroundRightBoundary - cameraHalfWidth && !anyPanelActive)
                {
                    float clampedX = Mathf.Clamp(newPosition.x, float.MinValue + cameraHalfWidth, float.MaxValue - cameraHalfWidth);

                    mainCamera.transform.position = new Vector3(clampedX, mainCamera.transform.position.y, mainCamera.transform.position.z);
                }

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    bool IsAnyOtherPanelActive()
    {
        foreach (GameObject panel in otherPanels)
        {
            if (panel.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}