using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    protected float jump = 7.5f;
    Animator PlayerAnimator;
    private float distance = 0.7f;
    protected float gravity = 9.8f;
    protected float gravityAcceleration = 0.0f;
    protected bool isGround = false;
    protected bool isWall = true;

    protected enum STATE
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
        var direction = transform.forward;

        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.7f, 0.0f);
        RaycastHit hitinfo, hitinfoFront;
        Ray ray = new Ray(rayPosition, Vector3.down);
        Physics.Raycast(ray, out hitinfo);
        Ray frontray = new Ray(rayPosition, direction);
        Physics.Raycast(frontray, out hitinfoFront);

        //transform.position = hitinfo.point;//接地している間はYのみ。
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.green);
        Debug.DrawRay(rayPosition, direction * distance, Color.red);

        CheckGroundLanding(ref hitinfo);
        CheckWallLanding(frontray, ref hitinfoFront);

        ChangeJump();

        if (myState == STATE.JUMP)
        {
            jumpDirection.y = jump;
        }

        moveDirection.Normalize();//正規化

        //Vector3 rotateDirecton = transform.position - PlayerPos;

        CheckRunning(moveDirection);

        //hitinfo.normal

        moveDirection *= speed * Time.deltaTime;
        jumpDirection.y -= gravityAcceleration;
        transform.position += moveDirection;
        transform.position += jumpDirection * Time.deltaTime;
        
        LandingFixedPositionY(ref hitinfo);
        Debug.Log(isWall);
    }

    Vector3 GetMoveInput()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.UpArrow) && isWall)
        {
            moveDirection.z = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow) && isWall)
        {
            moveDirection.z = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow) && isWall)
        {
            moveDirection.x = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && isWall)
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

    void CheckGroundLanding(ref RaycastHit hitinfo)//重力判定
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

    void CheckWallLanding(Ray frontray,ref RaycastHit hitinfoFront)//壁判定
    {
        if (Physics.Raycast(frontray, out hitinfoFront))
        {
            isWall = false;
        }
        else
        {
            isWall = true;
        }
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

    void LandingFixedPositionY(ref RaycastHit hitinfo)
    {
        if (isGround)
        {
            Vector3 Baseposition = transform.position;//まず全てをコピー
            Baseposition.y = hitinfo.point.y;//上からyだけを抽出
            transform.position = Baseposition;
        }
    }
}
