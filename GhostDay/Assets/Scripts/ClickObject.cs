using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour {
    [Header("인벤토리")]
    public Inventory inventory;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit.collider != null) {
                HitCheckObject(hit);
            }
        }
    }

    void HitCheckObject(RaycastHit2D hit) {
        ObjectItem clickInterface = hit.transform.gameObject.GetComponent<ObjectItem>();

        if (clickInterface != null) {
            Item item = clickInterface.ClickItem();
            print($"{item.itemName}");
            inventory.AddItem(item);

            // 오브젝트를 비활성화하여 사라지게 만듭니다.
            clickInterface.gameObject.SetActive(false);
        }
    }
}
