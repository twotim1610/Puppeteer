using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Sword : MonoBehaviour
{
    [SerializeField] private bool _isPhysicsSword;
    [SerializeField] private GameObject _physicsSword;
    private bool _active;

    public bool Active
    {
        get
        {
           return _isPhysicsSword ? true : _active;
        }
        set
        {
           _active = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            Active = false;
        }
    }

    private void OnDisable()
    {
        if(!_isPhysicsSword && _physicsSword!= null)
            _physicsSword.SetActive(true);
    }



}
