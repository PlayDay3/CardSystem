using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool isSelect;
    public bool isChange;

    public void SelectGrid()
    {
        //让所有网格取消选择
        isSelect = true;
    }
    public void ChangeGrid()
    {
        isChange = true;
    }


}
