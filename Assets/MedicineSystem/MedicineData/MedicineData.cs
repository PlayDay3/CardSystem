using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class MedicineData
{   
    

    public MedicineMessage MedicineMessage=new MedicineMessage();

    public BaseEnum unit;//��λ
    public Times Times;//���ô���

    public int Day;//��������
    public float eatdose;//һ�η��ü���
    public string usage;//�÷�
    public Useway Useway;
    public int number;
    public int addnumber;//�������
    public int BoxNumber;
    public int granuleNumber;
    public float originalCost;//������
    public float sellPrice;//���ۼ�
    public long  ProductionDate;//����ʱ��
    public long validity;//��Чʱ��
    public int RemainTime;//ʣ��ʱ��
    public int ListID;


    public MedicineData(MedicineData other)
    {
        //Debug.Log(other.addnumber);
        this.MedicineMessage = other.MedicineMessage;
        this.unit = other.unit;
        this.Times = other.Times;
        this.MedicineMessage.doseunit=other.MedicineMessage.doseunit;
        this.MedicineMessage.minunit=other.MedicineMessage.minunit;
        this.MedicineMessage.maxunit=other.MedicineMessage.maxunit;
        this.MedicineMessage.dosage=other.MedicineMessage.dosage;
        this.usage= other.usage;
        Useway= other.Useway;
        this.Day = other.Day;
        this.eatdose = other.eatdose;
        this.number = other.number;
        this.addnumber = other.addnumber;
        this.BoxNumber = other.BoxNumber;
        this.granuleNumber = other.granuleNumber;
        this.originalCost = other.originalCost;
        this.sellPrice = other.sellPrice;
        this.ProductionDate = other.ProductionDate;
        this.validity = other.validity;
        this.RemainTime = other.RemainTime;
        this.ListID = other.ListID;
    }
    public MedicineData()
    {

    }
    public MedicineData(MedicineMessage other)
    {

        this.MedicineMessage = other;

    }
    public MedicineData(int index)
    {
        this.ListID = index;
        this.number = 1;
        this.MedicineMessage.capacity = 1;
    }


}
