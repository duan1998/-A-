using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MapItemType
{
    Null,
    Player,
    Enemy
}
public class MapItem : MonoBehaviour
{

    public Vector2Int m_itemPosVector2;

    // Start is called before the first frame update


    public MapItemType m_currentType;

    public bool m_isWalkable;
    public bool m_isAttackable;

}
    