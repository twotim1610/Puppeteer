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
    [SerializeField] private Transform _target;

    private Vector2 _movementInput;
    private Vector2 _rotationInput;

    private ArmPosition _rightArmPosition, _leftArmPosition;

    [Header("ArmMovement")] 
    [SerializeField] private float _radius;

    [SerializeField] private float _armSpeed;

    private Vector3 _leftStart, _rightStart, _controlingStart;
    private Transform _controlingArm;
    void Start()
    {
        _leftStart = new Vector3(_leftArm.localPosition.x / 2, _leftArm.localPosition.y, _leftArm.localPosition.z);
        _rightStart = new Vector3(_rightArm.localPosition.x / 2, _rightArm.localPosition.y, _rightArm.localPosition.z);
    }
    void Update()
    {
        
       LookAtTarget();

      //  BringArmInPosition(_leftArm, _leftArmPosition,_leftRest,_leftMiddle, _leftHigh);
      //  BringArmInPosition(_rightArm, _rightArmPosition, _rightRest, _rightMiddle, _rightHigh);
      //  ResettingArms();
      MovingArm();
    }

    private void MovingArm()
    {
        if (_controlingArm != null)
        {
            _controlingArm.localPosition += new Vector3(_rotationInput.x, 0, _rotationInput.y) * Time.deltaTime* _armSpeed;

            Vector3 distance = _controlingArm.localPosition - _controlingStart;
            if (distance.magnitude > _radius)
            {
                _controlingArm.localPosition = _controlingStart + (distance.normalized*_radius);
            }
        }

       

    }

    void FixedUpdate()
    {
        Moving();
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

    private void LookAtTarget()
    {
        Vector3 lookDirection = _target.position - transform.position;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void Moving()
    {

        Vector3 movement = new Vector3(_movementInput.x, 0, _movementInput.y);
        movement =  transform.localToWorldMatrix * movement;

        _controller.Move(movement * Time.deltaTime * _movementSpeed);
    }

    public void Move(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        
        _rotationInput = context.ReadValue<Vector2>();
        _rotationInput = _rotationInput.normalized;
    }

    private float _leftTimeSincePress;
    public void Left(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _controlingArm = _leftArm;
            _controlingStart = _leftStart;
            //  _leftTimeSincePress = 0;
            //  _leftArmPosition = _leftArmPosition == ArmPosition.Rest ? ArmPosition.Middle : ArmPosition.High;
        }
    }
    private float _rightTimeSincePress;
    public void Right(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _controlingArm = _rightArm;
            _controlingStart = _rightStart;
          //  _rightTimeSincePress = 0;
          //  _rightArmPosition = _rightArmPosition == ArmPosition.Rest ? ArmPosition.Middle : ArmPosition.High;
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
