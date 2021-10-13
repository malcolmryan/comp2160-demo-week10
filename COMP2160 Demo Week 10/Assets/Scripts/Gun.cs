using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Bullet bulletPrefab;

    private Transform spawnPoint;
    private Plane plane;

    void Start()
    {
        plane = new Plane(Vector3.forward, Vector3.zero);    
        spawnPoint = transform.Find("SpawnPoint");
    }

    void Update()
    {
        Aim();
        Fire();
    }

    private void Aim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float t = 0; 
        if (plane.Raycast(ray, out t)) 
        {
            Vector3 point = ray.GetPoint(t);
            Vector3 dir = point - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
        }
    }

    private void Fire()
    {
        if (Input.GetButtonDown(InputAxes.Fire))
        {
            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = spawnPoint.rotation;
        }
    }
}
