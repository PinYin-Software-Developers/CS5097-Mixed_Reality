using UnityEngine;

public class StargateSpawner : MonoBehaviour
{
    public float spawnDelay = 3f;
    public GameObject stargatePrefab;
    
    public GameObject stargate;

    

    void Start()
    {
        Invoke("SpawnStargate", spawnDelay);
    }

    void SpawnStargate()
    {
        
        Vector3 randomPosition = Random.insideUnitSphere * 3;
        randomPosition.y = 2;
        
        // Modified rotation to make stargate stand vertically
        // 90 degrees on X-axis makes it stand upright (perpendicular to floor)
        Quaternion verticalRotation = Quaternion.Euler(90f, 0f, 0f);
        
        stargate = Instantiate(stargatePrefab, randomPosition, verticalRotation);
        
        // Let the GhostSpawner know the stargate exists
        FindObjectOfType<GhostSpawner>().SetStargate(stargate);
        
    }

    
}


// using UnityEngine;
// using UnityEngine.AI;

// public class StargateSpawner : MonoBehaviour
// {
//     public float spawnDelay = 3f;
//     public GameObject stargatePrefab;
//     public float minDistanceFromCamera = 5f;
    
//     private GameObject stargate;
//     private Transform cameraTransform;

//     void Start()
//     {
//         cameraTransform = Camera.main.transform;
//         Invoke("SpawnStargate", spawnDelay);
//     }

//     void SpawnStargate()
//     {
//         // Find valid NavMesh position away from camera
//         Vector3 spawnPosition = FindSpawnPosition();
        
//         // Calculate rotation to face camera (only on Y axis)
//         Vector3 lookDirection = cameraTransform.position - spawnPosition;
//         lookDirection.y = 0; // Keep stargate upright
//         Quaternion faceCameraRotation = Quaternion.LookRotation(lookDirection);
        
//         // Combine with vertical rotation (90° on X-axis)
//         Quaternion finalRotation = faceCameraRotation * Quaternion.Euler(90f, 0f, 0f);
        
//         stargate = Instantiate(stargatePrefab, spawnPosition, finalRotation);
        
//         // Add NavMeshAgent if not already present
//         if (!stargate.GetComponent<NavMeshAgent>())
//         {
//             var agent = stargate.AddComponent<NavMeshAgent>();
//             agent.enabled = false; // We don't want it to move, just validate position
//         }
        
//         FindObjectOfType<AlienSpawner>().SetStargate(stargate);
//     }

//     Vector3 FindSpawnPosition()
//     {
//         Vector3 randomDirection = Random.insideUnitSphere.normalized * minDistanceFromCamera;
//         randomDirection += cameraTransform.position;
//         randomDirection.y = 2f; // Your preferred height
        
//         NavMeshHit hit;
//         int attempts = 0;
//         int maxAttempts = 30;
        
//         do {
//             if (NavMesh.SamplePosition(randomDirection, out hit, minDistanceFromCamera, NavMesh.AllAreas))
//             {
//                 // Check if position is sufficiently away from camera
//                 if (Vector3.Distance(hit.position, cameraTransform.position) >= minDistanceFromCamera)
//                 {
//                     return hit.position;
//                 }
//             }
            
//             // Try new random direction if first attempt fails
//             randomDirection = Random.insideUnitSphere.normalized * minDistanceFromCamera;
//             randomDirection += cameraTransform.position;
//             randomDirection.y = 2f;
            
//             attempts++;
//         } while (attempts < maxAttempts);
        
//         // Fallback position if no valid position found
//         Debug.LogWarning("Couldn't find valid NavMesh position, using fallback");
//         return cameraTransform.position + cameraTransform.forward * minDistanceFromCamera + Vector3.up * 2f;
//     }
// }