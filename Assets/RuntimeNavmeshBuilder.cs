using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;

public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navmeshSurface;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navmeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);
        
    }

    public void BuildNavmesh()
    {
        navmeshSurface.BuildNavMesh();
        // StartCoroutine(BuildNavmeshRoutine());
    }

    // public IEnumerator buildNavmeshRoutine()
    // {
    //     yield return new WaitForEndOfFrame();
    //     navmeshSurface.BuildNavMesh();
    // }
    // Update is called once per frame
    // void Update()
    // {
    //     navmeshSurface.BuildNavMesh();
    // }
}
