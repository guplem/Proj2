using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected CharacterManager character { get; set; }
    public abstract void Tick(float deltaTime);
    public abstract void FixedTick(float fixedDeltaTime);
    public abstract void OnExit();

    public static void SetState(State newState, CharacterManager character)
    {
        // If only one of both is null or neither any of both is
        if ((character.state == null ^ newState == null) || (character.state != null && newState != null))
        {
            try // To ensue that a null state gives no problems. If one of both is null an exception will be catched.
            {
                // If both are the same state do not conitnue
                if (character.state.GetType() == newState.GetType())
                    return;
            }
            catch (System.NullReferenceException) { }

            // If one of both is null (jumped trugh carching exception)
            // ...or...
            // Old state is not null and neither is newState but both are different
            State.ForceSetState(newState, character);
        }
    }

    private static void ForceSetState(State newState, CharacterManager character)
    {
        if (character.state != null)
            character.state.OnExit();

        if (newState == null)
            Debug.LogWarning("Setting null state");

        character.state = newState;
    }
}
