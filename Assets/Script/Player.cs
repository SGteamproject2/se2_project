using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� ĳ���� ��ũ��Ʈ
// ���� ��� : �̵�, ��ȭ


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


    // �÷��̾��� ����
    private enum PlayerState
    {
        Idle,
        Move,
        Talk,
        Quiz
    }

    // �÷��̾� ĳ������ �ӵ�
    public float Player_Speed;
    private PlayerState player_state = PlayerState.Idle;

    // ��ȭ�� ������ NPC�� ����
    public GameObject Target_NPC;

    // �÷��̾� ĳ������ Update�Լ�
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

    // ĳ���� �̵��� �Է¹޴� �Լ�
    // �� �� �̵��� ������ Ⱦ��ũ�� �����̴�.
    // ����� �������� ȭ�� ��ġ�� ����Ͽ� ĳ���Ͱ� �̵��Ѵ�.
    // ���� ȭ���� Ŭ���ϸ� �������� ����ȭ���� Ŭ���ϸ� �������� �̵��Ѵ�.
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

    // ĳ���� �̵� �Լ�
    void Move(int way)
    {
        transform.Translate(new Vector3((Player_Speed * way) * Time.deltaTime, 0));
    }

    // �÷��̾��� ���¸� �����ϴ� �Լ�
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

    

    // ���� ����� NPC�� �̸��� ��ȯ�Ѵ�.
    public string return_name()
    {
        return Target_NPC.name;
    }
}
