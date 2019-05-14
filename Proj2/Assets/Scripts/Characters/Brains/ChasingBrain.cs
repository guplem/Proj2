using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBrain : Brain
{

    public GameObject target { get; private set; }

    public ChasingBrain(CharacterManager characterManager, GameObject target)
    {
        base.Setup(characterManager);
        this.target = target;
    }

    protected override void GetActions(float deltaTime)
    {
        jumping = false;
        interact = false;
        actionHold = character.IsNextToPosition(target.transform.position, deltaTime); //(Vector2.Distance(character.transform.position, target.transform.position) <= 1.5f);
        crouch = false;
        direction = ((Vector2)target.transform.position - ((Vector2)character.transform.position)).normalized * 1.2f;
    }

    internal void LostTrackOfTarget()
    {
        Vector2[] dir = new Vector2[1];
        dir[0] = (target.transform.position - character.transform.position).normalized * 5f;
        Brain.SetBrain(new EnemyPatrolBrain(character, dir), 0f, character); // To keep going on the last known player direction for some time
        Brain.SetBrain(character.defaultBrain, 2f, character);
    }
}
