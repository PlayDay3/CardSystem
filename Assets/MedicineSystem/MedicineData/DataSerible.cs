using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DataSerible 
{
    public string MedicineName; // ���� ScriptableObject ������
    public int number;
    public int addnumber;
    public int Boxnumber;
    public int granuleNumber;
    public BaseEnum unit;
    public float originalCost; // ������
    public float sellPrice;    // ���ۼ�
    public string  ProductionDate;//����ʱ��
    public string  validity;//��Чʱ��
    public int RemainTime;//ʣ��ʱ��
    public int ListID;
}
