using UnityEngine;
using UnityEngine.AI;
using Meta.XR.MRUtilityKit;
using System.Collections.Generic;


public class DestructibleMeshManager : MonoBehaviour
{
    public rayGun ray_Gun;
    public DestructibleGlobalMeshSpawner meshSpawner;
    private List<GameObject> segments = new List<GameObject>();
    private DestructibleMeshComponent currentComponent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ray_Gun.OnShootAndHit.AddListener(DestroyMeshSegment);
        meshSpawner.OnDestructibleMeshCreated.AddListener(SetupDestructibleComponents);
    }
    public void SetupDestructibleComponents(DestructibleMeshComponent component){
        currentComponent = component;
        component.GetDestructibleMeshSegments(segments);
        foreach (var item in segments)
        {
            item.AddComponent<MeshCollider>();

           // 添加 NavMesh Obstacle，让它阻挡 alien
            var obstacle = item.AddComponent<UnityEngine.AI.NavMeshObstacle>();
            obstacle.carving = true; // 自动在 NavMesh 上 carve 出洞
            obstacle.carveOnlyStationary = false; // 如果 segment 有动画就设为 false
        }
    }
    public void DestroyMeshSegment(GameObject segment)
    {
        if(segments.Contains(segment) && currentComponent.ReservedSegment != segment){
            currentComponent.DestroySegment(segment);
        }
    }

}
