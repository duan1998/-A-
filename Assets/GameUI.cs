using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    public Text m_actorText;
    public Button m_endTurnbutton;
 


    public void SetActonUI(Actors actors)
    {
        if (actors == Actors.AI)
        {
            SwitchToAIAction();
        }
        else
            SwitchToPlayerAction();
    }

    void SwitchToPlayerAction()
    {
        m_actorText.text ="第"+GameManager.Instance._CurrentTurnNumber.ToString()+"回合\n"+@"当前行动方:<color=""red"">玩家</color>";
        m_endTurnbutton.interactable = true;
        m_skillRootObj.SetActive(true);
        
    }
    void SwitchToAIAction()
    {
        m_actorText.text = "第" + GameManager.Instance._CurrentTurnNumber.ToString() + "回合\n"+@"当前行动方:<color=""red"">敌人</color>";
        m_endTurnbutton.interactable = false;
        m_skillRootObj.SetActive(false);
    }


    public void OnEndTurnButtonClick()
    {
        GameManager.Instance.NextTurn();
    }




    //角色行为+技能

    public GameObject m_skillRootObj;
    

    



}
