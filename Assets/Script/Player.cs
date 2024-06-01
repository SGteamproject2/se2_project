using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 캐릭터 스크립트
// 구현 기능 : 이동, 대화


public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }


    // 플레이어의 상태
    private enum PlayerState
    {
        Idle,
        Move,
        Talk,
        Quiz
    }

    // 플레이어 캐릭터의 속도
    public float Player_Speed;
    private PlayerState player_state = PlayerState.Idle;

    // 대화를 진행할 NPC를 저장
    public GameObject Target_NPC;

    // 플레이어 캐릭터의 Update함수
    public void Player_Update()
    {
        if(player_state == PlayerState.Move)
        {
            
        }
        else
        {

        }
        transform.position = ClampPosition(transform.position);
    }

    private Vector3 ClampPosition(Vector3 postion)
    {
        return new Vector3
            (
            Mathf.Clamp(postion.x, CameraMovement.Instance.LimitLeft.transform.position.x, CameraMovement.Instance.LimitRight.transform.position.x),
            transform.position.y, 0
            );
    }

    // 캐릭터 이동을 입력받는 함수
    // 좌 우 이동만 가능한 횡스크롤 게임이다.
    // 모바일 게임으로 화면 터치를 사용하여 캐릭터가 이동한다.
    // 좌측 화면을 클릭하면 좌측으로 우측화면을 클릭하면 우측으로 이동한다.
    public void InputMove(string way)
    {
        switch (way)
        {
            case "Right":
                Move(1);
                break;
            case "Left":
                Move(-1);
                break;
        }
    }

    // 캐릭터 이동 함수
    void Move(int way)
    {
        transform.Translate(new Vector3((Player_Speed * way) * Time.deltaTime, 0));
    }

    // 플레이어의 상태를 변경하는 함수
    public void State_Change(string state)
    {
        switch (state)
        {
            case "Idle":
                player_state = PlayerState.Idle;
                break;
            case "Move":
                player_state = PlayerState.Move;
                break;
            case "Talk":
                player_state = PlayerState.Talk;
                break;
            case "Quiz":
                player_state = PlayerState.Quiz;
                break;
        }
    }

    

    // 현제 대상인 NPC의 이름을 반환한다.
    public string return_name()
    {
        return Target_NPC.name;
    }
}
