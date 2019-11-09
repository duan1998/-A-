using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float m_moveSpeed;

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
            {
                if(hit.collider.CompareTag("MapItem"))
                {
                    //在2Dmap上记录目标点(终点)
                    Vector2Int targetPos = hit.collider.GetComponent<MapItem>().m_itemPosVector2;
                    //记录起点当前位置
                    Vector2Int startPos = GetCurrentPos();

                    StopAllCoroutines();
                    index = 0;
                    //通过某算法获得一位置数组
                    StartCoroutine("MoveTo", Vector2IntSwitchVector3(AStar.AutomaticPathFinding(startPos, targetPos).ToArray()));
                }
            }
        }
        
        


    }
    Vector2Int GetCurrentPos()
    {
        return new Vector2Int(transform.position.x - (int)transform.position.x > 0.5f ? (int)(transform.position.x + 1) : (int)transform.position.x,
            transform.position.z - (int)transform.position.z > 0.5f ? (int)(transform.position.z + 1) : (int)transform.position.z);
    }

    Vector3[] Vector2IntSwitchVector3(Vector2Int[] targets)
    {
        Vector3[] tempArrs = new Vector3[targets.Length];
        for(int i=0;i< targets.Length;i++)
        {
            tempArrs[i] = new Vector3(targets[i].x, transform.position.y, targets[i].y);
        }
        return tempArrs;
    }


    int index;
    IEnumerator MoveTo(Vector3[] targets)
    {
        while (true)
        {
            float step = m_moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targets[index], step);
            if (Vector3.Distance(transform.position, targets[index]) < 0.001f)
            {
                if (index == targets.Length - 1)
                {
                    print("寻路结束");
                    break;
                }
                else
                {
                    index++;
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
    }


    

}
