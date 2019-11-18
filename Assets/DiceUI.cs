using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DiceUI : MonoBehaviour
{
    public Text[] m_diceTexts;
    private Vector3[] m_dicePositions;


    public void UpdateDiceUIInNextTurn(int[] diceNumber)
    {

        if(m_dicePositions==null)
        {
            m_dicePositions = new Vector3[m_diceTexts.Length];
            for (int i = 0; i < 3; i++)
                m_dicePositions[i] = m_diceTexts[i].transform.parent.position;
        }

        if(diceNumber.Length!= m_diceTexts.Length)
        {
            Debug.LogError("数据不一致");
        }

        for(int i=0;i< diceNumber.Length; i++)
        {
            m_diceTexts[i].transform.parent.gameObject.SetActive(true);
            m_diceTexts[i].text = diceNumber[i].ToString();
            m_diceTexts[i].transform.parent.position = m_dicePositions[i];
            m_diceTexts[i].GetComponentInParent<DiceDrag>().m_diceIndex = i;
        }
    
    }
    /// <summary>
    /// 更新UI在当前轮（意味着不会重置为true，只是更新数值
    /// </summary>
    public void UpdateDiceUIInCurrentTurn(int[] diceNumber)
    {
        for (int i = 0; i < m_diceTexts.Length; i++)
        {
            m_diceTexts[i].text = diceNumber[i].ToString();
        }
    }



    
}
