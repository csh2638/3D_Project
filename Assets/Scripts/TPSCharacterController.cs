using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{

    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;

    
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = characterBody.GetComponent<Animator>();
    }

    // Update is called once per frame
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

            characterBody.forward = moveDir;

            //transform.position += moveDir * Time.deltaTime * 5f;
            transform.position += Vector3.ClampMagnitude(moveDir, 1f) * Time.deltaTime * 5f;
            Debug.Log(moveDir);
        }
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);

        float speed = (((transform.position - m_LastPosition).magnitude) / Time.deltaTime);
        m_LastPosition = transform.position;
        //Debug.Log(speed);
    }

    private void Move2()
    {
        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);


        Vector3 lookFoward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
        Debug.Log(lookFoward);

        Vector3 direction = lookFoward;
        // 정면을 기준으로 한다면 transform.forward; 를 입렵하면 된다.

        var quaternion = Quaternion.Euler(0, -90, 0);
        Vector3 newDirection = quaternion * direction;


        characterBody.forward = lookFoward;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.ClampMagnitude(lookFoward, 1f) * Time.deltaTime * 5f;
            //transform.position += Vector3.forward * Time.deltaTime * 5f;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (Vector3.forward).normalized * 1.2f * Time.deltaTime * 5f;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * 5f;
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (Vector3.back).normalized * 1.2f * Time.deltaTime * 5f;
            }
        }

        
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += newDirection * Time.deltaTime * 5f;
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (Vector3.left).normalized * 1.2f * Time.deltaTime * 5f;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * 5f;
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += (Vector3.right).normalized * 1.2f * Time.deltaTime * 5f;
            }
        }
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
