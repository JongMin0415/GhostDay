using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivation : MonoBehaviour
{
    // 선택된 오브젝트를 저장할 변수
    private GameObject selectedObject;

    // 오브젝트에 설정된 판넬
    public GameObject panel;

    void Start()
    {
        // 게임 시작 시 판넬 비활성화
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        // 마우스 클릭을 시작하고 끝낸 위치가 오브젝트 내부에 속하는지 확인
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                // 클릭을 시작한 위치가 오브젝트 내부에 속하면 선택된 오브젝트 저장
                selectedObject = hit.collider.gameObject;
            }
            else
            {
                // 오브젝트 외부에서 클릭을 시작한 경우 초기화
                selectedObject = null;
            }
        }

        // 마우스 클릭을 끝냈을 때
        if (Input.GetMouseButtonUp(0) && selectedObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // 클릭을 끝낸 위치가 오브젝트 내부에 속하면 판넬 활성화
            if (hit.collider != null && hit.collider.gameObject == selectedObject)
            {
                ActivatePanel();
            }
        }
    }

    // 판넬 활성화 함수
    void ActivatePanel()
    {
        // 선택된 오브젝트에 판넬이 있을 경우 활성화
        if (selectedObject != null && panel != null && !panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }

    // 판넬 비활성화 함수
    public void DeactivatePanel()
    {
        // 판넬 비활성화
        if (panel != null && panel.activeSelf)
        {
            panel.SetActive(false);
        }
    }
}