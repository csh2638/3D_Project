using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	float _speed = 5.0f;

	public enum PlayerState
	{
		Idle,
		Walking,
		Running,
		
	}

	PlayerState _state = PlayerState.Idle;

	void Start()
	{
		Managers.Input.KeyAction -= OnKeyboard;
		Managers.Input.KeyAction += OnKeyboard;
	}

	Animator anim;
	private void Awake()
    {
		anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
	{
        switch (_state)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Walking:
                UpdateWalking();
                break;
            case PlayerState.Running:
				UpdateRunning();
				break;
        }

        _state = PlayerState.Idle;
    }

    void UpdateIdle()
	{
		anim.SetBool("IsWalking", false);
		anim.SetBool("IsRunning", false);
	}

	void UpdateWalking()
    {
		anim.SetBool("IsWalking", true);
	}

	void UpdateRunning()
	{
		anim.SetBool("IsRunning", true);
	}

	void OnKeyboard()
	{

		
		
		if (Input.GetKey(KeyCode.W))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
			transform.position += Vector3.forward * Time.deltaTime * _speed;
			_state = PlayerState.Walking;
			if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            {
				transform.position += (Vector3.forward).normalized * 1.2f * Time.deltaTime * _speed;
				_state = PlayerState.Running;
			}
		}

		if (Input.GetKey(KeyCode.S))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
			transform.position += Vector3.back * Time.deltaTime * _speed;
			_state = PlayerState.Walking;
			if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
			{
				transform.position += (Vector3.back).normalized * 1.2f * Time.deltaTime * _speed;
				_state = PlayerState.Running;
			}
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
			transform.position += Vector3.left * Time.deltaTime * _speed;
			_state = PlayerState.Walking;
			if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
			{
				transform.position += (Vector3.left).normalized * 1.2f * Time.deltaTime * _speed;
				_state = PlayerState.Running;
			}
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
			transform.position += Vector3.right * Time.deltaTime * _speed;
			_state = PlayerState.Walking;
			if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
			{
				transform.position += (Vector3.right).normalized * 1.2f * Time.deltaTime * _speed;
				_state = PlayerState.Running;
			}
		}
		
	}
}	
	