using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehaviourTree : BehaviourTree
{


    public PatrolEnemyBehaviourTree(CharacterManager characterManager, State defaultState)
    {
        base.Setup(defaultState, characterManager);
    }

    public override void CalculateAndSetNextState(bool forceExitState)
    {

        if (forceExitState)
            ForceExitState(character);

        if (EnterOnAir()) return;
        if (EnterAttack()) return;
        if (EnterJump()) return;
        if (EnterWalking()) return;
        if (EnterCrouched()) return;
        if (EnterIdle()) return;

    }

    private bool EnterAttack()
    {
        if (!character.brain.actionHold)
            return false;

        State.SetState(new AttackState(character, 1f), character);
        return true;
    }

}
