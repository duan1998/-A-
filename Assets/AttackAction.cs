using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    public int m_attackDamage;

    public PlayerCtrl m_playerCtrl;

    

    public override void UseAction(Dice dice)
    {
        if (m_isUsed)
            return;
        base.UseAction(dice);
        //PlayerMove
        m_playerCtrl.PrepareAttack(dice.m_value);
        
        

    }
    public override void CancelOperation(Dice dice)
    {
        base.CancelOperation(dice);
        MapMgr.Instance.HideAttackableItem();
    }

    public override void OverAction(Dice dice)
    {
        base.OverAction(dice);
        dice.m_trans.gameObject.SetActive(false);
    }
}

