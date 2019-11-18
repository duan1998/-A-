using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDiceNumberAction : Action
{
   
    public int m_addDiceNumber;

    public int m_diceOriginalValue;


    public Animator m_anim;

    public override void UseAction(Dice dice)
    {
        if (!IsCanUse(dice))
        {
            m_anim.Play("Shake");   
            return;
        }
        base.UseAction(dice);
        m_diceOriginalValue = dice.m_value;
        m_diceCtrl.ModifyDiceNumber(dice.m_index, dice.m_value+ m_addDiceNumber);
        m_playerCtrl.m_currentPlayerActionType = PlayerActionType.CanAddNumber;
    }

    public override void CancelOperation(Dice dice)
    {
        base.CancelOperation(dice);
        m_diceCtrl.ModifyDiceNumber(dice.m_index, m_diceOriginalValue);
    }

    public override void OverAction(Dice dice)
    {
        base.OverAction(dice);
        
    }

    bool IsCanUse(Dice dice)
    {
        if (m_isUsed || dice.m_value + m_addDiceNumber > 6)
            return false;
        return true;
    }
}
