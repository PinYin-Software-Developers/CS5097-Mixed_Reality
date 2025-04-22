using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
public class Alien : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!agent.enabled){
            return;
        }
        Vector3 targetPosition = Camera.main.transform.position; //give you the position of main camera
        agent.SetDestination(targetPosition);
        agent.speed = speed;
    }
    public void kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");

    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
