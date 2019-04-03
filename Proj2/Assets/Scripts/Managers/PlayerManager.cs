using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICharacterManager
{

    public Rigidbody2D rb2d { get; set; }

    public Animator animator { get; set; }

    public AudioManager audioManager { get; set; }

    public IMovementController movementController { get; set; }
    public IInputController inputController { get; set; }
    public CharacterProperties characterProperties { get; set; }

    public IState state;

    // private 

    private void Start()
    {

    }

    private void Update()
    {
        state.Tick( Time.deltaTime);
        CheckTransition();
        inputController.ReadInput();
    }

    private void FixedUpdate()
    {
        state.FixedTick(Time.deltaTime);
    }

    private void CheckTransition()
    {
        if (inputController.jumping)
        {
            //Check transitions
        }
    }

    private void ChangeState(IState newState)
    {
        state = newState;
    }
}
