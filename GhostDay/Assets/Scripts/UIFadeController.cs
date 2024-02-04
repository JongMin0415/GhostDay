using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFadeController : MonoBehaviour
{
    // UI �ǳ��� Inspector���� �Ҵ��ϼ���.
    public GameObject fadePanel;

    // Fade In ȿ���� ���� �ڷ�ƾ �Լ�
    public IEnumerator FadeInCoroutine()
    {
        float fadeCount = 1.0f;
        int n = 1;

        while (fadeCount > 0)
        {
            fadeCount -= (0.001f * n++);
            yield return new WaitForSeconds(0.01f);
            
            // �ǳ��� �ڽ� �� Image ������Ʈ�� ������ ����
            fadePanel.GetComponent<Image>().color = new Color(0, 0, 0, fadeCount);
        }
    }

    // Fade Out ȿ���� ���� �ڷ�ƾ �Լ�
    public IEnumerator FadeOutCoroutine()
    {
        float fadeCount = 0.0f;
        int n = 1;

        while (fadeCount < 1)
        {
            fadeCount += (0.001f * n++);
            yield return new WaitForSeconds(0.01f);

            // �ǳ��� �ڽ� �� Image ������Ʈ�� ������ ����
            fadePanel.GetComponent<Image>().color = new Color(0, 0, 0, fadeCount);
        }
    }
}