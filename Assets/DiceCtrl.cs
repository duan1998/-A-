using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCtrl : MonoBehaviour
{

    private int[] m_diceNumbers;
    public DiceUI m_diceUI;

    public int[] _DiceNumbers
    {
        get { return m_diceNumbers; }
    }


    private void Start()
    {
        m_diceNumbers = new int[3];
    }


    /// <summary>
    /// 获取三个随机数
    /// </summary>
    public void GetNewThreeDice()
    {
        m_diceNumbers[0] = Random.Range(1, 7);
        m_diceNumbers[1] = Random.Range(1, 7);
        m_diceNumbers[2] = Random.Range(1, 7);
        m_diceUI.UpdateDiceUIInNextTurn(m_diceNumbers);
    }

    public void DiceAddNumber(int index,int value) 
    {
        m_diceNumbers[index] += value;
        if (m_diceNumbers[index] > 6)
            m_diceNumbers[index] = 6;
        m_diceUI.UpdateDiceUIInCurrentTurn(m_diceNumbers);

    }





}
