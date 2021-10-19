using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private Crosshair crosshair;
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
        Vector3 dir = crosshair.transform.position - transform.position;
        transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
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
