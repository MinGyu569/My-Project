using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerAction : MonoBehaviour
{
    public float speed;
    Vector2 inputVec;
    Vector2 nextMove;
    GameObject scanObject;
    Vector3 dir;

    public GameManager manager;
    Rigidbody2D rigid;
    Animator ani;

    //입력 축 방향 저장할 변수
    enum MoveAxis
    {
        None,   //입력 없음
        Horizontal, // x
        Vertical    // y
    }
    MoveAxis currentAxis = MoveAxis.None;   //현재 축 기본 상태

    

    private void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    
    private void FixedUpdate()
    {
        // 플레이어 이동
        rigid.linearVelocity = nextMove.normalized * speed;

        //레이
        Debug.DrawRay(rigid.position, dir*0.7f,new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dir , 0.7f, LayerMask.GetMask("Object"));
        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject=null;
        }
    }



    private void Update()
    {
        // 스페이스를 눌렀을 때
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // 1. 이미 대화 중이거나(대화를 넘겨야 하니까) 
            // 2. 대화 중은 아니지만 눈앞에 스캔된 오브젝트가 있을 때만 실행
            if (manager.isAction || scanObject !=null) 
            {
                // 현재 스캔 중인 대상을 넘겨줍니다. 
                // (이미 대화 중일 때는 scanObject가 잠깐 다른 곳을 보더라도
                // 기존 대화를 이어가기 위해 scanObject 대신 대화 중인
                // 대상을 기억해둘 필요가 있지만, 지금은 구조상 scanObject를 그대로 전달합니다)
                manager.Action(scanObject); 
            }
            else if (scanObject != null)    // 스캔된 오브젝트가 있으면
            {
                manager.Action(scanObject); // 오브젝트 정보 전달
            }
        }

        // 애니 움직임과 이동 방향
        HandleInput();

        // 레이 쏘는 방향
        if (inputVec.x < 0)
        {
            dir = Vector3.left;
        }
        else if (inputVec.x > 0)
        {
            dir = Vector3.right;
        }
        else if (inputVec.y < 0)
        {
            dir = Vector3.down;
        }
        else if (inputVec.y > 0)
        {
            dir = Vector3.up;
        }

        

    }

    // 입력값 축 결정과 축 바뀔때 애니메이션에 갱신된 값 전달
    private void HandleInput()
    {
        // 대화창 켜져있으면 움직임 제한
        if (manager.isAction)
        {
            nextMove = Vector2.zero;
        }
        else
        {
            nextMove = inputVec;
        }

        // 현재 축 상태가 정해지지 않았다면
        if (currentAxis == MoveAxis.None)
        {
            if (nextMove.x != 0) //처음 입력을 x축으로 할 때
            {
                SetMoveAxis(MoveAxis.Horizontal, (int)nextMove.x, 0);  //현재 x축 상태
            }
            else if (nextMove.y != 0) //처음 입력을 y축으로 할 때
            {
                SetMoveAxis(MoveAxis.Vertical, 0, (int)nextMove.y);    // 현재 y축 상태
            }
        }

        // 현재 축 방향으로만 이동
        if (currentAxis == MoveAxis.Horizontal)  //x축 이동 상태일때
        {
            nextMove.y = 0;     //y축 이동 불가
            if (nextMove.x == 0)    // x축 이동 끝났을 때
            {
                SetMoveAxis(MoveAxis.None, 0, 0);    // 축 정해지지 않은 상태로 변경
            }
        }
        else if (currentAxis == MoveAxis.Vertical)
        {
            nextMove.x = 0;
            if (nextMove.y == 0)
            {
                SetMoveAxis(MoveAxis.None, 0, 0);
            }
        }
    }

    // 애니메이션, 축 상태
    private void SetMoveAxis(MoveAxis axis, int h, int v)
    {
        currentAxis = axis;
        ani.SetInteger("H", h);
        ani.SetInteger("V", v);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
}
