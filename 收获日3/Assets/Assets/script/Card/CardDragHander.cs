using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHander : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Card Card;
    private Camera mainCamera;          // �������
    private float zDepth;               // �����������֮��Ĺ̶�����
    private Vector3 offset;             // �����λ��������λ�õ�ƫ��

    void Start()
    {
        mainCamera = Camera.main;       // ��ȡ�������
    }

    // ����ק��ʼʱ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �����������λ��ת��Ϊ��Ļ����
        zDepth = mainCamera.WorldToScreenPoint(transform.position).z;

        // ��������������������ĵ�ƫ��
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth));
        offset = transform.position - worldPoint;
    }

    // ����ק��ʱ����
    public void OnDrag(PointerEventData eventData)
    {
        // �������Ļ����ת��Ϊ��������
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zDepth; // ���ù̶��� Z ���
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(mousePosition);

        // ���������λ��
        transform.position = worldPoint + offset;
    }

    // ����ק����ʱ����
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drag ended on " + gameObject.name);
    }
}
