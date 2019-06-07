using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    float hitStrength;
    Vector2 hitDirection;

    public DeadState(float hitStrength, Vector2 hitDirection)
    {
        this.hitStrength = hitStrength;
        this.hitDirection = hitDirection.normalized;
    }

    protected override IEnumerator StartState()
    {
        character.rb2d.AddForce(hitDirection * hitStrength, ForceMode2D.Impulse);

        character.visualsAnimator.SetTrigger("Dead");

        character.audioController.PlaySound(character.deadSound, false, false);

        PlayerManager pm = null;
        try
        {
            pm = (PlayerManager)character;
        }
        catch (System.InvalidCastException) { pm = null; }

        if (pm != null)
        {
            GUIManager.Instance.DeathScreenPanel.SetObjectActive(true);

            yield return new WaitForSeconds(GUIManager.Instance.DeathScreenPanel.fadeTimeDuration);

            GameManager.Instance.RevivePlayer();
        }


        yield return "success";
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void FixedTick(float fixedDeltaTime)
    {

    }

    public override void OnExit()
    {
        GUIManager.Instance.DeathScreenPanel.SetObjectActive(false);
    }

}