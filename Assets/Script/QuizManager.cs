using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 퀴즈 관리 스크립트
// 구현 기능 : 퀴즈 별 실행 함수 작성
public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance { get; private set; }

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

    // 플레이어의 정답을 저장할 변수
    private string Answer = "";

    // 현제 문제의 번호를 저장할 변수
    public int Quiz_number = 0;

    public int snapOffset;

    public GameObject Node1,
        Node2;
    public GameObject snap_pos;

    public List<int> Check_snap_pos;
    private Vector2 origin1,
        origin2;

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

    // Node의 위치값을 리셋하는 함수
    public void Reset_Pos()
    {
        Node1.transform.localPosition = origin1;
        Node2.transform.localPosition = origin2;
    }

    // 퀴즈 시작 함수
    public void Quiz_Start()
    {
        Reset_Quiz();
        state = QuizState.Quiz_Start;
        UIManager.Instance.Open_Panel("Quiz");
    }

    // 퀴즈 초기화 함수
    public void Reset_Quiz()
    {
        Answer = "";
        Reset_Pos();
        Check_snap_pos = new List<int>();
    }

    // 퀴즈 정답 제출 함수
    public void Compare_answer()
    {
        Reset_Quiz();
    }

    // 퀴즈 종료 함수
    public void Quiz_End()
    {
        state = QuizState.End;
        UIManager.Instance.Open_Panel("Move");
    }

    // 정답을 저장하는 함수
    // 플레이어가 숫자 버튼을 누를때 호출되어 값을 저장한다.
    public void Save_Answer(string answ)
    {
        Answer += answ;
    }
}
