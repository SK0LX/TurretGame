using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    private Vector3 mousePosition; 

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        var direction = mousePosition - transform.position;
        
        var angle = Mathf.Atan2(direction.y, direction.x);
        
        angle = Mathf.Rad2Deg * angle;
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
