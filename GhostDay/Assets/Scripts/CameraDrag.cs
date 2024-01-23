using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private Camera mainCamera;
    private float cameraHalfWidth;
    private SpriteRenderer backgroundRenderer;

    public GameObject background;  // 배경 오브젝트를 참조할 변수

    void Start()
    {
        mainCamera = Camera.main;

        // 카메라의 반너비를 계산
        cameraHalfWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

        // 배경의 SpriteRenderer를 가져오기
        backgroundRenderer = background.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // 새로운 카메라 위치 계산
            Vector3 newPosition = mainCamera.transform.position - new Vector3(delta.x, 0, 0) * Time.deltaTime;

            // 새로운 위치를 카메라 영역 내에 제한
            float clampedX = Mathf.Clamp(newPosition.x, float.MinValue + cameraHalfWidth, float.MaxValue - cameraHalfWidth);

            // 카메라를 새로운 위치로 이동
            mainCamera.transform.position = new Vector3(clampedX, mainCamera.transform.position.y, mainCamera.transform.position.z);

            // 배경의 좌우 경계 계산
            float backgroundHalfWidth = backgroundRenderer.bounds.size.x * 0.5f;
            float backgroundLeftBoundary = background.transform.position.x - backgroundHalfWidth;
            float backgroundRightBoundary = background.transform.position.x + backgroundHalfWidth;

            // 배경의 경계 내에 카메라를 유지
            float clampedCameraX = Mathf.Clamp(mainCamera.transform.position.x, backgroundLeftBoundary + cameraHalfWidth, backgroundRightBoundary - cameraHalfWidth);
            mainCamera.transform.position = new Vector3(clampedCameraX, mainCamera.transform.position.y, mainCamera.transform.position.z);

            lastMousePosition = Input.mousePosition;
        }
    }
}