using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovmentController
{

    public CameraManager camera;
    public CharacterManager target;

    public CameraMovmentController(CameraManager camera, CharacterManager target)
    {
        this.camera = camera;
        this.target = target;
    }

    public void Tick()
    {
        if (target != null)
        {
            try
            {
                float negativePlayerVerticalVelocity = target.rb2d.velocity.y < 0 ? target.rb2d.velocity.y * 0.5f : 0;
                float offsetDirectionX = target.brain.direction.x;
                Vector3 targetPos = new Vector3(target.transform.position.x + (camera.cameraOffset.x * offsetDirectionX), target.transform.position.y + camera.cameraOffset.y + negativePlayerVerticalVelocity, camera.transform.position.z);
                camera.transform.position = new Vector3(Mathf.Lerp(camera.transform.position.x, targetPos.x, camera.cameraSpeed.x), Mathf.Lerp(camera.transform.position.y, targetPos.y, camera.cameraSpeed.y), targetPos.z);
            }
            catch (Exception e) { Debug.LogWarning(e.ToString()); }

        }
        else
        {
            Debug.LogError("Camera target not set", camera);
        }
    }
}
