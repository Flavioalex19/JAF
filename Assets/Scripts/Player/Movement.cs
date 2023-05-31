using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] float _stationaryTurnSpeed;
    [SerializeField] float _movingTurnSpeed;
    [SerializeField] float _characterSpeed;

    [Header("Dash Variables")]
    [SerializeField] float _dashDistance;
    [SerializeField] float _dashDuration;
    [SerializeField] float _dashCooldown;
    [SerializeField] float _stopDuration;

    float _forwardAmount;
    float _turnAmount;
    Vector3 groundNormal;

    #region Dash Variables
    bool _canDash = true;
    [SerializeField] bool _isDashing = false;
    bool _dashRequested = false;
    Vector3 _dashDirection;
    float _dashStartTime;
    float _stopStartTime;
    Vector3 _previousVelocity;
    #endregion

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_canDash && Input.GetKeyDown(KeyCode.Space))
        {
            //_dashRequested = true;
        }
    }

    private void FixedUpdate()
    {
        if (_dashRequested)
        {
            Dash();
            _dashRequested = false;
        }

        if (_isDashing)
        {
            DashProgress();
        }
        else if (!_canDash)
        {
            DashStopProgress();
        }
    }

    #region Get & Set
    public float GetForwardAmount()
    {
        return _forwardAmount;
    }
    public bool GetCanDash()
    {
        return _canDash;
    }
    public void SetCanDash(bool canDash)
    {
        _canDash = canDash;
    }
    public bool GetDashRequest()
    {
        return _dashRequested;
    }
    public void SetDashRequested(bool dashRequested)
    {
        _dashRequested = dashRequested;
    }
    #endregion

    public void Move(Vector3 move, bool isMoving, bool isSprinting)
    {
        isMoving = true;
        if (!_isDashing)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, groundNormal);
            _turnAmount = Mathf.Atan2(move.x, move.z);
            _forwardAmount = move.z;

            ApplyExtraTurnRotation();

            if (isSprinting) _forwardAmount = _forwardAmount * 2;

            transform.position += transform.forward * Time.deltaTime * _forwardAmount *1.5f;
        }

    }

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(_stationaryTurnSpeed, _movingTurnSpeed, _forwardAmount);
        transform.Rotate(0, _turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    private void Dash()
    {
        if (!_isDashing)
        {
            _isDashing = true;
            _canDash = false;

            _dashDirection = transform.forward;
            _dashStartTime = Time.fixedTime;

            Invoke(nameof(EnableDash), _dashCooldown);
        }
    }

    void DashProgress()
    {
       
        float dashProgress = (Time.fixedTime - _dashStartTime) / _dashDuration;
        float remainingDistance = _dashDistance * (1f - dashProgress);
        float modifiedSpeed = remainingDistance / Time.deltaTime;

        rb.velocity = _dashDirection * modifiedSpeed;

        if (dashProgress >= 1f)
        {
            _isDashing = false;
            _previousVelocity = rb.velocity;
            _stopStartTime = Time.fixedTime;
            Invoke(nameof(DashStopProgress), _stopDuration);
        }

    }

    void DashStopProgress()
    {
        float stopProgress = (Time.fixedTime - _stopStartTime) / _stopDuration;
        rb.velocity = Vector3.Lerp(_previousVelocity, Vector3.zero, stopProgress);

        if (stopProgress >= 1f)
        {
            print("Here");
            rb.velocity = Vector3.zero; // Stop the character completely
            _canDash = true;
        }
    }

    private void EnableDash()
    {
        _canDash = true;
    }
}
