using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowControl : MonoBehaviour
{
    public static WindowControl instance;
    public MedicineData MedicineData=new MedicineData();
    public Row Row;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance= this;
        }
    }


    public void SetWindowData(MedicineData TempData)
    {
        MedicineData=TempData;
        //this.transform.GetChild(0).GetComponent<Row>().medicineData = TempData;
        //this.transform.GetChild(0).GetComponent<MedicineData>().MedicineMessage = TempData.MedicineMessage;
        //this.transform.GetChild(0).GetComponent<MedicineData>().unit = TempData.unit;
        //this.transform.GetChild(0).GetComponent<MedicineData>().number = TempData.number;
        //this.transform.GetChild(0).GetComponent<MedicineData>().addnumber = TempData.addnumber;
        //this.transform.GetChild(0).GetComponent<MedicineData>().BoxNumber = TempData.BoxNumber;
        //this.transform.GetChild(0).GetComponent<MedicineData>().granuleNumber= TempData.granuleNumber;
        //this.transform.GetChild(0).GetComponent<MedicineData>().originalCost= TempData.originalCost;
        //this.transform.GetChild(0).GetComponent<MedicineData>().sellPrice= TempData.sellPrice;
        //this.transform.GetChild(0).GetComponent<MedicineData>().ProductionDate= TempData.ProductionDate;
        //this.transform.GetChild(0).GetComponent<MedicineData>().validity= TempData.validity;
        //this.transform.GetChild(0).GetComponent<MedicineData>().RemainTime= TempData.RemainTime;
        //this.transform.GetChild(0).GetComponent<MedicineData>().ListID=TempData.ListID;
        //����ҳ��
        Row.medicineData = TempData;
        Row.InitializeItem();   
    }



    public void AddBankList()
    {
        this.transform.GetChild(0).GetComponent<Row>().changeNumber();
        this.transform.GetChild(0).GetComponent<Row>().returnItem();
        TableManager.Instance.AddBankList(this.transform.GetChild(0).GetComponent<Row>().medicineData);
        MedicineSerializable.instance.SaveData();
    }
    public void AddNameList()
    {
        this.transform.GetChild(0).GetComponent<Row>().changeNumber();
        this.transform.GetChild(0).GetComponent<Row>().returnItem();
        TableManager.Instance.AddNameList(this.transform.GetChild(0).GetComponent<Row>().medicineData);
        MedicineSerializable.instance.SaveNameList();
        TableManager.Instance.ChnageList();
    }
    public void SendMedicineList()
    {
        TableManager.Instance.SendSellList();
    }
    public void AddNameMedicine()
    {
        MedicineData = new MedicineData(TableManager.Instance.NameTable.Count);
        Row.medicineData = MedicineData;
        Row.InitializeItem();
    }

    public void RemoveMedicine()
    {
        Row.ReturnToList();
        TableManager.Instance.RemoveMedicine(Row.medicineData);
        SetWindowData(null);        //���WindowControl����
    }

    public void SellWindow(bool isSell)
    {

            this.transform.GetChild(0).Find("��������").gameObject.SetActive(!isSell);
            this.transform.GetChild(0).Find("��Ч��").gameObject.SetActive(!isSell);
            this.transform.GetChild(0).Find("ԭ��").gameObject.SetActive(!isSell);
            //this.transform.GetChild(0).Find("�������").gameObject.SetActive(!isSell);
            this.transform.GetChild(0).Find("�������").gameObject.SetActive(!isSell);
            this.transform.GetChild(0).Find("���μ���").gameObject.SetActive(isSell);
            this.transform.GetChild(0).Find("���ô���").gameObject.SetActive(isSell);
            this.transform.GetChild(0).Find("��������").gameObject.SetActive(isSell);
            this.transform.GetChild(0).Find("�÷�").gameObject.SetActive(isSell);
    }
}
