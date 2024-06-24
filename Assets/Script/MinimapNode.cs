using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapNode : MonoBehaviour
{
    // 미니맵에서 자신을 나타내는 함수
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // 미니맵에서 자신을 숨기는 함수
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
