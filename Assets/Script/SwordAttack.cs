using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : Action
{
    [SerializeField] private float _attackTime, _rotationTime;
    [SerializeField] private Transform _hand, _endPosition;
    

    public override void StartAction(Transform armStart, ArmPosition position)
    {
        base.StartAction(armStart,position);
        StartCoroutine(MovingSword());
        StartCoroutine(RotatingSword());
    }

    IEnumerator RotatingSword()
    {
        float t = 0;
        while (t <= _rotationTime)
        {
            t += Time.deltaTime;
            _hand.localRotation = Quaternion.Lerp(ArmStart.localRotation, _endPosition.localRotation, t / _rotationTime);
            yield return null;
        }
    }

    IEnumerator MovingSword()
    {
        float t = 0;
        Vector3 endPosition = new Vector3(_endPosition.localPosition.x, ArmStart.localPosition.y, _endPosition.localPosition.z);

        while (t <= _attackTime)
        {
            t += Time.deltaTime;
            float progress = Mathf.Clamp01(t / _attackTime);
            float exponentialValue = Mathf.Pow(progress, 2);
            _hand.localPosition = Vector3.Lerp(ArmStart.localPosition, endPosition, exponentialValue);
            yield return null;
        }
        EndingAction();

    }

}
