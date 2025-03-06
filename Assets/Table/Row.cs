using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Row : MonoBehaviour
{
    // Start is called before the first frame update
    public RowEnum RowEnum;
    public  MedicineData medicineData=new MedicineData();

    private void Start()
    {
        this.transform.Find("name")?.GetComponent<Button>()?.onClick.AddListener(() =>
            TableManager.Instance.SetAddbutton(3, 2)
        );
    }
    private void OnDestroy()
    {
        this.transform.Find("name")?.GetComponent<Button>()?.onClick.RemoveAllListeners();
    }

    public void InitializeItem()
    {
        //medicineData = this.GetComponent<MedicineData>();
        if (medicineData != null)
        {
            changeUnit();
            changeNumber();

            if (RowEnum == RowEnum.display)
            {
                TextMeshProUGUI field;
                field = this.transform.Find("name")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.MedicineMessage.Name);

                field = this.transform.Find("���")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.MedicineMessage.capacity.ToString());



                field = this.transform.Find("����")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.MedicineMessage.source);

                field = this.transform.Find("����")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.MedicineMessage.signIndex);

                // �� long ���� Unix ʱ���ת��Ϊ DateTime ����ʽ��Ϊ yyyy-MM-dd
                field = this.transform.Find("��������")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                if (field != null)
                {
                    DateTime productionDate = DateTimeOffset.FromUnixTimeMilliseconds(medicineData.ProductionDate).DateTime;
                    field.SetText(productionDate.ToString("yyyy-MM-dd"));
                }

                field = this.transform.Find("��Ч��")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                if (field != null)
                {
                    DateTime validityDat = DateTimeOffset.FromUnixTimeMilliseconds(medicineData.validity).DateTime;
                    field.SetText(validityDat.ToString("yyyy-MM-dd"));
                    Debug.Log($"ProductionDate (long): {medicineData.ProductionDate}");
                    Debug.Log($"Validity (long): {medicineData.validity}");
                }

                field = this.transform.Find("ԭ��")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.originalCost.ToString());

                field = this.transform.Find("�ۼ�")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.sellPrice.ToString());

                field = this.transform.Find("���")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(medicineData.number.ToString());
                //Debug.Log(medicineData.number);
                //this.transform.GetChild(9).GetChild(0).GetComponent<TMP_InputField>().text = medicineData.number.ToString();

                DateTime validityDate = DateTimeOffset.FromUnixTimeMilliseconds(medicineData.validity).DateTime;
                // ���������ڵ�����
                TimeSpan difference = (validityDate - DateTime.Now);
                field = this.transform.Find("�������")?.GetChild(0)?.GetComponent<TextMeshProUGUI>();
                field?.SetText(Mathf.Abs(difference.Days).ToString());

                //����DropDown��ֵ
                SetUnitDrop();

            }else if (RowEnum == RowEnum.edit)
            {
                TMP_InputField field;
                field = this.transform.Find("name")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.MedicineMessage.Name;


                field = this.transform.Find("���")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.MedicineMessage.capacity.ToString();
                
                field=this.transform.Find("���")?.GetChild(1)?.GetComponent<TMP_InputField>();
                if(field) field.text=medicineData.MedicineMessage.dosage.ToString();

                TMP_Dropdown TempDrop = this.transform.Find("���")?.GetChild(2).GetComponent<TMP_Dropdown>();
                switch (medicineData.MedicineMessage.doseunit.ToString())
                {
                    case "mg":
                        TempDrop.value = 0;
                        break;
                    case "g":
                        TempDrop.value = 1;
                        break;
                    case "kg":
                        TempDrop.value = 2;                       
                        break;
                    case "ml":
                        TempDrop.value = 3;
                        break;
                }
                TempDrop.RefreshShownValue();

                TempDrop= this.transform.Find("���")?.GetChild(3).GetComponent<TMP_Dropdown>();             
                switch (medicineData.MedicineMessage.minunit.ToString())
                {
                    case "Ƭ":
                        TempDrop.value = 0;
                        
                        break;
                    case "��":
                        TempDrop.value = 1;
                        
                        break;
                    case "֧":
                        TempDrop.value = 2;
                        
                        break;
                    case "ƿ":
                        TempDrop.value = 3;
                        
                        break;
                }

                TempDrop.RefreshShownValue();

                TempDrop = this.transform.Find("���")?.GetChild(4).GetComponent<TMP_Dropdown>();
                switch (medicineData.MedicineMessage.maxunit.ToString())
                {
                    case "��":
                        TempDrop.value = 0;
                        break;
                    case "ƿ":
                        TempDrop.value = 1;
                        break;
                }
                TempDrop.RefreshShownValue();

                SetUnitDrop();
                field = this.transform.Find("��������")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field != null)
                {
                    DateTime productionDate = DateTimeOffset.FromUnixTimeMilliseconds(medicineData.ProductionDate).DateTime.ToLocalTime();
                    field.text = productionDate.ToString("yyyy-MM-dd");
                    Debug.Log($"��������: {productionDate:yyyy-MM-dd}");
                }

                field = this.transform.Find("��Ч��")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field != null)
                {
                    DateTime validityDat = DateTimeOffset.FromUnixTimeMilliseconds(medicineData.validity).DateTime.ToLocalTime();
                    field.text = validityDat.ToString("yyyy-MM-dd");
                    Debug.Log($"��Ч��: {validityDat:yyyy-MM-dd}");
                }


                field = this.transform.Find("�÷�")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.usage;




                field = this.transform.Find("����")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.MedicineMessage.source;
                

                field = this.transform.Find("����")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.MedicineMessage.signIndex;
                
               
                field = this.transform.Find("ԭ��")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.originalCost.ToString();
                
                field = this.transform.Find("�ۼ�")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.sellPrice.ToString();
                

                field = this.transform.Find("���")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.number.ToString();

                //this.transform.GetChild(9).GetChild(0).GetComponent<TMP_InputField>().text = medicineData.number.ToString();

                // �� long ���͵� validity ת���� DateTime
                DateTime validityDate = DateTimeOffset.FromUnixTimeMilliseconds(medicineData.validity).DateTime;
                // ���������ڵ�����
                TimeSpan difference = validityDate - DateTime.Now;
                field = this.transform.Find("�������")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = Mathf.Abs(difference.Days).ToString();

                field = this.transform.Find("�������")?.GetChild(0)?.GetComponent<TMP_InputField>();
                if (field) field.text = medicineData.addnumber.ToString();

                TempDrop = this.transform.Find("���ô���")?.GetChild(1).GetComponent<TMP_Dropdown>();
                switch (medicineData.Times.ToString())
                {
                    case "һ":
                        TempDrop.value = 0;
                        break;
                    case "��":
                        TempDrop.value = 1;
                        break;
                    case "��":
                        TempDrop.value = 2;
                        break;
                }
                TempDrop.RefreshShownValue();

            }
            TMP_Dropdown dropdown = this.transform.Find("��λ")?.GetChild(0).GetComponent<TMP_Dropdown>();
            switch (medicineData.unit.ToString())
            {
                case "granule"://Ӣ��
                    dropdown.value = 0;
                    dropdown.RefreshShownValue();
                    
                    //medicineData.BoxNumber = medicineData.granuleNumber / int.Parse(medicineData.MedicineMessage.capacity.ToString());
                    
                    break;
                case "box":
                    dropdown.value = 1;
                    dropdown.RefreshShownValue();
                    
                    //medicineData.granuleNumber = medicineData.BoxNumber * int.Parse(medicineData.MedicineMessage.capacity.ToString());
                    break;
                case "bag":
                    dropdown.value = 2;
                    dropdown.RefreshShownValue();
                    
                    break;
            }             

        }
    }

     public void returnItem()
    {
        //MedicineData medicineData = this.GetComponent<MedicineData>();
        if (medicineData != null)
        {
            if(this.transform.Find("name"))
            medicineData.MedicineMessage.Name = this.transform.Find("name")?.GetChild(0).GetComponent<TMP_InputField>().text;
            if(this.transform.Find("���"))
            medicineData.MedicineMessage.capacity = int.Parse(this.transform.Find("���")?.GetChild(0).GetComponent<TMP_InputField>().text);
            if (this.transform.Find("���"))
                medicineData.MedicineMessage.dosage = float.Parse(this.transform.Find("���")?.GetChild(1).GetComponent<TMP_InputField>().text);
           
            if (this.transform.Find("���"))
            {
                TMP_Dropdown dropdown = this.transform.Find("���")?.GetChild(2).GetComponent<TMP_Dropdown>();
                switch (dropdown.value)
                {
                    case 0:
                        medicineData.MedicineMessage.doseunit = doseunit.mg;
                        //medicineData.number=medicineData.granuleNumber;

                        break;
                    case 1:
                        medicineData.MedicineMessage.doseunit = doseunit.g;
                        //medicineData.number=medicineData.BoxNumber;

                        break;
                    case 2:
                        medicineData.MedicineMessage.doseunit = doseunit.kg;
                        break;
                    case 3:
                        medicineData.MedicineMessage.doseunit = doseunit.ml;
                        break;

                }
            }

            if (this.transform.Find("���"))
            {
                TMP_Dropdown dropdown = this.transform.Find("���")?.GetChild(3).GetComponent<TMP_Dropdown>();
                switch (dropdown.value)
                {
                    case 0:
                        medicineData.MedicineMessage.minunit = BaseEnum.Ƭ;
                        //medicineData.number=medicineData.granuleNumber;

                        break;
                    case 1:
                        medicineData.MedicineMessage.minunit = BaseEnum.��;
                        //medicineData.number=medicineData.BoxNumber;
                        break;
                   case 2:
                        medicineData.MedicineMessage.minunit = BaseEnum.֧;
                        break;
                    case 3:
                        medicineData.MedicineMessage.minunit = BaseEnum.ƿ;
                        break;

                }
            }
            if (this.transform.Find("���"))
            {
                TMP_Dropdown dropdown = this.transform.Find("���")?.GetChild(4).GetComponent<TMP_Dropdown>();
                switch (dropdown.value)
                {
                    case 0:
                        medicineData.MedicineMessage.maxunit = BaseEnum.��;
                        //medicineData.number=medicineData.granuleNumber;

                        break;
                    case 1:
                        medicineData.MedicineMessage.maxunit = BaseEnum.ƿ;
                        //medicineData.number=medicineData.BoxNumber;
                        break;

                }
            }

            float tempEatDose;
            if (this.transform.Find("���μ���") &&
                float.TryParse(this.transform.Find("���μ���")?.GetChild(0).GetComponent<TMP_InputField>().text, out tempEatDose))
            {
                medicineData.eatdose = tempEatDose;
            }

            int tempDay;
            if (this.transform.Find("��������") &&
                int.TryParse(this.transform.Find("��������")?.GetChild(0).GetComponent<TMP_InputField>().text, out tempDay))
            {
                medicineData.Day = tempDay;
            }


            if (this.transform.Find("�÷�"))
                medicineData.usage = this.transform.Find("�÷�")?.GetChild(0).GetComponent<TMP_InputField>().text;

            if (this.transform.Find("����"))
                medicineData.MedicineMessage.source = this.transform.Find("����")?.GetChild(0).GetComponent<TMP_InputField>().text;
            if (this.transform.Find("����"))
                medicineData.MedicineMessage.signIndex = this.transform.Find("����")?.GetChild(0).GetComponent<TMP_InputField>().text;

            Transform productionDateTransform = this.transform.Find("��������");

            if (productionDateTransform != null)
            {
                TMP_InputField inputField = productionDateTransform.GetChild(0)?.GetComponent<TMP_InputField>();

                if (inputField != null)
                {
                    string inputDate = inputField.text.Trim(); // ��ȡ�ı���ȥ���ո�

                    if (!string.IsNullOrEmpty(inputDate) &&
                        DateTime.TryParseExact(inputDate, "yyyy-MM-dd",
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out DateTime parsedDate))
                    {
                        // ת�� DateTime Ϊ Unix ʱ��������뼶��
                        medicineData.ProductionDate = new DateTimeOffset(parsedDate).ToUnixTimeMilliseconds();
                    }
                    else
                    {
                        Debug.LogWarning($"������������ڸ�ʽ��Ч: \"{inputDate}\"��ʹ��Ĭ��ֵ��");
                        medicineData.ProductionDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    }
                }
                else
                {
                    Debug.LogWarning("δ�ҵ� TMP_InputField �����");
                }
            }


            Transform validityTransform = this.transform.Find("��Ч��");

            if (validityTransform != null)
            {
                TMP_InputField inputField = validityTransform.GetChild(0)?.GetComponent<TMP_InputField>();

                if (inputField != null)
                {
                    string inputDate = inputField.text.Trim(); // ��ȡ�ı���ȥ���ո�

                    if (!string.IsNullOrEmpty(inputDate) &&
                        DateTime.TryParseExact(inputDate, "yyyy-MM-dd",
                                               System.Globalization.CultureInfo.InvariantCulture,
                                               System.Globalization.DateTimeStyles.None,
                                               out DateTime parsedDate))
                    {
                        // ת�� DateTime Ϊ Unix ʱ��������뼶��
                        medicineData.validity = new DateTimeOffset(parsedDate).ToUnixTimeMilliseconds();
                    }
                    else
                    {
                        Debug.LogWarning($"�������Ч�ڸ�ʽ��Ч: \"{inputDate}\"��ʹ��Ĭ��ֵ��");
                        medicineData.validity = DateTimeOffset.MaxValue.ToUnixTimeMilliseconds();
                    }
                }
                else
                {
                    Debug.LogWarning("δ�ҵ� TMP_InputField �����");
                }
            }


            if (this.transform.Find("ԭ��"))
            {
                TMP_InputField inputField = this.transform.Find("ԭ��")?.GetChild(0).GetComponent<TMP_InputField>();
                string inputText = inputField?.text.Trim(); // ��ȡ�ı���ȥ���ո�
                if (!string.IsNullOrEmpty(inputText) && float.TryParse(inputText, out float parsedValue))
                {
                    medicineData.originalCost = parsedValue;
                }
                else
                {
                    Debug.LogWarning($"�����ԭ�۸�ʽ��Ч: \"{inputText}\"��ʹ��Ĭ��ֵ��");
                    medicineData.originalCost = 0f; // ����ʹ������Ĭ��ֵ
                }
            }
            if (this.transform.Find("�ۼ�"))
            {
                TMP_InputField inputField = this.transform.Find("�ۼ�")?.GetChild(0).GetComponent<TMP_InputField>();
                string inputText = inputField?.text.Trim(); // ��ȡ�ı���ȥ���ո�
                if (!string.IsNullOrEmpty(inputText) && float.TryParse(inputText, out float parsedValue))
                {
                    medicineData.sellPrice = parsedValue;
                }
                else
                {
                    Debug.LogWarning($"�����ԭ�۸�ʽ��Ч: \"{inputText}\"��ʹ��Ĭ��ֵ��");
                    medicineData.sellPrice = 0f; // ����ʹ������Ĭ��ֵ
                }
            }
            if (this.transform.Find("��λ")) {
                TMP_Dropdown dropdown = this.transform.Find("��λ")?.GetChild(0).GetComponent<TMP_Dropdown>();
                switch (dropdown.value)
                {
                    case 0:
                        medicineData.unit = BaseEnum.granule;
                        //medicineData.number=medicineData.granuleNumber;

                        break;
                    case 1:
                        medicineData.unit = BaseEnum.box;
                        //medicineData.number=medicineData.BoxNumber;

                        break;
                    case 2:
                        medicineData.unit = BaseEnum.granule;
                        break;
                }
            }
            if (this.transform.Find("���")) {
                if (int.TryParse(this.transform.Find("���")?.GetChild(0).GetComponent<TMP_InputField>().text.ToString(), out int number))
                {
                    Debug.Log(number);
                    medicineData.number = number;
                }
            }
            if (this.transform.Find("�������"))
            {
                if (int.TryParse(this.transform.Find("�������")?.GetChild(0).GetComponent<TMP_InputField>().text.ToString(), out int Addnumber))
                {
                    if (Addnumber > 0)
                    {
                        medicineData.addnumber = Addnumber;
                    }

                }
            }
            if (this.transform.Find("�������")) {
                if (int.TryParse(this.transform.Find("�������")?.GetChild(0).GetComponent<TMP_InputField>().text.ToString(), out int day))
                {
                    medicineData.RemainTime = day;
                }
            }

            if (this.transform.Find("���ô���"))
            {
                TMP_Dropdown dropdown = this.transform.Find("���ô���")?.GetChild(1).GetComponent<TMP_Dropdown>();
                switch (dropdown.value)
                {
                    case 0:
                        medicineData.Times = Times.һ;
                        break;
                    case 1:
                        medicineData.Times = Times.��;
                        break;
                    case 2:
                        medicineData.Times = Times.��;
                        break;
                }
            }

        }
    }

    //public void SetDropdown()
    //{


    //    TMP_Dropdown dropdown = this.transform.Find("���")?.GetChild(3).GetComponent<TMP_Dropdown>();
    //    var Temp = this.transform.Find("���")?.GetChild(4);
    //    switch (dropdown.value)
    //    {
    //        case 0:
    //            medicineData.MedicineMessage.minunit = BaseEnum.Ƭ;
    //            //medicineData.number=medicineData.granuleNumber;
    //            Temp.gameObject.SetActive(true);
    //            break;
    //        case 1:
    //            medicineData.MedicineMessage.minunit = BaseEnum.��;
    //            //medicineData.number=medicineData.BoxNumber;
    //            Temp.gameObject.SetActive(true);
    //            break;
    //        case 2:
    //            medicineData.MedicineMessage.minunit = BaseEnum.֧;
    //            Temp.gameObject.SetActive(true);
    //            break;
    //        case 3:
    //            medicineData.MedicineMessage.minunit = BaseEnum.ƿ;
    //            Temp.gameObject.SetActive(false);
    //            break;

    //    }

    //}

    public void changeNumber()
    {
        //MedicineData medicineData = this.GetComponent<MedicineData>();
        switch (medicineData.unit)
        {
            case BaseEnum.granule:
                medicineData.granuleNumber = medicineData.number;
               // Debug.Log(medicineData.granuleNumber);
                medicineData.BoxNumber = medicineData.number / int.Parse(medicineData.MedicineMessage.capacity.ToString());
                break;
            case BaseEnum.box:
                medicineData.BoxNumber = medicineData.number;
                medicineData.granuleNumber = medicineData.BoxNumber * int.Parse(medicineData.MedicineMessage.capacity.ToString());
                break;
            case BaseEnum.bag:
                medicineData.granuleNumber = medicineData.number;
                medicineData.BoxNumber = medicineData.number / int.Parse(medicineData.MedicineMessage.capacity.ToString());
                break;
        }
    }


    public void SetUnitDrop()
    {
        TMP_Dropdown UnitDrop = this.transform.Find("��λ")?.GetChild(0).GetComponent<TMP_Dropdown>();
        switch (medicineData.MedicineMessage.minunit)
        {
            case BaseEnum.֧:
                UnitDrop.options[0].text = "֧";
                break;
            case BaseEnum.Ƭ:
                UnitDrop.options[0].text = "Ƭ";
                break;
            case BaseEnum.ƿ:
                UnitDrop.options[0].text = "ƿ";
                break;
            case BaseEnum.��:
                UnitDrop.options[0].text = "��";
                break;
        }
        switch (medicineData.MedicineMessage.maxunit)
        {
            case BaseEnum.ƿ:
                UnitDrop.options[1].text = "ƿ";
                break;
            case BaseEnum.��:
                UnitDrop.options[1].text = "��";
                break;
        }
        UnitDrop.RefreshShownValue();
    }

    public void changeUnit()
    {
        TMP_Dropdown dropdown = this.transform.Find("��λ")?.GetChild(0).GetComponent<TMP_Dropdown>();
        //MedicineData medicineData = this.GetComponent<MedicineData>();
        switch (dropdown.value)
        {
            case 0:
                
                medicineData.number=medicineData.granuleNumber;
                medicineData.unit=BaseEnum.granule;
                break;
            case 1:            
                medicineData.number=medicineData.BoxNumber;
                medicineData.unit= BaseEnum.box;               
                break;

        }
        if (RowEnum == RowEnum.display) {
            this.transform.Find("���").GetChild(0).GetComponent<TextMeshProUGUI>().text = medicineData.number.ToString();
        }
        else if(RowEnum==RowEnum.edit)
        {
            this.transform.Find("���").GetChild(0).GetComponent<TMP_InputField>().text = medicineData.number.ToString();
        }


    }

    public void disPlayMessage()
    {
        WindowControl.instance.SetWindowData(medicineData);
    }

    public void ReturnToList()
    {
        returnItem();
        changeNumber();

        TableManager.Instance.EditList(medicineData);
        TableManager.Instance.displayTable();
    }

    public void AddSellList()
    {
        returnItem();
        TableManager.Instance.AddSellList(medicineData);
    }

    public void OnValueChange()
    {
        returnItem();
        changeNumber();
        int TempTimes;
        switch (medicineData.Times)
        {
            case Times.һ:
                TempTimes = 1;
                break;
            case Times.��:
                TempTimes = 2;
                break;
            case Times.��: 
                TempTimes = 3;
                break;
            default:
                TempTimes = 0;
                break;
        }
        medicineData.addnumber = (int)((medicineData.eatdose) * TempTimes*medicineData.Day);
        InitializeItem();
        TableManager.Instance.EditList(medicineData);
        TableManager.Instance.displayTable();
    }






}
