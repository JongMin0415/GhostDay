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
    public GameObject[] otherPanels;  // 다른 패널들을 참조할 배열

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
        // 다른 패널 중 하나라도 활성화되어 있으면 드래그 불가능
        if (!IsAnyOtherPanelActive())
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

                // 배경의 좌우 경계 계산
                float backgroundHalfWidth = backgroundRenderer.bounds.size.x * 0.5f;
                float backgroundLeftBoundary = background.transform.position.x - backgroundHalfWidth;
                float backgroundRightBoundary = background.transform.position.x + backgroundHalfWidth;

                // 패널들 중 활성화된 것이 있는지 확인
                bool anyPanelActive = IsAnyOtherPanelActive();

                // 배경의 경계 내에 있고, 패널이 활성화되어 있지 않으면 카메라를 새로운 위치로 이동
                if (newPosition.x >= backgroundLeftBoundary + cameraHalfWidth && newPosition.x <= backgroundRightBoundary - cameraHalfWidth && !anyPanelActive)
                {
                    // 새로운 위치를 카메라 영역 내에 제한
                    float clampedX = Mathf.Clamp(newPosition.x, float.MinValue + cameraHalfWidth, float.MaxValue - cameraHalfWidth);

                    mainCamera.transform.position = new Vector3(clampedX, mainCamera.transform.position.y, mainCamera.transform.position.z);
                }

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    // 다른 패널 중 하나라도 활성화되어 있는지 확인하는 함수
    bool IsAnyOtherPanelActive()
    {
        foreach (GameObject panel in otherPanels)
        {
            if (panel.activeSelf)
            {
                return true; // 하나라도 활성화된 패널이 있으면 true 반환
            }
        }
        return false; // 모든 패널이 비활성화되어 있으면 false 반환
    }
}