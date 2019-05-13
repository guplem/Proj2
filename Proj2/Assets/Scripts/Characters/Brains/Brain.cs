using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brain
{
    public bool jumping { get; protected set; }
    public bool interact  { get; protected set; }
    public bool action { get; protected set; }
    public bool actionRelease  { get; protected set; }
    public bool crouch  { get; protected set; }
    public Vector2 direction  { get; protected set; }

    protected CharacterManager character;

    public void Act()
    {
        /*Pause could work by doing: if (gamemanager.instance.pause) --> Set all variables to false*/ //If doing so, remember removing the pause controller from PlayerInput

        GetActions();
        CheckAndFlip();
    }

    protected abstract void GetActions();

    protected void Setup(CharacterManager characterManager)
    {
        this.character = characterManager;
    }

    private void CheckAndFlip()
    {
        if (direction.x >= 0.1f)
        {
            character.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x < -0.1f)
        {
            character.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    private IEnumerator SetBrainDelayedCoroutine;
    public static void SetBrain(Brain newBrain, float delay, CharacterManager character)
    {
        if (character.brain.SetBrainDelayedCoroutine != null)
            character.StopCoroutine(character.brain.SetBrainDelayedCoroutine);

        character.brain.SetBrainDelayedCoroutine = character.brain.SetBrainDelayed(newBrain, delay);
        character.StartCoroutine(character.brain.SetBrainDelayedCoroutine);
    }

    private IEnumerator SetBrainDelayed(Brain newBrain, float delay)
    {
        yield return new WaitForSeconds(delay);

        character.brain = newBrain;

        SetBrainDelayedCoroutine = null;
    }

}