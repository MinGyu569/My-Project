using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text talkText;
    public GameObject scanObject;
    public Image portraitImg;
    public GameObject talkPanel;
    public TalkManager TalkManager;
    public bool isAction;   // 기본값은 false
    public int talkIndex;
    public string talkData;

    public void Action(GameObject scanObj)
    {
        // 안전장치: 대화 시작하려는데 전달받은 오브젝트가 없으면 중단
        if (scanObj == null)
        {
            return;
        }
        scanObject = scanObj;
        // 오브젝트 데이터 사용 등록
        ObjData objData = scanObj.GetComponent<ObjData>();

        // 대화 진행 (다음 대사를 가져옴)
        Talk(objData.id, objData.isNpc);

        // Talk()의 결과(isAction)에 따라 패널을 켜고 끎
        talkPanel.SetActive(isAction);
    }


    /*
    플레이어가 어떤 오브젝트를 조사하면, 
    그 오브젝트의 이름표(ObjData)를 슥 읽어봅니다. 
    그리고 대본 도서관(TalkManager)으로 달려가서 
    "여기 2000번 대본에서 0번째(혹은 1번째) 대사 좀 꺼내줘!"라고 요청한 뒤, 
    받아온 대사를 화면(텍스트 UI)에 띄워줍니다.
    */
    void Talk(int id, bool isNpc)
    {
        // 대본 도서관에서 현재 인덱스의 대사를 가져옴
        talkData = TalkManager.GetTalk(id, talkIndex);

        // [중요] 더 이상 가져올 대사가 없다면 (끝에 도달했다면)
        if (talkData == null)
        {
            isAction = false;// 대화 상태 해제 (패널이 꺼지게 됨)
            talkIndex = 0;  // ★ 다음 대화를 위해 인덱스를 꼭 0으로 리셋!
            return;
        }

        // 대사가 남아있다면 출력
        if (isNpc)
        {
            // 사람 데이터
            // '/'이걸로 나눈 문자열의 0번째 대사 보여주기
            talkText.text = talkData.Split('/')[0];

            // '/'이걸로 나눈 1문자열의 문자를 숫자 int형으로 바꿔서 전달
            portraitImg.sprite = TalkManager.GetPortrait(id,int.Parse(talkData.Split('/')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }
        // 사람 아니면
        else
        {
            // 오브젝트 데이터
            talkText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;    // 다음 스페이스바를 누를 때 다음 대사가 나오도록 번호 증가
    }

}
