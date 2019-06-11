using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain
{
    public bool jumping { get; protected set; }
    public bool interact  { get; protected set; }
    public bool actionHold { get; protected set; }
    public bool actionDown  { get; protected set; }
    public bool crouch  { get; protected set; }
    public Vector2 direction  { get; protected set; }

    protected CharacterManager character;

    private Vector3 lastSavedPosition;
    private int stuckedFrames = 0, maxStuckedFrames = 30, consecutiveStuck = 0;

    public void Act(float deltaTime)
    {
        /*Pause could work by doing: if (gamemanager.instance.pause) --> Set all variables to false*/ //If doing so, remember removing the pause controller from PlayerInput

        GetActions(deltaTime);

        if ((direction != Vector2.zero) && (character.transform.position == lastSavedPosition))
        {
            stuckedFrames++;
            if (stuckedFrames >= maxStuckedFrames)
            {
                stuckedFrames = 0;
                consecutiveStuck++;
                if (consecutiveStuck >= 2)
                {
                    if(character.defaultBrain is EnemyPatrolBrain)
                    {
                        character.defaultBrain = new EnemyPatrolBrain(character, new Vector2[] { (Vector2)character.transform.position + Vector2.right*-direction.x } );
                    }
                }
                Brain.SetBrain(character.defaultBrain, 0f, character, false);
            }
        }
        else
        {
            stuckedFrames = 0;
            consecutiveStuck = 0;
            lastSavedPosition = character.transform.position;
        }
    }

    internal void LookAt(Vector2 targetPosition)
    {
        direction = (targetPosition - (Vector2)character.transform.position).normalized;
        CheckAndFlip();
    }

    protected abstract void GetActions(float deltaTime);

    protected void Setup(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    public void CheckAndFlip()
    {
        if (direction.x >= 0.1f)
        {
            Utils.SetObjectLookingDirection(1, character.gameObject);
        }
        else if (direction.x < -0.1f)
        {
            Utils.SetObjectLookingDirection(-1, character.gameObject);
        }
    }




    private IEnumerator SetBrainDelayedCoroutine;
    public static void SetBrain(Brain newBrain, float delayOnSetBrain, CharacterManager character, bool overrideBrainChangesInProcess)
    {
        if (overrideBrainChangesInProcess && character.brain.SetBrainDelayedCoroutine != null)
            character.StopCoroutine(character.brain.SetBrainDelayedCoroutine);

        character.brain.SetBrainDelayedCoroutine = character.brain.SetBrainDelayed(newBrain, delayOnSetBrain);
        character.StartCoroutine(character.brain.SetBrainDelayedCoroutine);
    }

    private IEnumerator SetBrainDelayed(Brain newBrain, float delay)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        character.brain = newBrain;
        character.characterProperties.internalVelocity = character.characterProperties.maxWalkVelocity;

        SetBrainDelayedCoroutine = null;
    }

}