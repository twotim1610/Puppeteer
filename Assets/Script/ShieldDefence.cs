using System.Collections;
using UnityEngine;

public class ShieldDefence : EventArgs
{
    [SerializeField] private Transform _blockPosition;
    [SerializeField] private float _blockTime, _movementTime;
    public override void StartAction(Transform armStart, ArmPosition position)
    {
        base.StartAction(armStart, position);
        StartCoroutine(Blocking(armStart));
    }

    IEnumerator Blocking(Transform armStart)
    {
        Vector3 startPosition = armStart.localPosition;
        Vector3 endPosition = new Vector3(_blockPosition.localPosition.x, startPosition.y, _blockPosition.localPosition.z);
        Quaternion startRotation = armStart.localRotation;
        Quaternion endRotation = _blockPosition.localRotation;

        float t = 0;
        while (t <= _movementTime)
        {
            t += Time.deltaTime;
            armStart.localPosition = Vector3.Lerp(startPosition, endPosition, t/_movementTime);
            armStart.localRotation = Quaternion.Lerp(startRotation, endRotation, t/ _movementTime);
            yield return null;
        }
        armStart.localPosition = endPosition;
        armStart.localRotation = endRotation;
       yield return new WaitForSeconds(_blockTime);
        EndingAction();
    }
}
