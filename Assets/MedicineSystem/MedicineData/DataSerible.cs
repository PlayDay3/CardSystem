using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DataSerible 
{
    public string MedicineName; // 保存 ScriptableObject 的名称
    public int number;
    public int addnumber;
    public int Boxnumber;
    public int granuleNumber;
    public BaseEnum unit;
    public float originalCost; // 进货价
    public float sellPrice;    // 销售价
    public string  ProductionDate;//生产时间
    public string  validity;//有效时间
    public int RemainTime;//剩余时间
    public int ListID;
}
