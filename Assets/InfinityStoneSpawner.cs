using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;

// public class InfinityStoneSpawner : MonoBehaviour
// {
//     public GameObject infinityStonePrefab;
//     public float locationChangeInterval = 10f; // 1 minute
//     public float spawnDelay = 5f; // Delay before first spawn
    
//     private GameObject currentStone;
//     private float timer;
//     private bool isActive = true;
    
//     void Start()
//     {
//         Invoke("SpawnStone", spawnDelay);
//     }
    
//     void Update()
//     {
//         if (!isActive) return;
        
//         timer += Time.deltaTime;
//         if (timer >= locationChangeInterval)
//         {
//             MoveStoneToNewLocation();
//             timer = 0;
//         }
//     }
    
//     void SpawnStone()
//     {
//         if (!isActive) return;
        
//         Vector3 randomPosition = GetRandomSpawnPosition();
//         currentStone = Instantiate(infinityStonePrefab, randomPosition, Quaternion.identity);
        
//         // Add collider and tag for identification
//         var collider = currentStone.AddComponent<SphereCollider>();
//         collider.isTrigger = true;
//         currentStone.tag = "InfinityStone";
        
//         timer = 0;
//     }
    
//     void MoveStoneToNewLocation()
//     {
//         if (currentStone != null)
//         {
//             Vector3 newPosition = GetRandomSpawnPosition();
//             currentStone.transform.position = newPosition;
//         }
//         else
//         {
//             SpawnStone();
//         }
//     }
    
//     Vector3 GetRandomSpawnPosition()
//     {
//         Vector3 randomPosition = Random.insideUnitSphere * 5f;
//         randomPosition.y = 1f; // Slightly above ground level
//         return randomPosition;
//     }
    
//     public void OnStoneShot()
//     {
//         isActive = false;
        
//         // Stop ghost spawning
//         GhostSpawner ghostSpawner = FindObjectOfType<GhostSpawner>();
//         if (ghostSpawner != null)
//         {
//             ghostSpawner.StopSpawning();
//         }
        
//         // Make stargate disappear
//         StargateSpawner stargateSpawner = FindObjectOfType<StargateSpawner>();
//         if (stargateSpawner != null && stargateSpawner.stargate != null)
//         {
//             Destroy(stargateSpawner.stargate);
//         }
        
//         // Destroy the stone
//         if (currentStone != null)
//         {
//             Destroy(currentStone);
//         }
        
//     }
// }

public class InfinityStoneSpawner : MonoBehaviour
{
    public GameObject infinityStonePrefab;
    public float locationChangeInterval = 10f; // 1 minute
    public float spawnDelay = 5f; // Delay before first spawn
    
    private GameObject currentStone;
    private float timer;
    private bool isActive = true;

    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset;
    public float minEdgeDistance=0.3f;
    
    void Start()
    {
        Invoke("SpawnStone", spawnDelay);
    }
    
    void Update()
    {
        if (!isActive) return;
        
        timer += Time.deltaTime;
        if (timer >= locationChangeInterval)
        {
            MoveStoneToNewLocation();
            timer = 0;
        }
    }
    
    void SpawnStone()
    {
        if (!isActive) return;
        
        //Vector3 randomPosition = GetRandomSpawnPosition();
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();//获取当前房间的对象（MRUK = Mixed Reality Understanding Kit）
        bool hasFoundPosition = room.GenerateRandomPositionOnSurface(
                MRUK.SurfaceType.VERTICAL,              // 选择墙面（垂直表面）
                minEdgeDistance,                        // 距离边缘的最小距离（避免贴边）
                LabelFilter.Included(spawnLabels),      // 根据设定的 label 过滤哪些表面可以生成
                out Vector3 pos,                        // 输出一个随机点位
                out Vector3 norm                        // 输出该点的法线方向
            );

        //currentStone = Instantiate(infinityStonePrefab, randomPosition, Quaternion.identity);
        if(hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                //这行代码是对生成点稍微“浮出表面”一点点，使 Ghost 不会完全贴住墙面。
                //norm * normalOffset：在法线方向上偏移一段距离。

                randomPositionNormalOffset.y = 1f;
                currentStone = Instantiate(infinityStonePrefab, randomPositionNormalOffset, Quaternion.identity);
            }
        // Add collider and tag for identification
        var collider = currentStone.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        currentStone.tag = "InfinityStone";
        
        timer = 0;
    }
    
    void MoveStoneToNewLocation()
    {
        if (currentStone != null)
        {
            Vector3 newPosition = GetRandomSpawnPosition();
            currentStone.transform.position = newPosition;
        }
        else
        {
            SpawnStone();
        }
    }
    
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomPosition = Random.insideUnitSphere * 5f;
        randomPosition.y = 1f; // Slightly above ground level
        return randomPosition;
    }
    
    public void OnStoneShot()
    {
        isActive = false;
        
        // Stop ghost spawning
        GhostSpawner ghostSpawner = FindObjectOfType<GhostSpawner>();
        if (ghostSpawner != null)
        {
            ghostSpawner.StopSpawning();
        }
        
        // Make stargate disappear
        StargateSpawner stargateSpawner = FindObjectOfType<StargateSpawner>();
        if (stargateSpawner != null && stargateSpawner.stargate != null)
        {
            Destroy(stargateSpawner.stargate);
        }
        
        // Destroy the stone
        if (currentStone != null)
        {
            Destroy(currentStone);
        }
        
    }
}

