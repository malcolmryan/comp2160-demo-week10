using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WordsOnPlay.Utils;

public class Crosshair : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis(InputAxes.MouseX);
        move.y = Input.GetAxis(InputAxes.MouseY);
        transform.Translate(move);

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
        Rect cameraRect = new Rect( bottomLeft.x, 
                                    bottomLeft.y, 
                                    topRight.x - bottomLeft.x, 
                                    topRight.y - bottomLeft.y);

        transform.position = cameraRect.Clamp(transform.position);
    }
}
