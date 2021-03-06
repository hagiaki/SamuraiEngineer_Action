using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 5.0f;//移動速度
    protected float jump = 7.5f;//ジャンプ速度
    Animator PlayerAnimator;//アニメーション
    private float distance = 0.7f;//raycastの長さ
    protected float gravity = 9.8f;//重力
    protected float gravityAcceleration = 0.0f;//重力加速度
    protected bool isGround = false;//地面判定
    protected bool isWall = true;//壁判定

    const float charaDistance=0.2f;//後から変更されない

    protected enum STATE//キャラクターの状態
    {
        WAIT,
        RUN,
        JUMP
    }

    protected STATE myState;


    // Start is called before the first frame update
    void Start()
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerAnimator = GetComponent<Animator>();
        myState = STATE.WAIT;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = GetMoveInput();
        Vector3 jumpDirection = new Vector3(0, 0, 0);
        Vector3 CalcMoveDirection = moveDirection * speed * Time.deltaTime;
        var direction = transform.forward * 1.0f;
        
        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.7f, 0.0f);
        RaycastHit hitinfo, hitinfoFront;
        Ray ray = new Ray(rayPosition, Vector3.down);
        Physics.Raycast(ray, out hitinfo);
        Ray frontray = new Ray(rayPosition, CalcMoveDirection);//進行方向

        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.green);
        Debug.DrawRay(rayPosition, direction * distance, Color.red);
        
        CheckGroundLanding(hitinfo);//床判定
        hitinfoFront = CheckWallLanding(frontray);
        Vector3 AlongWallVec = CalcMoveDirection - Vector3.Dot(CalcMoveDirection, hitinfoFront.normal.normalized) * hitinfoFront.normal.normalized;
        ChangeJump();

        if (myState == STATE.JUMP)
        {
            jumpDirection.y = jump;
        }

        moveDirection.Normalize();//正規化
        CheckRunning(moveDirection);

        moveDirection *= speed * Time.deltaTime;

        if (isWall)
        {
            moveDirection = AlongWallVec;
            //Debug.Log(AlongWallVec);
        }

        jumpDirection.y -= gravityAcceleration;
        transform.position += moveDirection;
        if (!isGround)
        {
            transform.position += jumpDirection * Time.deltaTime;
        }

        LandingFixedPositionY(hitinfo);
        //Debug.Log(isWall);

    }

    Vector3 GetMoveInput()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection.z = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection.z = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection.x = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection.x = -1;
        }
        return moveDirection;
    }

    void ChangeJump()//ジャンプ
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            myState = STATE.JUMP;
            isGround = false;
            PlayerAnimator.SetBool("JUMP", true);
        }
    }

    void CheckGroundLanding(RaycastHit hitinfo)//重力判定
    {
        if (hitinfo.distance < distance)
        {
            gravityAcceleration = 0;
            isGround = true;
            PlayerAnimator.SetBool("JUMP", false);
            myState = STATE.WAIT;
        }
        else
        {
            gravityAcceleration += gravity * Time.deltaTime;
            isGround = false;
        }
    }

    RaycastHit CheckWallLanding(Ray frontray)//壁判定
    {
        RaycastHit raycastHit;
        int StageLayer = 1 << 7;
        isWall = false;

        if (Physics.Raycast(frontray, out raycastHit, 100, StageLayer)) 
        {
            if (raycastHit.distance < charaDistance)
            {
                isWall = true;
            }
        }
        return raycastHit;
    }

    void CheckRunning(Vector3 moveDirection)//ダッシュ・方向切替判定
    {

        if (moveDirection.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            PlayerAnimator.SetBool("RUNNING", true);
        }
        else
        {
            PlayerAnimator.SetBool("RUNNING", false);
        }
    }

    void LandingFixedPositionY(RaycastHit hitinfo)
    {
        if (isGround)
        {
            Vector3 Baseposition = transform.position;//まず全てをコピー
            Baseposition.y = hitinfo.point.y;//上からyだけを抽出
            transform.position = Baseposition;
        }
    }
}
