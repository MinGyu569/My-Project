using UnityEngine;
/*
 오브젝트(NPC나 상자) 머리에 붙어있는 이름표입니다.
"내 ID는 2000번이고, 나는 NPC야" 혹은
"내 ID는 100번이고, 나는 그냥 상자(일반 오브젝트)야"라는 정보만 들고 
가만히 서 있습니다.
 */

public class ObjData : MonoBehaviour
{
    // 오브젝트 주소와 사람유무
    public int id;
    public bool isNpc;
}
