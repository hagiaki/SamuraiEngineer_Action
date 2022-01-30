using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(0, 0, 0);
        //Vector3 forwardDirection = transform.forward;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection.z = 5;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection.z = -5;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection.x = 5;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection.x = -5;
        }
        moveDirection.Normalize();

        Vector3 rotateDirecton = transform.position - PlayerPos;

        if (rotateDirecton.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(rotateDirecton);
        }

        moveDirection *= speed * Time.deltaTime;
        transform.position += moveDirection;

    }
}
