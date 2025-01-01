using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private GameObject _physicsShield;
    private void OnDisable()
    {
        if ( _physicsShield != null)
            _physicsShield.SetActive(true);
    }
}
