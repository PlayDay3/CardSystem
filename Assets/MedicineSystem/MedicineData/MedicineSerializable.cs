using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MedicineSerializable : MonoBehaviour
{
    public static MedicineSerializable instance;
    public List<MedicineData> MedicList = new List<MedicineData>();
    public List<MedicineData> NameList = new List<MedicineData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Invoke("LoadData", 0.3f);
        Invoke("LoadNameList", 0.3f);
    }

    private string GetFilePath(string fileName)
    {
        string directoryPath = @"D:\MedicineData";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        return Path.Combine(directoryPath, fileName);
    }

    // ���� MedicList
    public void SaveToJson(string fileName)
    {
        string json = JsonUtility.ToJson(new MedicineDataListWrapper(MedicList), true);
        File.WriteAllText(GetFilePath(fileName), json);
        Debug.Log("MedicList �ѱ��棺" + GetFilePath(fileName));
    }

    // ��ȡ MedicList
    public void LoadFromJson(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MedicineDataListWrapper dataWrapper = JsonUtility.FromJson<MedicineDataListWrapper>(json);
            MedicList = dataWrapper.MedicList;
            Debug.Log("MedicList �Ѽ��أ�" + json);
        }
        else
        {
            Debug.LogWarning("�ļ������ڣ�" + path);
        }
    }

    // ���� NameList
    public void SaveNameListToJson(string fileName)
    {
        string json = JsonUtility.ToJson(new MedicineNameListWrapper(NameList), true);
        File.WriteAllText(GetFilePath(fileName),json);
        Debug.Log("NameList �ѱ��棺" + GetFilePath(fileName));
    }

    // ��ȡ NameList
    public void LoadNameListFromJson(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MedicineNameListWrapper dataWrapper = JsonUtility.FromJson<MedicineNameListWrapper>(json);
            NameList = dataWrapper.NameList;
            Debug.Log("NameList �Ѽ��أ�" + json);
        }
        else
        {
            Debug.LogWarning("�ļ������ڣ�" + path);
        }
    }

    [ContextMenu("Save MedicList")]
    public void SaveData()
    {
        SaveToJson("MedicineData.json");
    }

    [ContextMenu("Load MedicList")]
    public void LoadData()
    {
        LoadFromJson("MedicineData.json");
    }

    [ContextMenu("Save NameList")]
    public void SaveNameList()
    {
        SaveNameListToJson("MedicineNameList.json");
    }

    [ContextMenu("Load NameList")]
    public void LoadNameList()
    {
        LoadNameListFromJson("MedicineNameList.json");
    }

    // ��װ�࣬�������л� MedicList
    [System.Serializable]
    private class MedicineDataListWrapper
    {
        public List<MedicineData> MedicList;
        public MedicineDataListWrapper(List<MedicineData> list)
        {
            MedicList = list;
        }
    }

    // ��װ�࣬�������л� NameList
    [System.Serializable]
    private class MedicineNameListWrapper
    {
        public List<MedicineData> NameList;
        public MedicineNameListWrapper(List<MedicineData> list)
        {
            NameList = list;
        }
    }
}
