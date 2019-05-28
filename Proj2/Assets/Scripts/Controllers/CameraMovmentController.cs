using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovmentController
{
    public CameraManager camera;
    public CharacterManager target;
    public float velocity
    {
        get { return vel; }
        set
        {
            if (value >= 0 && value <= 1)
                vel = value;
            else
                Debug.LogError("The camera velocity must be between '0' and '1' and it is trying to be set as '" + value + "'", camera.gameObject);
        }
    }
    private float vel;

    public CameraMovmentController(CameraManager camera, CharacterManager target, float velocity)
    {
        this.camera = camera;
        this.target = target;
        this.velocity = velocity;
    }

    public void Tick()
    {
        if (target != null)
        {
            try
            {
                float offsetDirection = target.brain.direction.x;
                Vector3 targetPos = new Vector3(target.transform.position.x + (camera.cameraOffset.x * offsetDirection), target.transform.position.y + camera.cameraOffset.y, camera.transform.position.z);
                camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, velocity);
            }
            catch (Exception e) { Debug.LogWarning(e.ToString()); }

        }
        else
        {
            Debug.LogError("Camera target not set", camera);
        }
    }
}
