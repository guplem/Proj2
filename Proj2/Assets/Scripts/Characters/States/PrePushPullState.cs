using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrePushPullState : State
{
    Interactable interactable;

    private IEnumerator pushPullSetup;
    public PrePushPullState(CharacterManager characterManager, Interactable interactable)
    {
        this.character = characterManager;
        this.interactable = interactable;

        character.characterProperties.internalVelocity = character.characterProperties.maxWalkVelocity * 0.35f;
    }

    protected override IEnumerator StartState()
    {
        character.visualsAnimator.SetTrigger("Walk");

        character.rb2d.velocity = Vector3.zero;

        try
        {
            Vector2 pushPosition;
            ActivableBox boxObject = interactable.GetComponent<ActivableBox>();
            if (boxObject.gameObject.transform.position.x > character.transform.position.x)
            {
                pushPosition = new Vector2(boxObject.transform.position.x - 1.25f, character.transform.position.y);
            }
            else
            {
                pushPosition = new Vector2(boxObject.transform.position.x + 1.25f, character.transform.position.y);
            }
            pushPullSetup = MoveTowardsPosition(pushPosition);
            character.brain.LookAt(pushPosition);
            character.StartCoroutine(pushPullSetup);
        }
        catch
        {
            Debug.LogWarning("PushPull doesn't work correctly with a box!", interactable.gameObject);
        }
        yield return "success";
    }

    public IEnumerator MoveTowardsPosition(Vector2 position)
    {
        Vector2 direction = new Vector3(position.x, position.y, 0) - character.transform.position;
        do
        {
            character.movementController.MoveTowards(direction, character.characterProperties.acceleration, character.characterProperties.internalVelocity);
            yield return new WaitForEndOfFrame();
        } while (Mathf.Abs(position.x - character.transform.position.x) > 0.05f);
        
        character.transform.position = position;
        State.SetState(new PushPullState(character, interactable), character);
    }


    public override void OnExit()
    {
        character.StopCoroutine(pushPullSetup);
    }

    public override void Tick(float deltaTime) { }

    public override void FixedTick(float fixedDeltaTime) { }
}
