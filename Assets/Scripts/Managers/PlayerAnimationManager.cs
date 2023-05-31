using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum States
{
    None,
    Idle,
    Moving
}

public class PlayerAnimationManager : MonoBehaviour
{

    public States MyStates;




    //Components
    public Animator _playerAnimator;
    PlayerInput _playerInput;
    Movement _movement;


    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerAnimator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        //MyStates = States.Idle;
        ReturnToIdle();
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyStates)
        {
            case States.Idle:
                if (_playerInput.GetIsMoving())
                {
                    MyStates = States.Moving;

                }
                break;
            case States.Moving:
                _playerAnimator.SetBool("isMoving", true);
                _playerAnimator.SetFloat("Forward", _movement.GetForwardAmount());
                if (_movement.GetForwardAmount() <= 0)
                {
                    _playerAnimator.SetBool("isMoving", false);
                    ReturnToIdle ();
                }
                break;
            default: break;
        } 

    }

    void ReturnToIdle()
    {
        MyStates = States.Idle;
        float dice;
        dice = Random.Range(0f, 1f);
        _playerAnimator.SetFloat("IdleValue", dice);
    }

   
}
