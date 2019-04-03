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
                Debug.LogWarning("A wrong value is wanted to be saved as camera velocity: " + value);
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
            camera.transform.position = Vector3.Lerp(camera.transform.position, target.transform.position, vel);
        else
            Debug.LogError("Camera target not set", camera);
    }
}
