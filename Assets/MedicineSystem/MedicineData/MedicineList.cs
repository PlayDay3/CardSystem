using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MedicineList : MonoBehaviour
{
    public static MedicineList instance;
   
    public List<MedicineData> MedicList=new List<MedicineData>();
    public MedicineMessage Medicine;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
       
    }
   


    [ContextMenu("text")]
    public void addMedicine()
    {
        // ͨ���˻��������ҩ����Ϣ
        MedicineData medicine = new MedicineData();
        medicine.unit = BaseEnum.granule;
        medicine.number = 1;
        medicine.originalCost = 1;
        medicine.MedicineMessage = Medicine;

        // �� Unix ʱ��������룩�洢��������
        medicine.ProductionDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        MedicList.Add(medicine);
    }

    [ContextMenu("SubList")]
    public void SubList()
    {
        MedicList.RemoveAt(MedicList.Count - 1);
    }

}
