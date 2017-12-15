using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IGameObject
{
    [SerializeField]
    private Rigidbody2D rb = null;

    [SerializeField]
    private Animator anim;

    //플레이어 기본 정보
    private int hp = 1;
    private int initHp = 1;
    private float speed = 3.0f;
    private float jump = 400.0f;
    private bool jumpState = true;

    private float horizontal;
    private float vertical;

    private bool leftSite;
    private bool rightSite;



    private void Awake()
    {
        //hp 초기화
        hp = initHp;

        //초기 시작 오른쪽 향함
        rightSite = true;
    }//private void Awake() 종료



    public void GameUpdate()
    {

        //x 방향으로 가할 힘
        float xForce = horizontal * speed * Time.deltaTime;
        this.gameObject.transform.Translate(xForce, 0, 0);

        

        

    }//public void GameUpdate() 종료

    private void FixedUpdate()
    {
        //좌우 키보드 입력.
        horizontal = Input.GetAxis("Horizontal");
 

        //오른쪽으로 이동 및 애니메이션 작동.
        if (horizontal > 0)
        {
            leftSite = false;
            rightSite = true;
            anim.SetBool("IsRunRight", true);
            anim.SetBool("IsSite", true); //IsSite = true = 오른쪽 방향
        }
        else
        {
            anim.SetBool("IsRunRight", false);
        }

        //왼쪽 방향 애니메이션 작동
        if(horizontal < 0)
        {
            leftSite = true;
            rightSite = false;
            anim.SetBool("IsRunLeft", true);
            anim.SetBool("IsSite", false);
        }
        else
        {
            anim.SetBool("IsRunLeft", false);
        }
        
        

        //점프중일때
        if (!jumpState)
        {
            //달리기 애니메이션 종료
            anim.SetBool("IsRunRight", false);
            anim.SetBool("IsRunLeft", false);

            //점프 애니메이션 작동
            if (rightSite) { 
                anim.SetBool("IsJumpRight", true);
            }
            else if (leftSite)
            {
                anim.SetBool("IsJumpLeft", true);
            }
        }
        else
        {
            //점프 애니메이션 종료
            anim.SetBool("IsJumpRight", false);
            anim.SetBool("IsJumpLeft", false);
            
            //점프 버튼
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //땅에 닿으면 한번 위로 올려준다음 Add포스를 사용. 이유: 충돌 판정후에 즉각적으로 올라오지 않고 점프키를 난타하면 점프를 못할 경우 발생.
                this.gameObject.transform.Translate(0, 0.15f, 0);
                //translate 와 AddForce의 차이점 = 전자는 +1 만큼 이동 시키고,
                //후자는 +1만큼의 힘을 가한다.
                rb.AddForce(new Vector2(0, jump));
                jumpState = false;
            }
        }
    }//private void FixedUpdate() 종료

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            
            //땅에 닿아야 점프 가능
            jumpState = true;
        }
    }//private void OnTriggerEnter2D(Collider2D other) 종료


}
