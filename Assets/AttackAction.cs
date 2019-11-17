using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action
{
    public int m_attackDamage;

    public PlayerCtrl m_playerCtrl;

    private void Start()
    {
        m_actionType = ActionType.Attack;
    }

    public override void UseAction(Transform targetDiceTrans, int diceNumber)
    {
        if (m_isUsed)
            return;
        targetDiceTrans.GetComponent<DiceDrag>().HideDiceUI();
        targetDiceTrans.position = this.transform.position;
        m_isUsed = true;

        //PlayerMove
        m_playerCtrl.Attack(diceNumber);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}

