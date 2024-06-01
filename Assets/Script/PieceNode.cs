using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceNode : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // �ڽ��� �巡�� �Ǿ�����
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // �ڽ��� �巡�װ� ��������
    public void OnEndDrag(PointerEventData eventData)
    {
        for (int i = 0; i < QuizManager.Instance.snap_pos.transform.childCount; i++)
        {
            if (Check_Node() == true)
            {
                if (Vector3.Distance(QuizManager.Instance.snap_pos.transform.GetChild(i).position, transform.position) < QuizManager.Instance.snapOffset)
                {
                    Debug.Log(Vector3.Distance(QuizManager.Instance.snap_pos.transform.GetChild(i).position, transform.position));
                    transform.position = QuizManager.Instance.snap_pos.transform.GetChild(i).position;
                    QuizManager.Instance.Check_snap_pos.Add(i);
                    return;
                }
            }
        }

        QuizManager.Instance.Reset_Pos();
    }

    // ���� �����µ� ��ġ�� �ٸ� ������Ʈ�� �ִ��� Ȯ��
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
