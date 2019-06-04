using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviveState : State
{
    private bool reviving = false;

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("ReviveIdle");
        yield return "success";
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        if (character.brain.direction != Vector2.zero)
        {
            if (!reviving)
            {
                reviving = true;
                character.visualsAnimator.SetTrigger("Revive");
                character.StartCoroutine(ForceExitRevive(1.05f));
            }

        }
    }

    protected IEnumerator ForceExitRevive(float time)
    {
        yield return new WaitForSeconds(time);
        character.behaviourTree.CalculateAndSetNextState(true);
    }

    public override void OnExit()
    {

    }

    public override void Tick(float deltaTime)
    {

    }


}
