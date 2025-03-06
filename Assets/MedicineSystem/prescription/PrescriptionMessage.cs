using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.AddressableAssets.Build.Layout.BuildLayout;

[System.Serializable]
public class PrescriptionMessage
{
    // Start is called before the first frame update
    [System.Serializable]
    public struct PatientMessage
    {
        public string Name;
        public string age;
        public string Date;
        public string Sex;
        public string Description;
        public string Location;
        public string number;
        public float T;//����
        public float P;//����
        public float BP_b;
        public float BP_p;
        public float Blood_Sugar;
    }
    public PatientMessage Patient;
    public int ListId;
    public string PrescriptionId;
    public string Doctor;
    public string Check;
    public float SignCost;
    public float InjectionCost;
    public float TreatmentCost;
    public float MedicineCost;
    public float OriginalCost;
    public float OtherCost;
    public float SumCost;
    public float Profit;
    public List<MedicineData> MedicineLsit;
    //public bool isSave = false;
    public bool isPaid = false;


    public PrescriptionMessage(PrescriptionMessage Message)
    {
        Patient = Message.Patient;
        ListId = Message.ListId;
        Doctor= Message.Doctor;
        Check= Message.Check;
        PrescriptionId= Message.PrescriptionId;
        MedicineCost = Message.MedicineCost;
        OriginalCost= Message.OriginalCost;
        isPaid = Message.isPaid;
        SignCost= Message.SignCost;
        InjectionCost= Message.InjectionCost;
        TreatmentCost= Message.TreatmentCost;
        OtherCost=Message.OtherCost;
        SumCost = Message.SumCost;
        Profit= Message.Profit;
        MedicineLsit = Message.MedicineLsit != null ? new List<MedicineData>(Message.MedicineLsit) : new List<MedicineData>();

    }
    public PrescriptionMessage(int Index)
    {
        this.Patient.Name = "Name";
        this.Patient.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        ListId = Index;

        // ��ȡ��ǰ�����£�yyyy-MM ��ʽ��
        string currentMonth = DateTime.Now.ToString("yyyy-MM");

        // ��ȡ�����Ѿ����ڵĵ�ǰ�µ�PrescriptionId�������ʽΪ "yyyy-MM-xxx"��
        var existingIds = PrescriptManager.instance.PrescriptList
            .Where(pm => pm.PrescriptionId.StartsWith(currentMonth))  // ֻ�����Ե�ǰ�·ݿ�ͷ��ID
            .Select(pm => pm.PrescriptionId.Substring(8)) // ��ȡ����Ų��֣��� "xxx"������ʽ�� "yyyy-MM-" ��ʼ
            .Select(id => int.TryParse(id, out var num) ? num : (int?)null) // ת��Ϊ����
            .Where(num => num.HasValue) // ���˵���Чֵ
            .OrderBy(num => num.Value) // ������������
            .ToList();

        // Ѱ����С��ȱʧ���
        int newIdNumber = 1;
        foreach (var existingId in existingIds)
        {
            if (existingId != newIdNumber) // ����п�ȱ���
            {
                break; // �ҵ���һ����ȱ�ı��
            }
            newIdNumber++;
        }

        // �����µ� PrescriptionId����ʽΪ "yyyy-MM-xxx"��xxxΪ��ǰ�µ�ȱʧ��ţ�
        this.PrescriptionId = currentMonth + "-" + newIdNumber.ToString("D3");

    }
    public PrescriptionMessage() {
        this.Patient.Name = "Name";
        this.Patient.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

    }


}
