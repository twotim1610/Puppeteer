using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class BossControler : MonoBehaviour
{
    [Header("Player")] [SerializeField] private Transform _player;
    [SerializeField] private RopeStates _playerRopes;
    

    [Header("Repositioning")] 
    [SerializeField] private float _attackDictance;

    [SerializeField] private float _movementSpeed, _rotationSpeed;
    [SerializeField] private CharacterController _movementControler;

    [Header("Actions")]
    [SerializeField] private BossAction[] _actions;

    [Header("Ropes")] [SerializeField] private RopeStates _ropeStates;

    private bool _isInAction;
    void Start()
    {
        foreach (BossAction bossAction in _actions)
        {
            bossAction.EndOfAction += OnEndOfAction;
        }
    }

    private void OnEndOfAction(object sender, System.EventArgs eventArgs)
    {
        _isInAction = false;
    }
   
    void Update()
    {
        if (!_isInAction)
        {
            ChoseAction();
        }
        LookAtPlayer();
    }

    void FixedUpdate()
    {
        if (!_isInAction)
        {
            MovingToPlayer();
        }
    }

    private void ChoseAction()
    {
        BossAction[] randomisedActions = RandomizeActions(_actions);
        foreach (BossAction action in randomisedActions)
        {
            if (action.ConditionsAreMet(_player.position, _playerRopes, _ropeStates, transform.position))
            {
                _isInAction = true;
            }
        }
    }

    private BossAction[] RandomizeActions(BossAction[] actions)
    {
        List<int> l = Enumerable.Range(0, actions.Length).ToList();
        BossAction[] randomizeActions = new BossAction[l.Count];

        for (int i = 0; i < randomizeActions.Length; i++)
        {

            int random = Random.Range(0, l.Count-1);

            randomizeActions[i] = actions[l[random]];
            l.RemoveAt(random);
        }

        return randomizeActions;

    }

    private void LookAtPlayer()
    {

        Vector3 lookDirection = _player.position - transform.position;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void MovingToPlayer()
    {
      Vector3 direction =  _player.position - transform.position;
      direction.y = 0;
      if (direction.magnitude > _attackDictance)
      {
            _movementControler.Move(direction.normalized * Time.deltaTime * _movementSpeed);
      }
    }
}
