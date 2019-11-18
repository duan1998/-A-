using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDiceNumberAction : Action
{
   
    public int m_addDiceNumber;

    public int m_diceOriginalValue;

    public override void UseAction(Dice dice)
    {
        if (m_isUsed)
            return;
        base.UseAction(dice);
        m_diceOriginalValue = dice.m_value;
        m_diceCtrl.ModifyDiceNumber(dice.m_index, dice.m_value+ m_addDiceNumber);
        
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
}
