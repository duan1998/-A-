using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{

    public int m_moveRange;

    public PlayerCtrl m_playerCtrl;

    private void Start()
    {
        m_actionType = ActionType.Move;
    }

    public override void UseAction(Transform targetDiceTrans, int diceNumber)
    {
        if (m_isUsed)
            return;
        targetDiceTrans.position = this.transform.position;
        targetDiceTrans.GetComponent<DiceDrag>().HideDiceUI();
        m_isUsed = true;

        //PlayerMove
        m_playerCtrl.Move(diceNumber);

        this.gameObject.transform.parent.gameObject.SetActive(false);
    }
}
