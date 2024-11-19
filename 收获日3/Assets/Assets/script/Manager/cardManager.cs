using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class cardManager : MonoBehaviour
{
    public PoolTool poolTool;
    public List<CardData> CardDataList;//全部卡牌数据

    [Header("卡牌库")]
    public CardLibrarySo cardLibrary;//游戏内牌库
    private void Awake()
    {
        InitializeCardDataList();
        
    }
    public void InitializeCardDataList()
    {
        Addressables.LoadAssetsAsync<CardData>("CardData", null).Completed += OnCardDataLoaded;
    }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardData>> obj)
    {
        if(obj.Status == AsyncOperationStatus.Succeeded)
        {
            CardDataList=new List<CardData>(obj.Result);
        }
        else
        {
            Debug.Log("NO CardData Found");
        }

    }
    public GameObject GetCardObj()
    {
        var obj = poolTool.GetObj();
        obj.transform.localScale= Vector3.zero;
        return obj;
    }

    public void DiscardCard(GameObject obj)
    {
        poolTool.ReleaseObj(obj);
    }

}
