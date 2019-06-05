using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    public IdleState(CharacterManager character)
    {
        this.character = character;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Idle");
        character.rb2d.velocity = new Vector2(0, character.rb2d.velocity.y);
        yield return "success";
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void FixedTick(float fixedDeltaTime)
    {

    }

    public override void OnExit()
    {

    }


}
