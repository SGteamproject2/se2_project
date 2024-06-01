using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��ȭ ��ũ��Ʈ
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

    // ��ȭ�� ����� ��縦 �����Ͽ� �ٽ� �����ϴ� �Լ�
    void Con_String(List<Dictionary<string, object>> data)
    {
        Debug.Log("�۵� ����");
        for(int i = 0; i < data.Count; i++)
        {
            if((string)data[i]["NPC"] == Player.Instance.return_name())
            {
                Debug.Log("����");
                sentences.Enqueue((string)data[i]["Text"]);
            }
        }
    }

    // ��ȭ�� ���۵ɽ� ȣ��Ǵ� �Լ�
    public void Start_Dialog()
    {
        UIManager.Instance.Dialog_Panel.gameObject.SetActive(true);
        sentences.Clear();
        Con_String(DataBase.con_Data(DataBase.Kind.Sentence));

        Nextsentence();
    }

    // ���� �������� �Ѿ��
    // coroutine�� ����Ͽ� Ÿ���� ȿ�� ����
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

    // ������ �ڷ�ƾ �Լ�
    // Ÿ���� ȿ���� �ش�.
    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";

        // ���ڸ� �ϳ��� �߰��Ͽ� Ÿ���� ȿ���� �ش�.
        foreach(char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(Wait_time);
        }
    }

    // ��ȭ�� ������ ȣ��Ǵ� �Լ�
    void EndDialog()
    {
        QuizManager.Instance.Quiz_Start();
        Player.Instance.State_Change("Idle");
    }
}
