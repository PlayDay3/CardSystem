using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool isSelect;
    public bool isChange;

    public void SelectGrid()
    {
        //����������ȡ��ѡ��
        isSelect = true;
    }
    public void ChangeGrid()
    {
        isChange = true;
    }


}
