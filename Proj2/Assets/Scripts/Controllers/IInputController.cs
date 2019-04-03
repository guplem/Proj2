using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputController
{
    
    bool jumping { get; set; }
    bool interact { get; set; } //Push, pull, hide, pick, ...
    bool action { get; set; } //Throw
    float horizontalAxis { get; set; }
    float verticalAxis { get; set; }

    void ReadInput();

}
