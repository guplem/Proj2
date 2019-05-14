using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : State
{

    public ThrowState(PlayerManager characterManager)
    {
        this.character = characterManager;
        ((PlayerManager)character).rb2d.velocity = Vector3.zero;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Throw");
        yield return "success";
    }

    public override void Tick(float deltaTime)
    {
        CheckThrow();
    }

    public override void FixedTick(float fixedDeltaTime)
    {

    }

    private void CheckThrow()
    {
        Vector3 throwPosition = ((PlayerManager)character).getThrowPoint().position;

        if (character.brain.actionHold)
        {
            if (character.brain.interact)  //Use this button to cancel while holding the throwing button.
            {
                // return to default
                ((PlayerManager)character).behaviourTree.CalculateAndSetNextState(true);
                return;
            }

            Vector3 mousePosition = GameManager.Instance.cursor.GetCursorPositionOnWorld();

            Vector3[] points = { throwPosition, mousePosition };

            GameManager.Instance.lineManager.SetupLinePoints(points);
            GameManager.Instance.lineManager.SetDrawing(true);

            if (mousePosition.x < throwPosition.x)
            {
                Utils.SetObjectLookingDirection(0, character.gameObject);
            }
            else if (mousePosition.x > throwPosition.x)
            {
                Utils.SetObjectLookingDirection(1, character.gameObject);
            }
            Debug.Log("Aiming!");
        }
        else // Nota: és necessari controla actionRelease? no serveix !brain.action?
        {
            // Play animation!
            //character.visualsAnimator.SetTrigger("Throw");

            Vector3 direction;
            Vector3 mousePosition = GameManager.Instance.cursor.GetCursorPositionOnWorld();

            direction = mousePosition - throwPosition;

            ((PlayerManager)character).inventory.ThrowStoredItem(new Vector2(((PlayerManager)character).throwingForce.x, ((PlayerManager)character).throwingForce.y) * direction, throwPosition);

            ((PlayerManager)character).behaviourTree.CalculateAndSetNextState(true);
            Debug.Log("THROW!");
        }
    }

    public override void OnExit()
    {
        GameManager.Instance.lineManager.SetDrawing(false);
    }

}
