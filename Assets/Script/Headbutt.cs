using System;
using UnityEngine;

public class Headbutt : MonoBehaviour
{
    public bool IsHeadbutting { get; set; }
    public event EventHandler OnHeadbutt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && IsHeadbutting)
        {
            IsHeadbutting = false;
            OnHeadbutt?.Invoke(this, System.EventArgs.Empty);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 8 && IsHeadbutting)
        {
            IsHeadbutting = false;
            OnHeadbutt?.Invoke(this, System.EventArgs.Empty);
        }
    }

}
