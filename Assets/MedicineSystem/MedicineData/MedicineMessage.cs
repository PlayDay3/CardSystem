using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName="MedicineMessage",menuName = "Medicine/MedicineMessage")]
[System.Serializable]
public class MedicineMessage
{
    
    public string Name;//名字
    public string ID;//编号
    [Header("规格")]
    public int capacity;//一包容量
    [Header("产地")]
    public string source;//生产来源
    //public string dosage;//剂量
    [Header("批号")]
    public string signIndex;//批号

    public doseunit doseunit;//剂量单位,mg,g
    public BaseEnum minunit;//片,粒
    public BaseEnum maxunit;//盒,瓶
    public float dosage;//一片含量
}
