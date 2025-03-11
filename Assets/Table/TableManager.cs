using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI.Table;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    public static TableManager Instance;
    public TMP_InputField SearchBox;
    public List<MedicineData> currentTable;//
    public List<MedicineData> NameTable;//ҩƷ��Ϣ
    public List<MedicineData> SsarchTable;//������
    public List<MedicineData> bankTable;//���
    public List<MedicineData> selledTable=new List<MedicineData>();//���۱�
    public GameObject TableRow;
    public TMP_Dropdown Dropdown;//����������ʾ��ͬ��
    public int Maxindex;//ҳ��
    public int currentIndex;//����ҳ��
    public int maxRow;//ÿҳ�����
    public List<GameObject> AddButton;



    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        Invoke("SetTable", 0.5f);
        AddButton[4].GetComponent<Button>().onClick.AddListener(() =>
            SetAddbutton(2, 3)
        );
    }
    [ContextMenu("choiceTable")]
    public void choiceTable(List<MedicineData> list)
    {
        if (list == null)
        {
            currentTable = MedicineList.instance.MedicList;
        }
        currentTable = list;
        Maxindex = currentTable.Count / maxRow;
        currentIndex = 0;
    }

    [ContextMenu("display")]

    public void displayTable()//������������ʾ
    {
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
        //Debug.Log(this.transform.GetChild(0).childCount);
        //currentTable = MedicineList.instance.MedicList;
        for (int i = maxRow*currentIndex; i < (currentIndex+1)*maxRow; i++)
        {
            if (i==currentTable.Count)
            {
                //Debug.Log(i);
                break;
            }
            var newRow = Instantiate(TableRow, this.transform.GetChild(0));//������
            initializeRow(newRow, i);//��ʼ��
            newRow.GetComponent<Row>().InitializeItem();
        }
        //for�ҵ���������,RowNUmber++,֪������MaxROW,


    }
 
    public void addIndex()
    {
        if (currentIndex <= Maxindex)
        {
            currentIndex++;
            displayTable();
        }
    }


    public void subIndex()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            displayTable();
        }
    }
    public void initializeRow(GameObject row,int t)
    {
        //Debug.Log(i);
        ///GameObject row = this.transform.GetChild(0).GetChild(i).gameObject;
        if (row != null)
        {


            row.GetComponent<Row>().medicineData = currentTable[t];
            //row.GetComponent<MedicineData>().MedicineMessage= currentTable[t].MedicineMessage;
            //row.GetComponent<MedicineData>().unit = currentTable[t].unit;
            //row.GetComponent<MedicineData>().number= currentTable[t].number;
            //row.GetComponent<MedicineData>().addnumber= currentTable[t].addnumber;
            //row.GetComponent<MedicineData>().granuleNumber= currentTable[t].granuleNumber;
            //row.GetComponent<MedicineData>().BoxNumber= currentTable[t].BoxNumber;
            //row.GetComponent<MedicineData>().originalCost= currentTable[t].originalCost;
            //row.GetComponent<MedicineData>().sellPrice= currentTable[t].sellPrice;
            //row.GetComponent<MedicineData>().ProductionDate= currentTable[t].ProductionDate;
            //row.GetComponent<MedicineData>().validity= currentTable[t].validity;
            //row.GetComponent<MedicineData>().ListID = t;
        }

    }

    [ContextMenu("SaveMedicine")]
    public void SaveMedicine()//����ҳ������
    {
        currentTable = MedicineSerializable.instance.MedicList;
        for (int i=0;i<transform.GetChild(0).childCount;i++)
        {
            this.transform.GetChild(0).GetChild(i).GetComponent<Row>().returnItem();
            currentTable[i] = this.transform.GetChild(0).GetChild(i).GetComponent<MedicineData>();
            //����ID����
        }
    }
    [ContextMenu("InitializeTable")]
    public void SetTable()
    {
        bankTable = MedicineSerializable.instance.MedicList;//Load JsonData
        NameTable = MedicineSerializable.instance.NameList;
        currentTable = bankTable;
        ChnageList();//��ʼ���б����
    }

    [ContextMenu("SetMedicineID")]
    public void SetMedicineID()
    {
        if (bankTable != null)
        {
            for(int i=0;i<bankTable.Count;i++)
            {
                bankTable[i].ListID = i;

            }
        }
    }
    [ContextMenu("ToBankTable")]
    public void ToBankTable()
    {
        currentTable= bankTable;
    }

    [ContextMenu("TOsellTable")]
    public void TOsellTable()
    {
        currentTable = selledTable;
    }
    [ContextMenu("ToNameTable")]
    public void ToNameTable()
    {
        currentTable = NameTable;
    }

    public void ChnageList()
    {
        if (Dropdown!=null)
        {
            switch (Dropdown.value){
                case 0:
                    choiceTable(bankTable);
                    HideAllButtons();
                    AddButton[0].SetActive(true);
                    WindowControl.instance.SellWindow(false);
                    //currentTable = bankTable;
                    break;
                case 1:
                    choiceTable(selledTable);
                    HideAllButtons();
                    AddButton[1].SetActive(true);
                    WindowControl.instance.SellWindow(true);
                    break;
                 case 2:
                    choiceTable(NameTable);
                    HideAllButtons();
                    WindowControl.instance.SellWindow(false);
                    AddButton[2].SetActive(true);
                    AddButton[3].SetActive(true);
                    AddButton[4].SetActive(true);
                    break;
            }
            displayTable();
        }
    }

    public void SetAddbutton(int close,int open)
    {
        if (Dropdown.value == 2)
        {
            AddButton[close].SetActive(false);
            AddButton[open].SetActive(true);
        }
    }


    public void HideAllButtons()
    {
        foreach (GameObject button in AddButton)
        {
            if (button != null)
            {
                button.SetActive(false);
            }
        }
    }


    public void Search()
    {
        List<MedicineData>TempList= new List<MedicineData>();
        switch (Dropdown.value)
        {
            case 0:
                TempList = bankTable;
                break;
            case 1:
                TempList = selledTable;
                break;
            case 2:
                TempList= NameTable;
                break;
        }

        SsarchTable = TempList.Where(medicine => medicine.MedicineMessage.Name.Contains(SearchBox.text.ToString())).ToList();
        choiceTable(SsarchTable);
        displayTable();
        TempList = null;
    }

    public void AddSellList(MedicineData medicineData)
    {
        int Index = selledTable.FindIndex(medicine => medicine.ListID == medicineData.ListID);
 
        if (Index>=0)
        {
            selledTable[Index].addnumber += medicineData.addnumber;
        }
        else
        {
            MedicineData Temp = new MedicineData(medicineData);
            selledTable.Add(Temp);
        }

    }

    public void SubSellList(List<MedicineData> PrescriptionList)
    {
        foreach(MedicineData Temp in PrescriptionList)
        {
            int Index = bankTable.FindIndex(medicine => medicine.MedicineMessage.Name == Temp.MedicineMessage.Name &&
            medicine.MedicineMessage.source == Temp.MedicineMessage.source &&
            medicine.ProductionDate == Temp.ProductionDate &&
            medicine.validity == Temp.validity
            );
            if (Index >= 0)
            {
                if (Temp.unit == BaseEnum.granule || Temp.unit == BaseEnum.box)
                {
                    bankTable[Index].granuleNumber -= Temp.addnumber;
                    bankTable[Index].BoxNumber -= Temp.addnumber/Temp.MedicineMessage.capacity;
                }
                else
                {
                    bankTable[Index].BoxNumber -= Temp.addnumber;
                    bankTable[Index].granuleNumber -= Temp.addnumber *Temp.MedicineMessage.capacity;
                }                

            }

        }
        selledTable.Clear();
        
    }

    public void RemoveMedicine(MedicineData Data)
    {
        int index = currentTable.FindIndex(temp => temp.MedicineMessage == Data.MedicineMessage
        && temp.ProductionDate==Data.ProductionDate && temp.validity==Data.validity);//������Ϣ
        if (index >= 0)
        {
            currentTable.RemoveAt(index);
        }


        SetListID();
        displayTable();
    }

    public void SendSellList()
    {
        //UIManager.Instance.OnBackButtonClicked();
        if (prescription.instance.PrescriptionMessage.Patient.Name == "")
        {
            //������������������Ϊ��
            return;
        }
        else
        {
            UIManager.Instance.ShowPanel(3);
            prescription.instance.PrescriptionMessage.MedicineLsit = selledTable.ToList();
            prescription.instance.PrescriptionMessage.MedicineCost = 0;
            prescription.instance.PrescriptionMessage.OriginalCost = 0;
            foreach (MedicineData data in prescription.instance.PrescriptionMessage.MedicineLsit)
            {
                if (data.unit == BaseEnum.granule || data.unit == BaseEnum.bag)
                {
                    prescription.instance.PrescriptionMessage.MedicineCost += data.addnumber * (data.sellPrice/data.MedicineMessage.capacity);
                    prescription.instance.PrescriptionMessage.OriginalCost += data.addnumber * (data.originalCost / data.MedicineMessage.capacity);
                    
                }
                else
                {
                    prescription.instance.PrescriptionMessage.MedicineCost += data.addnumber * data.sellPrice;
                    prescription.instance.PrescriptionMessage.OriginalCost += data.addnumber * data.originalCost;
                }

            }
            prescription.instance.PrescriptionMessage.SumCost = prescription.instance.PrescriptionMessage.MedicineCost + prescription.instance.PrescriptionMessage.InjectionCost
                + prescription.instance.PrescriptionMessage.SignCost + prescription.instance.PrescriptionMessage.OtherCost + prescription.instance.PrescriptionMessage.TreatmentCost;
            prescription.instance.PrescriptionMessage.Profit = prescription.instance.PrescriptionMessage.SumCost - prescription.instance.PrescriptionMessage.OriginalCost;
            prescription.instance.Setprescription();
        }

    }

    public void AddBankList(MedicineData Data)
    {
        int Index = bankTable.FindIndex(medicine => medicine.MedicineMessage.Name == Data.MedicineMessage.Name &&
            medicine.MedicineMessage.source == Data.MedicineMessage.source &&
            medicine.ProductionDate == Data.ProductionDate &&
            medicine.validity == Data.validity

            );

        if (Index >= 0)
        {
            if (Data.unit == BaseEnum.granule || Data.unit == BaseEnum.bag)
            {
                bankTable[Index].granuleNumber += Data.addnumber;
                bankTable[Index].BoxNumber += Data.addnumber / Data.MedicineMessage.capacity;
            }
            else
            {
                bankTable[Index].BoxNumber += Data.addnumber;
                bankTable[Index].granuleNumber += Data.addnumber * Data.MedicineMessage.capacity;
            }

        }
        else
        {
            MedicineData Temp = new MedicineData(Data);
            if (Temp.unit == BaseEnum.granule || Temp.unit == BaseEnum.bag)
            {
                Temp.granuleNumber = Temp.addnumber;
                Temp.BoxNumber = Temp.addnumber / Temp.MedicineMessage.capacity;
            }
            else
            {
                Temp.BoxNumber = Temp.addnumber;
                Temp.granuleNumber = Temp.addnumber * Temp.MedicineMessage.capacity;
            }
            bankTable.Add(Temp);
        }
        SetListID();
    }
    public void returnMedicineList(List<MedicineData> MedicineList)//��������ҩ��Ż�BankList
    {
        foreach (MedicineData Temp in MedicineList)
        {
            TableManager.Instance.AddBankList(Temp);
        }
    }

    public void AddNameList(MedicineData Data)
    {
        NameTable.Add(Data);
        MedicineSerializable.instance.NameList = NameTable;
        MedicineSerializable.instance.SaveNameList();
    }

    public void DisplaySellList()
    {
        for(int i = 0; i < selledTable.Count; i++)
        {
            Debug.Log(selledTable[i].number);
        }
    }

    public void InitializeNameTable()
    {
        NameTable = new List<MedicineData>();

    }

    public void EditList(MedicineData data)
    {
        for(int i=0;i<currentTable.Count;i++)
        {
            if (currentTable[i].ListID == data.ListID)
            {
                //Debug.Log(currentTable[i].number);
                currentTable[i] = new MedicineData(data);               
                break;
            }
        }
        MedicineSerializable.instance.SaveData();
        MedicineSerializable.instance.SaveNameList();

    }

    public void SetListID()
    {
        for(int i = 0; i < bankTable.Count;i++)
        {
            bankTable[i].ListID= i;
        }
    }
    public void ReturnCanvas()
    {
        UIManager.Instance.OnBackButtonClicked();
    }



}
