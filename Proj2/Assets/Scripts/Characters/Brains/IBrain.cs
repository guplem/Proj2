using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Brain
{
    
    bool jumping { get; set; }
    bool interact { get; set; } //Push, pull, hide, pick, ...
    bool action { get; set; } //Throw
    Vector2 direction { get; set; }

    void GetActions();

}
