using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jump = 10.0f;
    Animator PlayerAnimator;
    private float distance = 0.1f;
    protected float gravity = 1.0f;
    //private bool isGrounded = false;
    // Start is called before the first frame update
    void Start()
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation;
        PlayerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        Vector3 jumpDirection = new Vector3(0, 0, 0);

        Vector3 rayPosition = transform.position + new Vector3(0.0f, 0.6f, 0.0f);
        RaycastHit hitinfo;
        Ray ray = new Ray(rayPosition, Vector3.down);
        bool isGround = Physics.Raycast(ray, out hitinfo);
        distance = hitinfo.distance;
        transform.position = hitinfo.point;//Ú’n‚µ‚Ä‚¢‚éŠÔ‚ÍY‚Ì‚ÝB
        Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        //Vector3 forwardDirection = transform.forward;

        //isGrounded = Physics.Raycast(gameObject.transform.position + 0.1f * gameObject.transform.up, -gameObject.transform.up, 0.15f);
        //Debug.DrawRay(gameObject.transform.position + 0.1f * gameObject.transform.up, -0.15f * gameObject.transform.up, Color.blue);

        /*if (jumpDirection.y > 0)
        {
            jumpDirection.y -= 1 * Time.deltaTime;
        }*/

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpDirection.y = jump * Time.deltaTime;
            PlayerAnimator.SetBool("JUMP", true);
        }
        else
        {
            
            PlayerAnimator.SetBool("JUMP", false);
        }

        /*if (isGround == false)
        {
            jumpDirection.y -= 1 * Time.deltaTime;
        }*/

        moveDirection.Normalize();//³‹K‰»
        jumpDirection.Normalize();

        //Vector3 rotateDirecton = transform.position - PlayerPos;

        if (moveDirection.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            PlayerAnimator.SetBool("RUNNING", true);
        }
        else
        {
            PlayerAnimator.SetBool("RUNNING", false);
        }

        moveDirection *= speed * Time.deltaTime;
        jumpDirection *= jump * Time.deltaTime;
        transform.position += moveDirection;
        transform.position += jumpDirection;
        Debug.Log(isGround);
    }
}
