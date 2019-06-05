using System.Collections;
using UnityEngine;

public class JumpingState : State
{

    public JumpingState(CharacterManager characterManager)
    {
        this.character = characterManager;
        // timeToStopJumping = Time.time + characterManager.characterProperties.jumpTime;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Jump");

        character.movementController.Jump(character.characterProperties.jumpForce, ForceMode2D.Impulse);

        try
        {
            character.rb2d.sharedMaterial = ((PlayerProperties)(character.characterProperties)).OnAirMaterial;
        }
        catch (System.InvalidCastException)
        { }

        PhysicsMaterial2D pMat = new PhysicsMaterial2D();
        pMat.friction = ((PlayerManager)character).onAirMaterial.friction;
        pMat.bounciness = ((PlayerManager)character).onAirMaterial.bounciness;

        foreach (Collider2D collider in character.gameObject.GetComponents<Collider2D>())
        {
            collider.sharedMaterial = pMat;
        }

        yield return "success";
    }

    public override void Tick(float deltaTime)
    {
        /*if (Time.time >= timeToStopJumping || !characterManager.brain.jumping)
        {
            //characterManager.ChangeState(new OnAirState(), characterManager);
            characterManager.behaviourTree.SetNextState(true);
        }*/
    }

    public override void FixedTick(float fixedDeltaTime)
    {
        character.movementController.MoveTowards(new Vector2(character.brain.direction.x, 0), new Vector2(character.characterProperties.acceleration.x * 0.5f, character.characterProperties.acceleration.y*0.5f), character.characterProperties.maxOnAirVelocity);
        //character.behaviourTree.CalculateAndSetNextState(true);
    }

    public override void OnExit()
    {
        try
        {
            character.rb2d.sharedMaterial = ((PlayerProperties)(character.characterProperties)).DefaultMaterial;
        }
        catch (System.InvalidCastException)
        { }

        PhysicsMaterial2D pMat = new PhysicsMaterial2D();
        pMat.friction = ((PlayerManager)character).defaultMaterial.friction;
        pMat.bounciness = ((PlayerManager)character).defaultMaterial.bounciness;

        foreach (Collider2D collider in character.gameObject.GetComponents<Collider2D>())
        {
            collider.sharedMaterial = pMat;
        }
    }
}