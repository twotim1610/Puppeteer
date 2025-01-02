using System.Collections;
using UnityEngine;

public class SlashingAnimation : AnimationAction
{
    [SerializeField] private float _distance, _swordActivationTime;
    [SerializeField] private Sword _sword;
    [SerializeField] private AttackAria _aria;
    public override bool ConditionsAreMet(Vector3 playerPosition, RopeStates player, RopeStates boss, Vector3 bossPosition)
    {
        float distance = (playerPosition - bossPosition).magnitude;
        if (boss.Right && distance <= _distance && (player.Right || player.Left))
        {
            StartCoroutine(ActivateSword());
            _aria.gameObject.SetActive(true);
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
       // _sword.Active = true;
       _aria.Attack();
    }

    protected override void OnEndOfAction()
    {
        _sword.Active = false;
        _aria.gameObject.SetActive(false);
        base.OnEndOfAction();
    }
}
