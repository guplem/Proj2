using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    ICharacterManager characterManager { get; set;  }
    void Initialize();
    void Tick(float deltaTime);
    void FixedTick(float fixedDeltaTime);
}
