using System;
using UnityEngine;
public class EventArgs:MonoBehaviour
{
    public event EventHandler OnActionEnd;
    public event EventHandler LoseControl;

    public ArmPosition ArmPosition { get; protected set; }
    public Transform ArmStart { get; protected set; }

    public virtual void StartAction(Transform armStart, ArmPosition position)
    {
      ArmStart = armStart;
        ArmPosition = position;
    }

    protected void EndingAction()
    {
        OnActionEnd?.Invoke(this, System.EventArgs.Empty);
    }

    protected void LosingControl()
    {
        LoseControl?.Invoke(this, System.EventArgs.Empty);
    }
}
