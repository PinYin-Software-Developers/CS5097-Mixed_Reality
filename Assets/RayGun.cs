using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(OVRInput.GetDown(shootingButton))
        {
            Shoot();
        }
        
    }

    public void Shoot()
    {
        // // LineRenderer line = Instantiate(linePrefab);
        // GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // block.transform.position = new Vector3(0, 0, 0);

        // Create a new cube
        GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        // Randomize the direction
        Vector3 randomDirection = Random.insideUnitSphere.normalized * maxLineDistance;
        randomDirection += shootingPoint.position; // Start from the shooting point

        // Set the block's position
        block.transform.position = randomDirection;
    }
}


