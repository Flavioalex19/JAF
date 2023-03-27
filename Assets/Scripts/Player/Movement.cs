using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField]float _stationaryTurnSpeed;
    [SerializeField]float _movingTurnSpeed;

    //Private Variables
    float _forwardAmount;
    float _turnAmount;
    float _characterSpeed;
    Vector3 _groundNormal;

    public void Move(Vector3 move)
    {
        // convert the world relative moveInput vector into a local-relative
        // turn amount and forward amount required to head in the desired
        // direction.
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, _groundNormal);
        _turnAmount = Mathf.Atan2(move.x, move.z);
        _forwardAmount = move.z;

        ApplyExtraTurnRotation();
        //if forwardAmount > .5 the characterSpeed is 7
        if (_forwardAmount > .5)
        {
            _characterSpeed = 7;
        }
        else
        {
            _characterSpeed = 3;
        }
        transform.position += transform.forward * Time.deltaTime * _forwardAmount * _characterSpeed;
    }
    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
        transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
    }
}
