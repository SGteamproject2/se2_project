using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UI�� �����ϴ� ��ũ��Ʈ
// ���� ��� : ��ư�� ���, UI �г� ������ Ȱ��ȭ ���
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

    // ��Ȳ�� ���� �г� ������Ʈ�� Ȱ��ȭ �����ִ� �Լ�
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

    // ������ �̵�
    public void Right_Move()
    {
        Button_State = "Right";
        Button_Press = true;
        Player.Instance.State_Change("Move");
    }

    // ���� �̵�
    public void Left_Move()
    {
        Button_State = "Left";
        Button_Press = true;
        Player.Instance.State_Change("Move");
    }

    // ��ư ���� �ʱ�ȭ
    public void Reset_State()
    {
        Button_State = null;
        Button_Press = false;
        Player.Instance.State_Change("Idle");
    }

    // ���� ��ư �Լ�
    // ��ư�� ������ ���� ������Ʈ�� �̿��Ͽ� ���� �����Ѵ�.
    public void Number_button()
    {
        // ������ ��ư�� �̸��� ����
        string Name = EventSystem.current.currentSelectedGameObject.name;
    }

    // �ػ� ���� �Լ�
    private void SetResoultion()
    {
        int setW = 1080;
        int setH = 1920;

        int deviceW = Screen.width;
        int deviceH = Screen.height;

        Screen.SetResolution(setW, (int)((float)deviceH / deviceW) * setW, true);

        // ����� �ػ� �� �� Ŭ ���
        if ((float)setW / setH < (float)deviceW / deviceH)
        {
            float newW = ((float)setW / setH) / ((float)deviceW / deviceH);
            Camera.main.rect = new Rect((1f - newW) / 2f, 0f, newW, 1f);
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newH = ((float)deviceW / deviceH) / ((float)setW / setH);
            Camera.main.rect = new Rect(0f, (1f - newH) / 2f, 1f, newH);
        }
    }
}
