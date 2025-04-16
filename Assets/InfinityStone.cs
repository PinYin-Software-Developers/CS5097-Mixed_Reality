using UnityEngine;

public class InfinityStone : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Kill(){
        // agent.enabled = false;
        animator.SetTrigger("Destroy");
    }

    public void Destroy(){
        Destroy(gameObject);
    }
}