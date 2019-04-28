using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovmentController
{
    public GameObject camera;
    public GameObject target;
    public float velocity { get { return vel; } set {
            if (value >= 0 && value <= 1)
                vel = value;
            else
                Debug.LogError("The camera velocity must be between '0' and '1' and it is trying to be set as '" + value + "'", camera.gameObject);
        } }
    private float vel;

    public CameraMovmentController(GameObject camera, GameObject target, float velocity)
    {
        this.camera = camera;
        this.target = target;
        this.velocity = velocity;
    }

    public void Tick()
    {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.transform.position.x, target.transform.position.y, camera.transform.position.z);
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, velocity);
        }
        else
        {
            Debug.LogError("Camera target not set", camera);
        }
    }
}
