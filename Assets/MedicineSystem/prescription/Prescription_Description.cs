using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class Prescription_Description : MonoBehaviour
{
    // Start is called before the first frame update
    public static Prescription_Description Instance;
    public TextMeshProUGUI Description;
    public Action<PrescriptionMessage> OnItemClicked;
    public PrescriptionMessage Message=new PrescriptionMessage();
    public Button OpenButton;
    public Button RemoveButton;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        OnItemClicked += SetDescription;
        OpenButton.onClick.AddListener(() =>
            PrescriptManager.instance.OpenItem(Message)
        );
        RemoveButton.onClick.AddListener(() =>
            PrescriptManager.instance.RemoveItem?.Invoke(Message)     
        );
    }

    public void SetDescription(PrescriptionMessage message)
    {
        Message=message;
        Description.text ="姓名:"+ message.Patient.Name + "\n";
        Description.text += message.Patient.Description + "\n";
        foreach(MedicineData data in message.MedicineLsit)
        {
            Description.text += data.MedicineMessage.Name + "\n";

        }
        Description.text += "花费:"+message.SumCost+"\n";
        Description.text += message.Patient.Date;
    }
    public void RemoveDescription()
    {
        Message = null;
        Description.text = "选择处方单";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
