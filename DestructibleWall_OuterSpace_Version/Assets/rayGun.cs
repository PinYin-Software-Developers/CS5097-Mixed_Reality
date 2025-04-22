// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;

// public class rayGun : MonoBehaviour
// {
//     [System.Serializable]
//     public class HitEvent : UnityEngine.Events.UnityEvent<GameObject> { }

//     public HitEvent OnShootAndHit;



//     public LayerMask layerMask;
//     public OVRInput.RawButton shootingButton;
//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     public LineRenderer linePrefab;
//     public GameObject rayImpactPrefab;
//     public Transform shootingPoint;
//     public float maxLineDistance = 5;
//     public float lineShowTimer = 0.3f;
//     public AudioSource source;
//     public AudioClip shootingAudioClip;

//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if(OVRInput.GetDown(shootingButton)){
//             Shoot();
//         }
//     }
//     public void Shoot(){
//         //Debug.Log("Pew");
//         source.PlayOneShot(shootingAudioClip);

//         Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
//         bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

//         Vector3 endPoint = Vector3.zero;
//         if (hasHit)
//         {
//             // Stop the ray at the hit point
//             endPoint = hit.point;

//             // 发出事件（无论击中什么都触发）
//             OnShootAndHit?.Invoke(hit.collider.gameObject);

//             // Check if the hit object or its parent has a "Alien" component
//             Alien alien = hit.transform.GetComponentInParent<Alien>();

//             if (alien)
//             {
//                 hit.collider.enabled = false;
//                 // Kill the elien
//                 alien.kill();
//             }
//             else
//             {
//                 // Instantiate a ray impact effect at the hit point
//                 Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
//                 GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
//                 Destroy(rayImpact, 1); // Auto-destroy the effect after 1 second
//             }
//         }
//         else{
//             endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
//         }

//         LineRenderer line = Instantiate(linePrefab);
//         line.positionCount = 2;
//         line.SetPosition(0,shootingPoint.position);

//        // Vector3 endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;

//         line.SetPosition(1, endPoint);

//         Destroy(line.gameObject, lineShowTimer);
//     }

// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class rayGun : MonoBehaviour
{
    public LayerMask layerMask;
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public GameObject rayImpactPrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;
    public AudioSource source;
    public AudioClip shootingAudioClip;

    public UnityEvent OnShoot;
    public UnityEvent<GameObject> OnShootAndHit;
    public UnityEvent OnShootAndMiss;

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
        OnShoot.Invoke();

        source.PlayOneShot(shootingAudioClip);

        Ray ray = new Ray(shootingPoint.position, shootingPoint.forward);
        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, maxLineDistance, layerMask);

        Vector3 endPoint = Vector3.zero;

        if(hasHit)
        {
            //stop the ray
            endPoint = hit.point;
            // Check if the hit object or its parent has a "Alien" component
            Alien alien = hit.transform.GetComponentInParent<Alien>();

            if (alien)
            {
                hit.collider.enabled = false;
                // Kill the elien
                alien.kill();
            }
            else
            {
                // Instantiate a ray impact effect at the hit point
                Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);
                GameObject rayImpact = Instantiate(rayImpactPrefab, hit.point, rayImpactRotation);
                Destroy(rayImpact, 1); // Auto-destroy the effect after 1 second
                OnShootAndHit.Invoke(hit.transform.gameObject);
            }
        }
        else
        {
            endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;
            OnShootAndMiss.Invoke();
        }

        LineRenderer line = Instantiate(linePrefab);

        line.positionCount = 2;
        line.SetPosition(0, shootingPoint.position);
        line.SetPosition(1, endPoint);

        Destroy(line.gameObject, lineShowTimer);
    }
}

