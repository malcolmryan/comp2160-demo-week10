using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordsOnPlay.Utils;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float extraSpeedPerDistance = 5;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Crosshair crosshair;
    [SerializeField]
    private float lerpRatio = 0.1f;
    [SerializeField]
    private Rect worldRect;
    private Rect cameraRect;

    void Start()
    {
        // Assumes that camera is orthographic and aligned along the z-axis
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
        float w = topRight.x - bottomLeft.x;
        float h = topRight.y - bottomLeft.y;
        cameraRect = new Rect(worldRect.x + w / 2, worldRect.y + h / 2, worldRect.width - w, worldRect.height - h); 
    }

    void LateUpdate()
    {
        Vector3 target = Vector3.Lerp(player.transform.position, crosshair.transform.position, lerpRatio);
        target = cameraRect.Clamp(target);
        Vector3 offset = target - transform.position;
        float s = (speed + extraSpeedPerDistance * offset.magnitude);
        Vector3 move = offset.normalized * s * Time.deltaTime;
        if (move.magnitude > offset.magnitude)
        {
            move = offset;
        }

        transform.Translate(move);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        worldRect.DrawGizmo();

        Gizmos.color = Color.yellow;
        cameraRect.DrawGizmo();
    }

}
