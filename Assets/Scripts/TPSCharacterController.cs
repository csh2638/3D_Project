using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{

    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;


    [SerializeField]
    float _speed = 5.0f;

    Animator animator;
    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        LookAround();
        Move2();
    }

    private Vector3 m_LastPosition;
    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool isMove = moveInput.magnitude != 0;

        animator.SetBool("IsWalking", isMove);

        if(isMove)
        {
            Vector3 lookFoward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.forward.z).normalized;

            Vector3 moveDir = lookFoward * moveInput.y  + lookRight * moveInput.x;

            characterBody.forward = moveDir.normalized;

            //transform.position += moveDir * Time.deltaTime * 5f;
            transform.position += Vector3.ClampMagnitude(moveDir, 1f) * Time.deltaTime * 5f;
            Debug.Log(moveDir);
        }
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

       
    }

    bool ismoving = false;
    bool isfront = false;
    bool isback = false;
    private void Move2()
    {
        
        Vector3 lookFoward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Vector3 newDirection = new Vector3(0,0,0);
        if (Input.GetKey(KeyCode.W))
        {
            newDirection = lookFoward; // 현재 방향 기준으로 왼쪽 재정의
            ismoving = true;
            isfront = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            newDirection = -lookFoward; // 현재 방향 기준으로 왼쪽 재정의
            ismoving = true;
            isback = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            newDirection = Quaternion.Euler(0f, -90f, 0f) * lookFoward; // 현재 방향 기준으로 왼쪽 재정의
            ismoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            newDirection = Quaternion.Euler(0f, 90f, 0f) * lookFoward; // 현재 방향 기준으로 왼쪽 재정의
            ismoving = true;
        }

        //characterBody.forward = (newDirection + lookFoward).normalized;
        
        if (ismoving)
        {
            if (isfront)
            {
                characterBody.forward = (newDirection + lookFoward).normalized;
                transform.position += (newDirection + lookFoward).normalized * Time.deltaTime * _speed;
            }
            
            else if(isback)
            {
                characterBody.forward = (newDirection - lookFoward).normalized;
                transform.position += (newDirection - lookFoward).normalized * Time.deltaTime * _speed;
            }
            else
            {
                characterBody.forward = (newDirection).normalized;
                transform.position += newDirection.normalized * Time.deltaTime * _speed;
            }
            

        }
        ismoving = false;
        isback = false;
        isfront = false;



        Debug.DrawRay(cameraArm.position, lookFoward.normalized, Color.red);
        Debug.DrawRay(cameraArm.position, newDirection.normalized, Color.yellow);
        //Debug.DrawRay(cameraArm.position, (-lookFoward).normalized, Color.black);
        //Debug.DrawRay(cameraArm.position, (newDirection + lookFoward), Color.yellow);
        //Debug.DrawRay(cameraArm.position, (newDirection + (lookFoward)), Color.yellow);


    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 cameraAngle = cameraArm.rotation.eulerAngles;
        float x = cameraAngle.x - mouseDelta.y; // 회전각 제어 상하

        if(x < 180.0f)
        {
            x = Mathf.Clamp(x, -1f, 20.0f); // 0.f로 해보기
        }
        else
        {
            x = Mathf.Clamp(x, 335.0f, 361.0f);
        }
        cameraArm.rotation = Quaternion.Euler(x, cameraAngle.y + mouseDelta.x, cameraAngle.z);
    }
}
