using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHander : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Card Card;
    private Camera mainCamera;          // 主摄像机
    private float zDepth;               // 物体与摄像机之间的固定距离
    private Vector3 offset;             // 鼠标点击位置与物体位置的偏移

    void Start()
    {
        mainCamera = Camera.main;       // 获取主摄像机
    }

    // 当拖拽开始时调用
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 将物体的世界位置转换为屏幕坐标
        zDepth = mainCamera.WorldToScreenPoint(transform.position).z;

        // 计算鼠标点击点与物体中心的偏移
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth));
        offset = transform.position - worldPoint;
    }

    // 当拖拽中时调用
    public void OnDrag(PointerEventData eventData)
    {
        // 将鼠标屏幕坐标转换为世界坐标
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zDepth; // 设置固定的 Z 深度
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePosition);

        // 更新物体的位置
        transform.position = worldPoint + offset;
    }

    // 当拖拽结束时调用
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended on " + gameObject.name);
    }
}
