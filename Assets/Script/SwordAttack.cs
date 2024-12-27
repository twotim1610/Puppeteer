using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : Action
{
    [SerializeField] private float _attackTime;
    [SerializeField] private Transform _animationHieght, _endPosition;
    [SerializeField] private Animator _animator;

    public override void StartAction(Transform armStart, ArmPosition position)
    {
        base.StartAction(armStart,position);
        if (_animator != null)
        {
            _animationHieght.localPosition = new Vector3(0, armStart.localPosition.y, 0);
            _animator.enabled = true;
            _animator.SetTrigger("Attack");
            StartCoroutine(WaitForEndOfAnimation());
        }
    }

    IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(_attackTime);
        _animator.enabled = false;
        transform.localPosition = _endPosition.localPosition;
        transform.localRotation = _endPosition.localRotation;
        _animationHieght.localPosition = Vector3.zero;

        EndingAction();
    }

}
