using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapNode : MonoBehaviour
{
    // �̴ϸʿ��� �ڽ��� ��Ÿ���� �Լ�
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // �̴ϸʿ��� �ڽ��� ����� �Լ�
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
