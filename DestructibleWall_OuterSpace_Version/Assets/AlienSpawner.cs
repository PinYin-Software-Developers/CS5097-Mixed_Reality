using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;

public class AlienSpawner : MonoBehaviour
{
    public float spawnTimer = 1;
    public GameObject prefabToSpawn;
    private float timer;

    public float minEdgeDistance=0.3f;
    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset;

    public int spawnTry = 1000;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!MRUK.Instance && !MRUK.Instance.IsInitialized) return;
        timer += Time.deltaTime;
        if(timer > spawnTimer){
            SpawnAlien();
            timer -= spawnTimer;
        }
    }

    public void SpawnAlien(){
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();//获取当前房间的对象（MRUK = Mixed Reality Understanding Kit）
        int currentTry = 0;
        while(currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(
                MRUK.SurfaceType.VERTICAL,              // 选择墙面（垂直表面）
                minEdgeDistance,                        // 距离边缘的最小距离（避免贴边）
                LabelFilter.Included(spawnLabels),      // 根据设定的 label 过滤哪些表面可以生成
                out Vector3 pos,                        // 输出一个随机点位
                out Vector3 norm                        // 输出该点的法线方向
            );

            if(hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset;
                //这行代码是对生成点稍微“浮出表面”一点点，使 Ghost 不会完全贴住墙面。
                //norm * normalOffset：在法线方向上偏移一段距离。

                randomPositionNormalOffset.y = Random.Range(1f, 3f);
                Instantiate(prefabToSpawn, randomPositionNormalOffset, Quaternion.identity);
                return;
            }
            else
            {
                currentTry ++;
            }
        }
        

    }
}
