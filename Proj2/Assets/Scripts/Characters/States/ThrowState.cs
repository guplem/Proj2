using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowState : State
{
    bool lookingRight;
    public ThrowState(PlayerManager characterManager)
    {
        this.character = characterManager;
        ((PlayerManager)character).rb2d.velocity = Vector3.zero;
        lookingRight = character.transform.eulerAngles == new Vector3(0, 0, 0);
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Charge");
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
        lookingRight = character.transform.eulerAngles == new Vector3(0, 0, 0);

        Vector3 mousePosition = GameManager.Instance.cursor.GetCursorPositionOnWorld();
        Vector3 throwPosition = ((PlayerManager)character).getThrowPoint().position;

        float mousePositionCorrected = mousePosition.x;

        if (character.brain.actionHold)
        {
            if (character.brain.interact)  //Use this button to cancel while holding the throwing button.
            {
                // return to default
                ((PlayerManager)character).behaviourTree.CalculateAndSetNextState(true);
                return;
            }

            if (lookingRight)
            {
                if (mousePosition.x < throwPosition.x)
                    mousePositionCorrected = throwPosition.x;

                if (mousePosition.x < character.transform.position.x)
                    Utils.SetObjectLookingDirection(-1, character.gameObject);
            }
            else
            {
                if (mousePosition.x > throwPosition.x)
                    mousePositionCorrected = throwPosition.x;

                if (mousePosition.x > character.transform.position.x)
                {
                    Utils.SetObjectLookingDirection(1, character.gameObject);
                }
            }
            Vector3[] points = { throwPosition, new Vector3(mousePositionCorrected, mousePosition.y, mousePosition.z) };

            GameManager.Instance.lineManager.SetupLinePoints(points);
            GameManager.Instance.lineManager.SetDrawing(true);
        }
        else
        {
            Vector3 direction = (new Vector3(mousePositionCorrected, mousePosition.y, mousePosition.z) - throwPosition);

            if (throwCorroutine == null)
            {
                throwCorroutine = Throw(direction, throwPosition);
                character.StartCoroutine(throwCorroutine);
            }
        }
    }

    IEnumerator throwCorroutine;
    private IEnumerator Throw(Vector3 direction, Vector3 throwPosition)
    {
        GameManager.Instance.lineManager.SetDrawing(false);
        character.visualsAnimator.SetTrigger("Throw");
        yield return new WaitForSeconds(0.1f);
        ((PlayerManager)character).inventory.ThrowStoredItem(((PlayerManager)character).throwingForce * direction, throwPosition);
        yield return new WaitForSeconds(0.4f);
        ((PlayerManager)character).behaviourTree.CalculateAndSetNextState(true);

    }

    public override void OnExit()
    {
        GameManager.Instance.lineManager.SetDrawing(false);
        if (throwCorroutine != null)
            character.StopCoroutine(throwCorroutine);
    }

}
