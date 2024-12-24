using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuppetControler : MonoBehaviour
{
    
    [SerializeField] private Transform _leftArm, _rightArm, _head, _feet;

    [Header("ArmReferences")] 
    [SerializeField] private float _armMoveSpeed;

    [SerializeField] private float _armRotationSpeed, _armUpTime;

    [SerializeField] private Transform _leftRest, _leftMiddle, _leftHigh;
    [SerializeField] private Transform _rightRest, _rightMiddle, _rightHigh;



    [Header("Ducking")]
    [SerializeField] private float _duckDistance;
    [SerializeField] private float _duckingTime, _duckingSpeed;
    private bool _isDucked;

    [Header("Jumping")] [SerializeField] private float _jumbDistance;
    [SerializeField] private float _jumbTime, _jumbSpeed;
    private bool _isJumping;

    [Header("Movement")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _movementSpeed, _rotationSpeed;


    private Vector2 _movementInput;
    private Vector2 _forwardDirection;

    private ArmPosition _rightArmPosition, _leftArmPosition;
    void Start()
    {
    }
    void Update()
    {
        Moving();
        Rotating();
        BringArmInPosition(_leftArm, _leftArmPosition,_leftRest,_leftMiddle, _leftHigh);
        BringArmInPosition(_rightArm, _rightArmPosition, _rightRest, _rightMiddle, _rightHigh);
        ResettingArms();
    }

    private void ResettingArms()
    {
        _leftTimeSincePress += Time.deltaTime;
        _rightTimeSincePress += Time.deltaTime;

        if (_leftTimeSincePress >= _armUpTime)
        {
            _leftArmPosition = ArmPosition.Rest;
        }

        if (_rightTimeSincePress >= _armUpTime)
        {
            _rightArmPosition = ArmPosition.Rest;
        }
        
    }

    private void BringArmInPosition(Transform arm, ArmPosition armPosition, Transform rest, Transform middle, Transform high)
    {
        Transform newTransform = new RectTransform();
        switch (armPosition)
        {
            case ArmPosition.Rest:
                newTransform = rest;
                break;
            case ArmPosition.Middle: 
                newTransform = middle;
                break;
            case ArmPosition.High:
                newTransform = high;
                break;
        }
            arm.position = Vector3.MoveTowards(arm.position, newTransform.position, _armMoveSpeed * Time.deltaTime);
            arm.rotation = Quaternion.RotateTowards(arm.rotation, newTransform.rotation, _armRotationSpeed * Time.deltaTime);


    }

    private void Rotating()
    {
        if (_forwardDirection != Vector2.zero)
        {
            Vector3 lookdirection = new Vector3(_forwardDirection.x, 0, _forwardDirection.y);
            Quaternion targetRotation = Quaternion.LookRotation(lookdirection);

            // Use Quaternion.RotateTowards for smooth and shortest path rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        }
    }


    private void Moving()
    {
        Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y);
        _controller.Move(movement * Time.deltaTime * _movementSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        
        _forwardDirection = context.ReadValue<Vector2>();
        _forwardDirection = _forwardDirection.normalized;
    }

    private float _leftTimeSincePress;
    public void Left(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _leftTimeSincePress = 0;
            _leftArmPosition = _leftArmPosition == ArmPosition.Rest ? ArmPosition.Middle : ArmPosition.High;
        }
    }
    private float _rightTimeSincePress;
    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rightTimeSincePress = 0;
            _rightArmPosition = _rightArmPosition == ArmPosition.Rest ? ArmPosition.Middle : ArmPosition.High;
        }
    }
    public void Top(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_isDucked)
            {
                StartCoroutine(Ducking());
                _isDucked = true;
            }
        }
    }

    IEnumerator Ducking()
    {
        float ellapsedTime = 0;
        Vector3 start = _head.localPosition;
        Vector3 end = _head.localPosition - Vector3.down * _duckDistance;

        while (ellapsedTime < _duckingTime)
        {
            ellapsedTime += Time.deltaTime;
            _head.localPosition = Vector3.Lerp(start, end, ellapsedTime * _duckingSpeed);
            yield return null;
        }

        ellapsedTime = 0;
        while (_head.localPosition != start)
        {
            ellapsedTime += Time.deltaTime;
            _head.localPosition = Vector3.Lerp(end, start, ellapsedTime * _duckingSpeed);
            yield return null;
        }

        _isDucked = false;

    }

    public void Down(InputAction.CallbackContext context)
    {
         if (context.performed)
         {
             if (!_isJumping)
             {
                StartCoroutine(Jumping());
                _isJumping = true;
            }
         }
    }
    IEnumerator Jumping()
    {
        float ellapsedTime = 0;
        Vector3 start = _feet.localPosition;
        Vector3 end = _feet.localPosition - Vector3.up * _jumbDistance;

        while (ellapsedTime < _duckingTime)
        {
            ellapsedTime += Time.deltaTime;
            _feet.localPosition = Vector3.Lerp(start, end, ellapsedTime * _duckingSpeed);
            yield return null;
        }

        ellapsedTime = 0;
        while (_feet.localPosition != start)
        {
            ellapsedTime += Time.deltaTime;
            _feet.localPosition = Vector3.Lerp(end, start, ellapsedTime * _duckingSpeed);
            yield return null;
        }

        _isJumping = false;

    }

}

public enum ArmPosition
{
    Rest,
    Middle,
    High
}
