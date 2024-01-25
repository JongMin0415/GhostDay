using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour
{
    private Vector3 lastMousePosition;
    private Camera mainCamera;
    private float cameraHalfWidth;
    private SpriteRenderer backgroundRenderer;

    public GameObject background;  // ��� ������Ʈ�� ������ ����
    public GameObject[] otherPanels;  // �ٸ� �гε��� ������ �迭

    void Start()
    {
        mainCamera = Camera.main;

        // ī�޶��� �ݳʺ� ���
        cameraHalfWidth = mainCamera.orthographicSize * Screen.width / Screen.height;

        // ����� SpriteRenderer�� ��������
        backgroundRenderer = background.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �ٸ� �г� �� �ϳ��� Ȱ��ȭ�Ǿ� ������ �巡�� �Ұ���
        if (!IsAnyOtherPanelActive())
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - lastMousePosition;

                // ���ο� ī�޶� ��ġ ���
                Vector3 newPosition = mainCamera.transform.position - new Vector3(delta.x, 0, 0) * Time.deltaTime;

                // ����� �¿� ��� ���
                float backgroundHalfWidth = backgroundRenderer.bounds.size.x * 0.5f;
                float backgroundLeftBoundary = background.transform.position.x - backgroundHalfWidth;
                float backgroundRightBoundary = background.transform.position.x + backgroundHalfWidth;

                // �гε� �� Ȱ��ȭ�� ���� �ִ��� Ȯ��
                bool anyPanelActive = IsAnyOtherPanelActive();

                // ����� ��� ���� �ְ�, �г��� Ȱ��ȭ�Ǿ� ���� ������ ī�޶� ���ο� ��ġ�� �̵�
                if (newPosition.x >= backgroundLeftBoundary + cameraHalfWidth && newPosition.x <= backgroundRightBoundary - cameraHalfWidth && !anyPanelActive)
                {
                    // ���ο� ��ġ�� ī�޶� ���� ���� ����
                    float clampedX = Mathf.Clamp(newPosition.x, float.MinValue + cameraHalfWidth, float.MaxValue - cameraHalfWidth);

                    mainCamera.transform.position = new Vector3(clampedX, mainCamera.transform.position.y, mainCamera.transform.position.z);
                }

                lastMousePosition = Input.mousePosition;
            }
        }
    }

    // �ٸ� �г� �� �ϳ��� Ȱ��ȭ�Ǿ� �ִ��� Ȯ���ϴ� �Լ�
    bool IsAnyOtherPanelActive()
    {
        foreach (GameObject panel in otherPanels)
        {
            if (panel.activeSelf)
            {
                return true; // �ϳ��� Ȱ��ȭ�� �г��� ������ true ��ȯ
            }
        }
        return false; // ��� �г��� ��Ȱ��ȭ�Ǿ� ������ false ��ȯ
    }
}