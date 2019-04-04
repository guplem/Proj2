using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    CharacterManager characterManager { get; set; }
    IState Initialize(CharacterManager characterManager);
    void Tick(float deltaTime);
    void FixedTick(float fixedDeltaTime);
}
