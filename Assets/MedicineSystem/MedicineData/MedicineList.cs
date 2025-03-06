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
        // 通过人机交互添加药物信息
        MedicineData medicine = new MedicineData();
        medicine.unit = BaseEnum.granule;
        medicine.number = 1;
        medicine.originalCost = 1;
        medicine.MedicineMessage = Medicine;

        // 以 Unix 时间戳（毫秒）存储生产日期
        medicine.ProductionDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        MedicList.Add(medicine);
    }

    [ContextMenu("SubList")]
    public void SubList()
    {
        MedicList.RemoveAt(MedicList.Count - 1);
    }

}
