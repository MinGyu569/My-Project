using System.Collections.Generic;
using UnityEngine;
/*
 모든 대사집을 보관하는 책방입니다.
책장(Dictionary)에 2000번 서랍을 열면 ["안녕?", "이 곳은 처음이구나."]라는
대사 리스트가 들어있습니다.
 */

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;
    public Sprite[] porArr;

    private void Awake()
    {
        // 오브젝트 사전 정의
        talkData = new Dictionary<int, string[]>();

        portraitData = new Dictionary<int, Sprite>();
        // 사전 펼치기
        GenerateData();
    }

    void GenerateData()
    {
        // 사전 내용 작성
        talkData.Add(2000, new string[] { "안녕?/0", "이 곳은 처음이구나./1" });
        talkData.Add(1000, new string[] { "응../1", "혼자있고싶어../0" });
        talkData.Add(100, new string[] { "이것은 무슨 박스지?" });
        talkData.Add(200, new string[] { "뭐가 적혀있는거지." });

        // 2000번의 + ?번째, 스프라이트는 이 이미지를 저장
        portraitData.Add(2000 + 0, porArr[0]);
        portraitData.Add(2000 + 1, porArr[1]);
        portraitData.Add(2000 + 2, porArr[2]);
        portraitData.Add(2000 + 3, porArr[3]);

        portraitData.Add(1000 + 0, porArr[4]);
        portraitData.Add(1000 + 1, porArr[5]);
        portraitData.Add(1000 + 2, porArr[6]);
        portraitData.Add(1000 + 3, porArr[7]);



    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        // 외부에 사전 내용 전달
        return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        // ? 아이디의 + ? 번째 스프라이트를 받아서 전달
        return portraitData[id +  portraitIndex];
    }
}
