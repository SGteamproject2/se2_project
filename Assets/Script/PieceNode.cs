using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceNode : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // 자신이 드래그 되었을떼
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // 자신의 드래그가 끝났을떼
    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < QuizManager.Instance.snap_pos.transform.childCount; i++)
        {
            if (Check_Node() == true)
            {
                if (
                    Vector3.Distance(
                        QuizManager.Instance.snap_pos.transform.GetChild(i).position,
                        transform.position
                    ) < QuizManager.Instance.snapOffset
                )
                {
                    Debug.Log(
                        Vector3.Distance(
                            QuizManager.Instance.snap_pos.transform.GetChild(i).position,
                            transform.position
                        )
                    );
                    transform.position = QuizManager
                        .Instance.snap_pos.transform.GetChild(i)
                        .position;
                    QuizManager.Instance.Check_snap_pos.Add(i);
                    return;
                }
            }
        }

        QuizManager.Instance.Reset_Pos();
    }

    // 현제 오프셋될 위치에 다른 오브젝트가 있는지 확인
    private bool Check_Node()
    {
        for (int i = 0; i < QuizManager.Instance.Check_snap_pos.Count; i++)
        {
            if (QuizManager.Instance.Check_snap_pos.Contains(i) == true)
            {
                return false;
            }
        }

        return true;
    }
}
