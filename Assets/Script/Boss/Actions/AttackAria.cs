using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class AttackAria : MonoBehaviour
{
private List<Rope> _ropesInTrigger = new List<Rope>();


public void Attack()
{
    foreach (Rope rope in _ropesInTrigger)
    {
        rope.TakeDamage();
    }
}
    private void OnTriggerEnter(Collider other)
    {
        Rope rope = other.gameObject.GetComponent<Rope>();
        if (rope != null && !CheckIfRopeIsInList(rope)&& other.gameObject.tag == "Player")
        {
            _ropesInTrigger.Add(rope);
        }
    }

    private bool CheckIfRopeIsInList(Rope rope)
    {
        foreach (Rope ropeInList in _ropesInTrigger)
        {
            if (ropeInList == rope)
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerExit(Collider other)
    {
        Rope rope = other.gameObject.GetComponent<Rope>();
        if (rope != null)
        {
            _ropesInTrigger.Remove(rope);
        }
    }

    private void OnDisable()
    {
        _ropesInTrigger = new List<Rope>();
    }


}
