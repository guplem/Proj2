using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourTree
{
    protected CharacterManager character;
    public State defaultState;

    public void Setup(State defaultState, CharacterManager character)
    {
        this.defaultState = defaultState;
        this.character = character;
    }

    public abstract void CalculateAndSetNextState(bool forceExitState);

    protected void ForceExitState(CharacterManager character)
    {
        State.SetState(null, character);
    }



    //////// DEFAULT TRANSITIONS  /////////////////////


    protected bool EnterIdle()
    {
        if (!character.IsTouchingGround())
            return false;

        if (character.brain.direction != Vector2.zero)
            return false;

        if (character.rb2d.velocity != Vector2.zero)
            return false;

        State.SetState(new IdleState(character), character);
        return true;
    }

    protected bool EnterCrouched()
    {
        if (!character.IsTouchingGround())
            return false;

        if (!character.brain.crouch)
            return false;

        State.SetState(new CrouchedState(character), character);
        return true;
    }

    protected bool EnterWalking()
    {
        if (!Utils.IsColliderTouchingLayer(character.groundCollider, GameManager.Instance.walkableLayers))
            return false;

        if (character.brain.direction.x == 0)
            return false;

        if ((character.rb2d.velocity.y > 0.1f) || (character.rb2d.velocity.y < -0.1f))
            return false;

        if (character.state is CrouchedState)
            return false;

        State.SetState(new WalkingState(character), character);
        return true;
    }

    protected bool EnterOnAir()
    {
        if (character.IsTouchingGround())
            return false;

        if (!(character.rb2d.velocity.y <= 0.1f))
            return false;

        State.SetState(new OnAirState(character), character);
        return true;
    }

    protected bool EnterJump()
    {
        if (!character.brain.jumping)
            return false;

        if (!character.IsTouchingGround())
            return false;

        State.SetState(new JumpingState(character), character);
        return true;
    }

}
