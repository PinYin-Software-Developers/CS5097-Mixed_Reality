using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public float initialSpawnDelay = 3f; // 3s (stargate) + 3s (ghost delay)
    public float spawnInterval = 1f;
    public GameObject ghostPrefab;
    public float ghostSpawnHeight = 2f; // Manual Y-position control
    
    private GameObject stargate;
    private float timer;
    private bool canSpawn = false;

    public void SetStargate(GameObject gate)
    {
        stargate = gate;
        // Start spawning ghosts 3 seconds after stargate appears
        Invoke("EnableSpawning", 3f);
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    void EnableSpawning()
    {
        canSpawn = true;
        timer = 0f;
    }

    void Update()
    {
        if (!canSpawn) return;
        
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnGhost();
            timer = 0f;
        }
    }

    void SpawnGhost()
    {
        if (stargate != null)
        {
            // Set fixed Y position while keeping X/Z positions of stargate
            Vector3 spawnPos = new Vector3(
                stargate.transform.position.x,
                ghostSpawnHeight,
                stargate.transform.position.z
            );
            
            Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
        }
    }
}

