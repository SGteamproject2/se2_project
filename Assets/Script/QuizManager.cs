using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ��ũ��Ʈ
// ���� ��� : ���� �� ���� �Լ� �ۼ�
public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private enum QuizState
    {
        Quiz_Start,
        End,
        Suc,
        Fail
    }

    private void Start()
    {
        origin1 = Node1.transform.localPosition;
        origin2 = Node2.transform.localPosition;
    }

    QuizState state;

    // �÷��̾��� ������ ������ ����
    private string Answer = "";

    // ���� ������ ��ȣ�� ������ ����
    public int Quiz_number = 0;

    public int snapOffset;

    public GameObject Node1, Node2;
    public GameObject snap_pos;

    public List<int> Check_snap_pos;
    private Vector2 origin1, origin2;

    public void Quiz_Update()
    {
        switch (state)
        {
            case QuizState.Quiz_Start:

                break;
            case QuizState.End:

                break;
            case QuizState.Suc:
                GameManager.Instance.Suc_Quiz();
                state = QuizState.End;
                break;
            case QuizState.Fail:

                break;
        }
    }

    // Node�� ��ġ���� �����ϴ� �Լ�
    public void Reset_Pos()
    {
        Node1.transform.localPosition = origin1;
        Node2.transform.localPosition = origin2;
    }

    // ���� ���� �Լ�
    public void Quiz_Start()
    {
        Reset_Quiz();
        state = QuizState.Quiz_Start;
        UIManager.Instance.Open_Panel("Quiz");
    }

    // ���� �ʱ�ȭ �Լ�
    public void Reset_Quiz()
    {
        Answer = "";
        Reset_Pos();
        Check_snap_pos = new List<int>();
    }

    // ���� ���� ���� �Լ�
    public void Compare_answer()
    {
        Reset_Quiz();
    }

    // ���� ���� �Լ�
    public void Quiz_End()
    {
        state = QuizState.End;
        UIManager.Instance.Open_Panel("Move");
    }

    // ������ �����ϴ� �Լ�
    // �÷��̾ ���� ��ư�� ������ ȣ��Ǿ� ���� �����Ѵ�.
    public void Save_Answer(string answ)
    {
        Answer += answ;
    }
}
