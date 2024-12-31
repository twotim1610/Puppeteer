using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossAction : MonoBehaviour
{
    public event EventHandler EndOfAction;

    protected bool _isActive;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual bool ConditionsAreMet(Vector3 playerPosition,RopeStates player, RopeStates boss, Vector3 bossPosition )
    {
        return false;
    }

    protected virtual void OnEndOfAction()
    {
        EndOfAction?.Invoke(this, System.EventArgs.Empty);
    }
}
