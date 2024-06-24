using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UI를 관리하는 스크립트
// 구현 기능 : 버튼별 기능, UI 패널 선택적 활성화 기능 
public class UIManager : MonoBehaviour
{
    public static UIManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetResoultion();
        Open_Panel("Move");
    }

    public GameObject Move_Panel;
    public GameObject Dialog_Panel;
    public GameObject Quiz_Panel;

    public Text GoldeKey_Text;
    public Text SilverKey_Text;

    public bool Button_Press;
    public string Button_State;

    public void UI_Update(int goldenkey,int silverkey)
    {
        if(Button_Press == true)
        {
            Player.Instance.InputMove(Button_State);
        }

        //GoldeKey_Text.text = "X " + goldenkey;
        //SilverKey_Text.text = "X " + silverkey;
    }

    // 상황에 맞춰 패널 오브젝트를 활성화 시켜주는 함수
    public void Open_Panel(string name)
    {
        Move_Panel.SetActive(false);
        Dialog_Panel.SetActive(false);
        Quiz_Panel.SetActive(false);

        switch (name)
        {
            case "Move":
                Move_Panel.SetActive(true);
                break;
            case "Dialog":
                Dialog_Panel.SetActive(true);
                break;
            case "Quiz":
                Quiz_Panel.SetActive(true);
                break;
        }
    }

    // 오른쪽 이동
    public void Right_Move()
    {
        Button_State = "Right";
        Button_Press = true;
        Player.Instance.State_Change("Move");
    }

    // 왼쪽 이동
    public void Left_Move()
    {
        Button_State = "Left";
        Button_Press = true;
        Player.Instance.State_Change("Move");
    }

    // 버튼 상태 초기화
    public void Reset_State()
    {
        Button_State = null;
        Button_Press = false;
        Player.Instance.State_Change("Idle");
    }

    // 숫자 버튼 함수
    // 버튼을 누르면 누른 오브젝트를 이용하여 값을 전달한다.
    public void Number_button()
    {
        // 눌렀던 버튼의 이름을 저장
        string Name = EventSystem.current.currentSelectedGameObject.name;
    }

    // 해상도 설정 함수
    private void SetResoultion()
    {
        int setW = 1080;
        int setH = 1920;

        int deviceW = Screen.width;
        int deviceH = Screen.height;

        Screen.SetResolution(setW, (int)((float)deviceH / deviceW) * setW, true);

        // 기기의 해상도 비가 더 클 경우
        if ((float)setW / setH < (float)deviceW / deviceH)
        {
            float newW = ((float)setW / setH) / ((float)deviceW / deviceH);
            Camera.main.rect = new Rect((1f - newW) / 2f, 0f, newW, 1f);
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newH = ((float)deviceW / deviceH) / ((float)setW / setH);
            Camera.main.rect = new Rect(0f, (1f - newH) / 2f, 1f, newH);
        }
    }
}
