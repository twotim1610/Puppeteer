using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Sword : MonoBehaviour
{
    public bool Active { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Shield")
        {
            Active = false;
        }
    }


}
