using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ ���� ��ũ��Ʈ
public class DataBase
{
    private static List<Dictionary<string, object>> Sentences_data = CSVParser.Read("Sentence");

    public enum Kind
    {
        Sentence,
        Answer
    }

    public static List<Dictionary<string,object>> con_Data(Kind kind)
    {
        switch (kind)
        {
            case Kind.Sentence:
                return Sentences_data;
            case Kind.Answer:

                break;
        }
        return null;
    }
}
