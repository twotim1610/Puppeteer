using System.Collections;
using UnityEngine;

public class AnimationAction : BossAction
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;
    [SerializeField] private float _animationTime;

    public override bool ConditionsAreMet(Vector3 playerPosition, RopeStates player, RopeStates boss, Vector3 bossPosition)
    {
        if (_isActive)
        { 
            _animator.SetTrigger(_animationName);
            StartCoroutine(DiactiviationDelay());
            return true;
        }

        return false;
    }

    IEnumerator DiactiviationDelay()
    {
        yield return new WaitForSeconds(_animationTime);
        _isActive = false;
        OnEndOfAction();
    }

}
