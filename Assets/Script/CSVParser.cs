using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVParser
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        // 데이터를 저장한 리스트 생성
        var list = new List<Dictionary<string, object>>();
        // 저장할 데이터를 유니티 Asset의 Resource파일에서 찾아서 불러온다.
        TextAsset data = Resources.Load("Datas/" + file) as TextAsset;

        // 데이터 가공 시작 //
        // 데이터의 내용중 줄바꿈을 기준으로 끊어낸다.
        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        // 만약 데이터에 분류를 제외한 내용이 존제하지 않을경우
        if (lines.Length <= 1)
            return list;

        // Header : 데이터들의 분류기준으로 사용될 열을 저장한다.
        // 예시) 몬스터, 아이템, 무기, 재료
        var header = Regex.Split(lines[0], SPLIT_RE);

        // Header에 저장된 데이터를 제외한 나머지 데이터들을 작업한다.
        for (var i = 1; i < lines.Length; i++)
        {
            // values 변수에 데이터의 내용을 SPLIT_RE를 기준으로 끊어서 저장한다.
            var values = Regex.Split(lines[i], SPLIT_RE);

            if (values.Length == 0 || values[0] == "")
                continue;

            // 데이터를 저장한 Dictionary를 생성한다.
            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];

                // value의 공백을 제거한다.
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;

                // 정보가 정수인지 실수인지 확인하여 알맞은 변수로 저장한다.
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
