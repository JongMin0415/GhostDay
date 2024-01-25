using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivation : MonoBehaviour
{
    // ���õ� ������Ʈ�� ������ ����
    private GameObject selectedObject;

    // ������Ʈ�� ������ �ǳ�
    public GameObject panel;

    void Start()
    {
        // ���� ���� �� �ǳ� ��Ȱ��ȭ
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        // ���콺 Ŭ���� �����ϰ� ���� ��ġ�� ������Ʈ ���ο� ���ϴ��� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                // Ŭ���� ������ ��ġ�� ������Ʈ ���ο� ���ϸ� ���õ� ������Ʈ ����
                selectedObject = hit.collider.gameObject;
            }
            else
            {
                // ������Ʈ �ܺο��� Ŭ���� ������ ��� �ʱ�ȭ
                selectedObject = null;
            }
        }

        // ���콺 Ŭ���� ������ ��
        if (Input.GetMouseButtonUp(0) && selectedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Ŭ���� ���� ��ġ�� ������Ʈ ���ο� ���ϸ� �ǳ� Ȱ��ȭ
            if (hit.collider != null && hit.collider.gameObject == selectedObject)
            {
                ActivatePanel();
            }
        }
    }

    // �ǳ� Ȱ��ȭ �Լ�
    void ActivatePanel()
    {
        // ���õ� ������Ʈ�� �ǳ��� ���� ��� Ȱ��ȭ
        if (selectedObject != null && panel != null && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    // �ǳ� ��Ȱ��ȭ �Լ�
    public void DeactivatePanel()
    {
        // �ǳ� ��Ȱ��ȭ
        if (panel != null && panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }
}