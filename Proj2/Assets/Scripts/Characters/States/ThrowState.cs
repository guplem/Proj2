using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : IState
{
    public CharacterManager character { get; set; }

    public ThrowState(CharacterManager characterManager)
    {
        this.character = characterManager;
        ((PlayerManager)character).rb2d.velocity = Vector3.zero;
    }

    public void Tick(float deltaTime)
    {
        CheckThrow();
    }

    public void FixedTick(float fixedDeltaTime)
    {

    }

    private void CheckThrow()
    {
        Vector3 throwPosition = ((PlayerManager)character).getThrowPoint().position;
        bool storedItem = ((PlayerManager)character).inventory.HasStoredItem();

        if (character.brain.action)
        {
            if (character.brain.interact)  //Use this button to cancel while holding the throwing button.
            {
                GameManager.Instance.lineManager.SetDrawing(false);
                // return to default
                ((PlayerManager)character).behaviourTree.CalculateAndSetNextState(true);
                return;
            }
            if (storedItem)
            {
                Vector3 mousePosition = GameManager.Instance.camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                Vector3[] points = { throwPosition, mousePosition };

                GameManager.Instance.lineManager.SetupLinePoints(points);
                GameManager.Instance.lineManager.SetDrawing(true);
            }
            else
            {
                Debug.Log("I do not have an item stored!");
            }
        }
        else if (character.brain.actionRelease)
        {
            if (storedItem)
            {
                Vector3 direction;
                Vector3 mousePosition = GameManager.Instance.camera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);

                direction = mousePosition - throwPosition;

                ((PlayerManager)character).inventory.ThrowStoredItem(new Vector2(((PlayerManager)character).throwingForce.x, ((PlayerManager)character).throwingForce.y) * direction, throwPosition);
                GameManager.Instance.lineManager.SetDrawing(false);
            }
            ((PlayerManager)character).behaviourTree.CalculateAndSetNextState(true);
        }
    }

    public void OnExit()
    {

    }

}
