using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : EventArgs
{
    [SerializeField] private float _attackTime, _swordActivationTime;
    [SerializeField] private Transform _animationHieght, _endPosition;
    [SerializeField] private Animator _animator;
    [SerializeField] private Sword _sword;

    public override void StartAction(Transform armStart, ArmPosition position)
    {
        base.StartAction(armStart,position);
        if (_animator != null && gameObject.activeSelf)
        {
            
            _animationHieght.localPosition = new Vector3(0, armStart.localPosition.y, 0);
            _animator.enabled = true;
            _animator.SetTrigger("Attack");
            StartCoroutine(WaitForEndOfAnimation());
            StartCoroutine(ActivateSword());
        }
    }

    IEnumerator ActivateSword()
    {
        yield return new WaitForSeconds(_swordActivationTime);
        _sword.Active = true;
    }

    IEnumerator WaitForEndOfAnimation()
    {
        yield return new WaitForSeconds(_attackTime);
        _animator.enabled = false;
        transform.localPosition = _endPosition.localPosition;
        transform.localRotation = _endPosition.localRotation;
        _animationHieght.localPosition = Vector3.zero;
        _sword.Active = false;
        EndingAction();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }


}
