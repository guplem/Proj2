using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBrain
{
    bool jumping { get; set; }
    bool interact { get; set; } //Push, pull, hide, pick, ... --> Interact with external elements
    bool action { get; set; } //Throw --> Player actions
    Vector2 direction { get; set; }
    CharacterManager characterManager { get; set; }

    void GetActions();

    void SetInteractState(bool state);

}
