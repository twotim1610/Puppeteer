using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private GameObject _controler;
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _controler.SetActive(false);
        }

    }


}
