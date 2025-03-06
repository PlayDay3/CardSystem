using FancyScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MedicineManager : MonoBehaviour
{
    public static MedicineManager instance;
    public PoolTool pooltool;
    public MedicineList medicineList;
    [SerializeField]



    private void Awake()
    {
        //InitializeList();
    }

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

        Invoke("StartShow", 0.1f);


    }

    public void StartShow()
    {
        UIManager.Instance.ShowPanel(0);
    }


}
