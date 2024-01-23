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
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;

            // ���ο� ī�޶� ��ġ ���
            Vector3 newPosition = mainCamera.transform.position - new Vector3(delta.x, 0, 0) * Time.deltaTime;

            // ���ο� ��ġ�� ī�޶� ���� ���� ����
            float clampedX = Mathf.Clamp(newPosition.x, float.MinValue + cameraHalfWidth, float.MaxValue - cameraHalfWidth);

            // ī�޶� ���ο� ��ġ�� �̵�
            mainCamera.transform.position = new Vector3(clampedX, mainCamera.transform.position.y, mainCamera.transform.position.z);

            // ����� �¿� ��� ���
            float backgroundHalfWidth = backgroundRenderer.bounds.size.x * 0.5f;
            float backgroundLeftBoundary = background.transform.position.x - backgroundHalfWidth;
            float backgroundRightBoundary = background.transform.position.x + backgroundHalfWidth;

            // ����� ��� ���� ī�޶� ����
            float clampedCameraX = Mathf.Clamp(mainCamera.transform.position.x, backgroundLeftBoundary + cameraHalfWidth, backgroundRightBoundary - cameraHalfWidth);
            mainCamera.transform.position = new Vector3(clampedCameraX, mainCamera.transform.position.y, mainCamera.transform.position.z);

            lastMousePosition = Input.mousePosition;
        }
    }
}