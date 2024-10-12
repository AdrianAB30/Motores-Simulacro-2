using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerObstacle : MonoBehaviour
{
    public ObstacleSOS obstacles;
    void Update()
    {
        if (transform.position.x <= -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x)
        {
            GenerateObstacles.instance.ManagerObstacle(this);
        }
    }
}
