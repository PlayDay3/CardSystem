using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName="MedicineMessage",menuName = "Medicine/MedicineMessage")]
[System.Serializable]
public class MedicineMessage
{
    
    public string Name;//����
    public string ID;//���
    [Header("���")]
    public int capacity;//һ������
    [Header("����")]
    public string source;//������Դ
    //public string dosage;//����
    [Header("����")]
    public string signIndex;//����

    public doseunit doseunit;//������λ,mg,g
    public BaseEnum minunit;//Ƭ,��
    public BaseEnum maxunit;//��,ƿ
    public float dosage;//һƬ����
}
