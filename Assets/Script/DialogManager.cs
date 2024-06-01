using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 대화 스크립트
public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public float Wait_time;
    public Text dialogText;
    private Queue<string> sentences = new Queue<string>();

    // 대화에 사용할 대사를 구분하여 다시 저장하는 함수
    void Con_String(List<Dictionary<string, object>> data)
    {
        Debug.Log("작동 시작");
        for(int i = 0; i < data.Count; i++)
        {
            if((string)data[i]["NPC"] == Player.Instance.return_name())
            {
                Debug.Log("저장");
                sentences.Enqueue((string)data[i]["Text"]);
            }
        }
    }

    // 대화가 시작될시 호출되는 함수
    public void Start_Dialog()
    {
        UIManager.Instance.Dialog_Panel.gameObject.SetActive(true);
        sentences.Clear();
        Con_String(DataBase.con_Data(DataBase.Kind.Sentence));

        Nextsentence();
    }

    // 다음 문장으로 넘어가기
    // coroutine을 사용하여 타이핑 효과 구현
    public void Nextsentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // 구현된 코루틴 함수
    // 타이핑 효과를 준다.
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        // 글자를 하나씩 추가하여 타이핑 효과를 준다.
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(Wait_time);
        }
    }

    // 대화가 끝날때 호출되는 함수
    void EndDialog()
    {
        QuizManager.Instance.Quiz_Start();
        Player.Instance.State_Change("Idle");
    }
}
