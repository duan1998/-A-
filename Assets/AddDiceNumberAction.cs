using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDiceNumberAction : Action
{
    public DiceCtrl m_diceCtrl;
    public int m_addDiceNumber;

    private void Start()
    {
        m_actionType = ActionType.AddDiceNumber;
    }

    public override void UseAction(Transform targetDiceTrans, int diceNumber)
    {
        if (m_isUsed)
            return;
        targetDiceTrans.position = this.transform.position;
        m_isUsed = true;
        m_diceCtrl.DiceAddNumber(targetDiceTrans.GetOrderOfBrother(), m_addDiceNumber);
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
