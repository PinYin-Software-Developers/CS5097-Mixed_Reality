using UnityEngine;

public class SwordController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Ghost ghost = other.GetComponentInParent<Ghost>();
        if (ghost != null)
        {
            ghost.Kill();
        }
    }
}