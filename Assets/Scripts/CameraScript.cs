using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Range(40,80)]
    public float zoom = 65;
    public float speed = 0.1f;

    void Update()
    {
        Camera.main.orthographicSize = zoom;

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved )
        {
            Vector2 touchPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchPosition);
        }

        if(Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Camera moving!");
        }
    }
}
