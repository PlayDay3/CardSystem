using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Stack<int> uiStack = new Stack<int>();

    public GameObject[] uiPanels;  // ����UI����ڳ�����

    void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance= this;
        }
    }

    public void ShowPanel(int index)
    {
        // ���ص�ǰ��岢����Ŀ�����
        foreach (GameObject panel in uiPanels)
            panel.SetActive(false);

        uiPanels[index].SetActive(true);

        if (uiStack.Count > 1)
        {
            int FirstObj= uiStack.Pop();
            int SecondObj= uiStack.Pop();
            if (index == SecondObj)//���ǰ����һ��ҳ�棬����ջ������Ԫ��
            {
                uiStack.Push(FirstObj);
                uiStack.Push(SecondObj);
            }
            else
            {
                uiStack.Push(SecondObj);
                uiStack.Push(FirstObj);
                uiStack.Push(index);
            }
        }
        else
        {
            uiStack.Push(index);  // ����ǰ��������ջ
        }

    }

    public void OnBackButtonClicked()
    {
        if (uiStack.Count > 1)
        {
            uiStack.Pop();  // ������ǰ���
            GameObject previousPanel = uiPanels[uiStack.Peek()];  // ��ȡ��һ�����
            foreach (GameObject panel in uiPanels)
                panel.SetActive(false);

            previousPanel.SetActive(true);  // ��ʾ��һ�����
        }
    }


}
