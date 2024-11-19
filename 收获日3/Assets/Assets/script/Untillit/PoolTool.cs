using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Pool;
public class PoolTool : MonoBehaviour
{
    [SerializeField]
    public GameObject CardPrefab;
    public ObjectPool<GameObject> pool;
    public static PoolTool instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }


    private void Start()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(CardPrefab, transform),
            actionOnGet:(obj)=> obj.SetActive(true),
            actionOnRelease:(obj)=> obj.SetActive(false),
            actionOnDestroy:(obj)=> Destroy(obj),
            collectionCheck:false,
            defaultCapacity:10,
            maxSize:40

            ) ;


        //PreFillPool(5);
 
    }
    private void PreFillPool(int count)
    {
        var PreFillArray=new GameObject[count];
        for(int i=0;i<count;i++)
        {
            //Debug.Log(pool.CountAll);
            PreFillArray[i] = pool.Get();

        }
        foreach(var item in PreFillArray)
        {
            pool.Release(item);
        }

    }

    public GameObject GetObj()
    {
        
        return pool.Get();
    }
    public void ReleaseObj(GameObject obj)
    {
        pool.Release(obj);
    }

}
