using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject _controler;
    [SerializeField] private GogoGaga.OptimizedRopesAndCables.Rope _rope;
    [SerializeField] private float _startWith, _endWith;
    [SerializeField] private int _hp;

    private int _maxHP;
    private void Start()
    {
        _maxHP = _hp;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _hp--;
            float with = Mathf.Lerp(_startWith, _endWith,Mathf.Clamp01((float)_hp/_maxHP));
            _rope.ropeWidth = with;
            _rope.InitializeLineRenderer();
            if (_hp <= 0)
            {
                _rope.Disapear();
                _controler.SetActive(false);
            }

           
        }

    }


}
