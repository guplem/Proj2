using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullState : IState
{
    public CharacterManager characterManager { get; set; }

    public PushPullState(CharacterManager characterManager)
    {
        this.characterManager = characterManager;
    }

    public void FixedTick(float fixedDeltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void Tick(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        
    }
}
