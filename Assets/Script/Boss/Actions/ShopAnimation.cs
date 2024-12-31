using System.Collections;
using UnityEngine;

public class ShopAnimation : AnimationAction
{
    [SerializeField] private float _shopDistance, _swordActivationTime;
    [SerializeField] private Sword _sword;
    public override bool ConditionsAreMet(Vector3 playerPosition, RopeStates player, RopeStates boss, Vector3 bossPosition)
    {
        float distance = (playerPosition - bossPosition).magnitude;
        if (boss.Right && distance <= _shopDistance)
        {
            StartCoroutine(ActivateSword());
            _isActive = true;
        }
        else
        {
            _isActive = false;
        }
        return base.ConditionsAreMet(playerPosition, player, boss, bossPosition);
    }
    IEnumerator ActivateSword()
    {
        yield return new WaitForSeconds(_swordActivationTime);
        _sword.Active = true;
    }

    protected override void OnEndOfAction()
    {
        _sword.Active = false;
        base.OnEndOfAction();
    }
}
