using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeController : MonoBehaviour
{
    // UI 판넬을 Inspector에서 할당하세요.
    public GameObject fadePanel;

    // Fade In 효과를 위한 코루틴 함수
    public IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1.0f;
        int n = 1;

        while (fadeCount > 0)
        {
            fadeCount -= (0.001f * n++);
            yield return new WaitForSeconds(0.01f);
            
            // 판넬의 자식 중 Image 컴포넌트의 색상을 조절
            fadePanel.GetComponent<Image>().color = new Color(0, 0, 0, fadeCount);
        }
    }

    // Fade Out 효과를 위한 코루틴 함수
    public IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0.0f;
        int n = 1;

        while (fadeCount < 1)
        {
            fadeCount += (0.001f * n++);
            yield return new WaitForSeconds(0.01f);

            // 판넬의 자식 중 Image 컴포넌트의 색상을 조절
            fadePanel.GetComponent<Image>().color = new Color(0, 0, 0, fadeCount);
        }
    }
}