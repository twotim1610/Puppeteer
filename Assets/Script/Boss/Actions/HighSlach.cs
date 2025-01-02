using System.Collections;
using UnityEngine;

public class HighSlach : AnimationAction
{
    [SerializeField] private float _distance, _swordActivationTime;
    [SerializeField] private Sword _sword;
    [SerializeField] private AttackAria _aria;
    public override bool ConditionsAreMet(Vector3 playerPosition, RopeStates player, RopeStates boss, Vector3 bossPosition)
    {
        float distance = (playerPosition - bossPosition).magnitude;
        if (boss.Right && distance <= _distance && player.Top)
        {
            StartCoroutine(ActivateSword());
            _isActive = true;
            _aria.gameObject.SetActive(true);
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
        _aria.Attack();
        //_sword.Active = true;
    }

    protected override void OnEndOfAction()
    {
        _sword.Active = false;
        _aria.gameObject.SetActive(false);
        base.OnEndOfAction();
    }
}
