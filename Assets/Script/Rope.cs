using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject _controler;
    [SerializeField] private GogoGaga.OptimizedRopesAndCables.Rope _rope;
    [SerializeField] private float _startWith, _endWith;
    [SerializeField] private int _hp;
    [SerializeField] private RopeStates _state;
    [SerializeField] private Connection _connection;
    [SerializeField] private int _layer;

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

        if (other.tag == "Weapon" && other.gameObject.layer == _layer)
        {
            Sword sword = other.GetComponent<Sword>();
            if (sword != null && sword.Active)
            {
               
                _hp--;
                float with = Mathf.Lerp(_startWith, _endWith, Mathf.Clamp01((float)_hp / _maxHP));
                _rope.ropeWidth = with;
                _rope.InitializeLineRenderer();
                if (_hp <= 0)
                {
                    _rope.Disapear();
                    _controler.SetActive(false);
                    switch (_connection)
                    {
                        case Connection.Left:
                            _state.Left = false;
                            break;
                        case Connection.Right:
                            _state.Right = false;
                            break;
                        case Connection.Up:
                            _state.Top = false;
                            break;
                        case Connection.Down:
                            _state.Bottom = false;
                            break;
                    }

                }


            }
        }

    }

    public enum Connection
    {
        Left,
        Right,
        Up,
        Down
    }


}
