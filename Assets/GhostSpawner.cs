// using UnityEngine;
// using Meta.XR.MRUtilityKit;

// public class GhostSpawner : MonoBehaviour
// {
//     public float spawnTimer = 1;
//     public GameObject prefabToSpawn;

//     public float minEdgeDistance = 0.3f;
//     public MRUKAnchor.SceneLabels spawnLabels;

//     public float normalOffset;

//     private float timer;

//     public int spawnTry = 1000;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(!MRUK.Instance && !MRUK.Instance.IsInitialized)
//         {
//             return;
//         }

//         timer += Time.deltaTime;
//         if(timer > spawnTimer){
//             spawnGhost();
//             timer -= spawnTimer;
//         }
        
//     }

//     public void spawnGhost()
//     {
//         MRUKRoom room = MRUK.Instance.GetCurrentRoom();

//         int currentTry = 0;

//         while(currentTry < spawnTry)
//         {
//             bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);
//             if(hasFoundPosition){
//                 Vector3 randomPositionNormalOffSet = pos + norm * normalOffset;
//                 randomPositionNormalOffSet.y = 0;
//                 Instantiate(prefabToSpawn, randomPositionNormalOffSet, Quaternion.identity);
//             }
//             else {
//                 currentTry++;
//             }
//         }

        

        

        
//     }
// }
// ======================================================================================
using UnityEngine;


public class GhostSpawner : MonoBehaviour
{
    public float spawnTimer = 3f; // Changed from 1 to 3 (spawns every 3 seconds)
    public GameObject prefabToSpawn;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimer)
        {
            spawnGhost();
            timer = 0; // Reset timer instead of subtracting (more precise)
        }
    }

    public void spawnGhost()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 3;
        randomPosition.y = 0;
        Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
    }
}

// ======================================================================================

// using UnityEngine;

// public class GhostSpawner : MonoBehaviour
// {
//     public GameObject ghostPrefab; // Assign your ghost prefab in Inspector
//     public float spawnInterval = 5f; // Time between spawns (seconds)
//     public float lifetime = 300f; // 5 minutes (set to 60 for quick testing)

//     private float timer;

//     void Start()
//     {
//         Destroy(gameObject, lifetime); // Self-destruct after lifetime ends
//     }

//     void Update()
//     {
//         timer += Time.deltaTime;
//         if (timer >= spawnInterval)
//         {
//             SpawnGhost();
//             timer = 0;
//         }
//     }

//     void SpawnGhost()
//     {
//         Vector3 spawnPos = transform.position + Random.insideUnitSphere * 2f; // Spawn near the cube
//         spawnPos.y = 1f; // Adjust height if needed
//         Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
//     }
// }