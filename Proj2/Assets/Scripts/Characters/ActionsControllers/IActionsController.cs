using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionsController
{
    
    bool jumping { get; set; }
    bool interact { get; set; } //Push, pull, hide, pick, ...
    bool action { get; set; } //Throw
    Vector2 direction { get; set; }
    CharacterManager characterManager { get; set; }

    void ReadInput();

}
