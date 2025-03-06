using FancyScrollView.Example08;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class PrescriptManager : MonoBehaviour
{
    public static PrescriptManager instance;
    public PrescriptionMessage SelectMessage = new PrescriptionMessage();
    public List<PrescriptionMessage> PrescriptList = new List<PrescriptionMessage>();
    public List<PrescriptionMessage> SearchList;
    public string customPath;
    private string filePath;
    public Action<PrescriptionMessage> OpenItem;
    public Action<PrescriptionMessage> RemoveItem;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;

            // �Զ��� D ��Ŀ¼
            
            if (!Directory.Exists(customPath))
            {
                Directory.CreateDirectory(customPath);
            }

            filePath = Path.Combine(customPath, "Prescriptions.json");

            LoadPrescriptions();
        }
        else
        {
            Destroy(gameObject);
        }
        OpenItem += OpenPrescription;
        RemoveItem += RemovePrescription;
    }


    public void AddList(PrescriptionMessage message)
    {
        if (PrescriptList.Count == message.ListId)
        {
            PrescriptList.Add(message);
        }
        else if (message.ListId >= 0 && message.ListId < PrescriptList.Count)
        {
            PrescriptList[message.ListId] = message;
        }
        SavePrescriptions();
    }

    public void NewPrescription()
    {
        if (SelectMessage != null)
        {
            SelectMessage = new PrescriptionMessage(PrescriptList.Count);
        }
        prescription.instance.SetPrescriptMessage(SelectMessage);
        prescription.instance.Setprescription(); // ��ʼ��������
        UIManager.Instance.ShowPanel(3);    
    }

    public void RemovePrescription(PrescriptionMessage message)
    {
        PrescriptList.Remove(message);
        for(int i = 0; i < PrescriptList.Count; i++)
        {
            PrescriptList[i].ListId = i;
        }
        Example08.instance.GenerateCells(PrescriptList, PrescriptList.Count);//��������Item
        Prescription_Description.Instance.RemoveDescription();//ɾ������������
        SavePrescriptions();
    }

    public void OpenPrescription(PrescriptionMessage message)
    {
        if (message != null)
        {
            prescription.instance.SetPrescriptMessage(message);
            prescription.instance.Setprescription(); // ��ʼ��������
            UIManager.Instance.ShowPanel(3);

        }
        else
        {
            //������δ��Ϊ��
        }

    }
    public void SavePrescriptions()
    {
        // 
        PrescriptionListWrapper wrapper = new PrescriptionListWrapper { Prescriptions = PrescriptList };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(filePath, json);
        Debug.Log("JSON �ļ��ѱ�����: " + filePath);
    }

    public void LoadPrescriptions()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            // 
            PrescriptionListWrapper wrapper = JsonUtility.FromJson<PrescriptionListWrapper>(json);
            if (wrapper != null)
            {
                PrescriptList = wrapper.Prescriptions;
            }
        }
    }
    [System.Serializable]
    public class PrescriptionListWrapper
    {
        public List<PrescriptionMessage> Prescriptions;
    }


    public void SearchPrescriptions(string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString)) return;

        SearchList = PrescriptList
            .Where(p => (!string.IsNullOrEmpty(p.Patient.Name) && p.Patient.Name.Contains(searchString)) ||
                        (!string.IsNullOrEmpty(p.Patient.Date) && p.Patient.Date.Contains(searchString)))
            .ToList();
    }

}
