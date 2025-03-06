using UnityEngine;
using UnityEngine.UI;
using System.IO;
//using UnityEditor.VersionControl;
using System.Collections.Generic;
using TMPro;
using FancyScrollView.Example08;
using System;
using System.Collections;
using UnityEngine.AI;

public class prescription : MonoBehaviour
{
    public static prescription instance;
    public Canvas diagnosisCanvas; // ��ϵ��� Canvas
    public Camera uiCamera; // ��Ⱦ��ϵ��� UI �����
    public int imageWidth = 1920; // ��ͼ���
    public int imageHeight = 1080; // ��ͼ�߶�
    public string savePath = "DiagnosisReport.png"; // ����·��
    public GameObject BackGround;
    public PrescriptionMessage PrescriptionMessage;
    public string saveDirectory;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        this.gameObject.SetActive(false);
    }


    // ������ϵ�ΪͼƬ
    private Rect GetScreenRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // ��ȡ UI ����Ļ�ϵ��ĸ��ǵ�����
        Vector2 bottomLeft = RectTransformUtility.WorldToScreenPoint(null, corners[0]);
        Vector2 topRight = RectTransformUtility.WorldToScreenPoint(null, corners[2]);

        // ����ʵ�ʿ��
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        // ���� CanvasScaler Ӱ��
        //Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
        //if (canvas != null && canvas.renderMode != RenderMode.WorldSpace)
        //{
        //    float scaleFactor = canvas.scaleFactor;
        //    width /= scaleFactor;
        //    height /= scaleFactor;
        //}

        // �������յ���Ļ Rect
        //width += 50;
        float x = bottomLeft.x;
        float y = Screen.height - topRight.y;  // ReadPixels ��Ҫ���½�Ϊ���

        Debug.Log($"Final Rect: x={x}, y={y}, width={width}, height={height}");


        return new Rect(x, y, width, height);
    }

    public void SaveDiagnosisReport()
    {
        StartCoroutine(CaptureUIElement());
    }

    private IEnumerator CaptureUIElement()
    {
        yield return new WaitForEndOfFrame(); // ȷ�� UI ��ȫ��Ⱦ

        // ���� BackGround ����Ļ�ϵ� Rect
        Rect rect = GetScreenRect(BackGround.GetComponent<RectTransform>());

        // ȷ�� rect ��С��Ч
        if (rect.width <= 0 || rect.height <= 0)
        {
            Debug.LogError("Invalid UI size for screenshot!");
            yield break;
        }

        // ���� Texture2D ���н�ͼ
        Texture2D texture = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        texture.ReadPixels(rect, 0, 0);
        texture.Apply();

        // �����ļ���
        string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"{PrescriptionMessage.Patient.Name}{timestamp}.png";

        // ��� saveDirectory Ϊ�գ���ʹ��Ĭ��·��
        if (string.IsNullOrEmpty(saveDirectory))
        {
            saveDirectory = Application.persistentDataPath;
        }

        // ȷ��Ŀ¼����
        if (!Directory.Exists(saveDirectory))
        {
            Directory.CreateDirectory(saveDirectory);
        }

        // �������·��
        string fullPath = Path.Combine(saveDirectory, fileName);

        // ����Ϊ PNG
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(fullPath, bytes);

        Debug.Log("Diagnosis report saved at: " + fullPath);
    }



    public void Setprescription()
    {
        TMP_InputField Field;
        //�������
        InitializedPrescription();
        Field = BackGround.transform.Find("Id").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.PrescriptionId;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.Name;

        Field = BackGround.transform.Find("�Ա�").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.Sex;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.age;

        Field = BackGround.transform.Find("��ַ").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.Location;

        Field = BackGround.transform.Find("�绰").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.number;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.Date;

        Field = BackGround.transform.Find("���").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.Description;

        Field = BackGround.transform.Find("ҽʦ").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Doctor;

        Field = BackGround.transform.Find("���").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Check;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.MedicineCost.ToString();

        TextMeshProUGUI TempText = BackGround.transform.Find("����").GetChild(1).GetComponent<TextMeshProUGUI>();
        TempText.text =$"({PrescriptionMessage.OriginalCost})" ;

        Field = BackGround.transform.Find("�Һŷ�").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.SignCost.ToString();

        Field = BackGround.transform.Find("ע���").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.InjectionCost.ToString();

        Field = BackGround.transform.Find("���Ʒ�").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.TreatmentCost.ToString();

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.OtherCost.ToString();

        Field = BackGround.transform.Find("�ϼ�").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.SumCost.ToString();

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Profit.ToString();

        Field = BackGround.transform.Find("����").GetChild(1).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.T.ToString();

        Field = BackGround.transform.Find("����").GetChild(1).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.P.ToString();

        Field = BackGround.transform.Find("Ѫѹ").GetChild(1).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.BP_b.ToString();

        Field = BackGround.transform.Find("Ѫѹ").GetChild(2).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.BP_p.ToString();

        Field = BackGround.transform.Find("Ѫ��").GetChild(1).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.Patient.Blood_Sugar.ToString();
        TextMeshProUGUI TempPrescription = BackGround.transform.Find("Rx")?.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        TempPrescription.text="\n";
        
        foreach(MedicineData message in PrescriptionMessage.MedicineLsit)
        {
            TempPrescription.text += message.MedicineMessage.Name +"    "+message.MedicineMessage.dosage+message.MedicineMessage.doseunit+"*"+
               message.MedicineMessage.capacity + message.MedicineMessage.minunit+"/"+message.MedicineMessage.maxunit+ "      "+$"�ܼ�:{message.addnumber}{message.MedicineMessage.minunit}"+ "\n";
            TempPrescription.text += "�÷�:" + message.usage + "        " + "���η���:" + message.eatdose * message.MedicineMessage.dosage + message.MedicineMessage.doseunit;
            TempPrescription.text += "\t"+$"ÿ��{message.Times}��\t" + message.Day + "��\n" ;


        }



    }

    public void ReturnPrescription()
    {
        TMP_InputField Field;
        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.Name = Field.text;

        Field = BackGround.transform.Find("�Ա�").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.Sex= Field.text;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.age = Field.text;

        Field = BackGround.transform.Find("��ַ").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.Location = Field.text;

        Field = BackGround.transform.Find("�绰").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.number = Field.text;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.Date = Field.text;

        Field = BackGround.transform.Find("���").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Patient.Description = Field.text;

        Field = BackGround.transform.Find("ҽʦ").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Doctor = Field.text;

        Field = BackGround.transform.Find("���").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.Check = Field.text;

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.MedicineCost = float.Parse(Field.text);

        Field = BackGround.transform.Find("�Һŷ�").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.SignCost = float.Parse(Field.text);

        Field = BackGround.transform.Find("ע���").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.InjectionCost = float.Parse(Field.text);

        Field = BackGround.transform.Find("���Ʒ�").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.TreatmentCost = float.Parse(Field.text);

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.OtherCost = float.Parse(Field.text);

        Field = BackGround.transform.Find("�ϼ�").GetChild(0).GetComponent<TMP_InputField>();
        PrescriptionMessage.SumCost = float.Parse(Field.text);


        Field = BackGround.transform.Find("����").GetChild(1).GetComponent<TMP_InputField>();
        if (Field.text != "")
        {
            PrescriptionMessage.Patient.T = float.Parse(Field.text);
        }
        else
        {
            PrescriptionMessage.Patient.T = 0;
        }


        Field = BackGround.transform.Find("����").GetChild(1).GetComponent<TMP_InputField>();
        if (Field.text != "")
        {
            PrescriptionMessage.Patient.P = float.Parse(Field.text);
        }
        else
        {
            PrescriptionMessage.Patient.P = 0;
        }


        Field = BackGround.transform.Find("Ѫѹ").GetChild(1).GetComponent<TMP_InputField>();
        if (Field.text != "")
        {
            PrescriptionMessage.Patient.BP_b = float.Parse(Field.text);
        }
        else
        {
            PrescriptionMessage.Patient.BP_b = 0;
        }


        Field = BackGround.transform.Find("Ѫѹ").GetChild(2).GetComponent<TMP_InputField>();
        if (Field.text != "")
        {
            PrescriptionMessage.Patient.BP_p = float.Parse(Field.text);
        }
        else
        {
            PrescriptionMessage.Patient.BP_p = 0;
        }

        Field = BackGround.transform.Find("Ѫ��").GetChild(1).GetComponent<TMP_InputField>();
        if (Field.text != "")
        {
            PrescriptionMessage.Patient.Blood_Sugar = float.Parse(Field.text);
        }
        else
        {
            PrescriptionMessage.Patient.Blood_Sugar = 0;
        }


        SaveToManager();
        PrescriptManager.instance.SavePrescriptions();//����PrescriptionList
        MedicineSerializable.instance.SaveData();
    }


    public void ChangeCost()
    {
        TMP_InputField Field;
        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        if(float.TryParse(Field.text,out float temp))
        {
            PrescriptionMessage.MedicineCost = float.Parse(Field.text);

        }
        else
        {
            PrescriptionMessage.MedicineCost = 0;
            Field.text = "0";
        }

        Field = BackGround.transform.Find("�Һŷ�").GetChild(0).GetComponent<TMP_InputField>();
        if (float.TryParse(Field.text,out float a))
        {
            PrescriptionMessage.SignCost = a;
        }
        else
        {
            PrescriptionMessage.SignCost = 0;
            Field.text = "0";
        }

        Field = BackGround.transform.Find("ע���").GetChild(0).GetComponent<TMP_InputField>();
        if (float.TryParse(Field.text, out float b))
        {
            PrescriptionMessage.InjectionCost = b;
        }
        else
        {
            PrescriptionMessage.InjectionCost = 0;
            Field.text = "0";
        }


        Field = BackGround.transform.Find("���Ʒ�").GetChild(0).GetComponent<TMP_InputField>();
        if (float.TryParse(Field.text, out float c))
        {
            PrescriptionMessage.TreatmentCost = c;
        }
        else
        {
            PrescriptionMessage.TreatmentCost = 0;
            Field.text = "0";
        }

        Field = BackGround.transform.Find("����").GetChild(0).GetComponent<TMP_InputField>();
        if (float.TryParse(Field.text, out float d))
        {
            PrescriptionMessage.OtherCost = d;
        }
        else
        {
            PrescriptionMessage.OtherCost = 0;
            Field.text = "0";
        }
        PrescriptionMessage.SumCost= PrescriptionMessage.MedicineCost+ PrescriptionMessage.SignCost+ PrescriptionMessage.InjectionCost
            +PrescriptionMessage.TreatmentCost+PrescriptionMessage.OtherCost;
        PrescriptionMessage.Profit = PrescriptionMessage.SumCost - PrescriptionMessage.OriginalCost;

        Field = BackGround.transform.Find("�ϼ�").GetChild(0).GetComponent<TMP_InputField>();
        Field.text = PrescriptionMessage.SumCost.ToString();
        ReturnPrescription();
        Setprescription();
        PrescriptManager.instance.SavePrescriptions();
    }

    public void SaveToManager()
    {
        PrescriptManager.instance.AddList(PrescriptionMessage);
        if (!PrescriptionMessage.isPaid && PrescriptionMessage.MedicineLsit.Count!=0)
        {
            TableManager.Instance.SubSellList(PrescriptionMessage.MedicineLsit);
            PrescriptionMessage.isPaid = true;

        }

        Example08.instance.GenerateCells(PrescriptManager.instance.PrescriptList, PrescriptManager.instance.PrescriptList.Count);

    }

    public void InitializedPrescription()
    {
        for(int i = 0; i < 7; i++)
        {
            TMP_InputField Field =BackGround.transform.GetChild(i).GetChild(0).GetComponent<TMP_InputField>();
            Field.text = "";
        }
        TextMeshProUGUI TempPrescription = BackGround.transform.Find("Rx")?.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        TempPrescription.text = "";
        TempPrescription = null;


    }




    public void EditMessage()
    {
        //UIManager.Instance.OnBackButtonClicked();
        UIManager.Instance.ShowPanel(1);
        TableManager.Instance.selledTable = new List<MedicineData>(PrescriptionMessage.MedicineLsit);
        if (PrescriptionMessage.isPaid)
        {
            TableManager.Instance.returnMedicineList(PrescriptionMessage.MedicineLsit);//��������ҩ��Ż�BankList
            PrescriptionMessage.MedicineLsit.Clear();
            PrescriptionMessage.isPaid = false;
        }
        ReturnPrescription();

        TableManager.Instance.Dropdown.value = 1;//�л���SellList
        TableManager.Instance.ChnageList();

    }



    public void RenturnCanvas()
    {

        UIManager.Instance.OnBackButtonClicked();
    }

    public void SetPrescriptMessage(PrescriptionMessage Message)
    {
        PrescriptionMessage = new PrescriptionMessage(Message);
    }



}
