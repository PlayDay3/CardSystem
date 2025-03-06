using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Stack<int> uiStack = new Stack<int>();

    public GameObject[] uiPanels;  // 所有UI面板在场景中

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
        // 隐藏当前面板并激活目标面板
        foreach (GameObject panel in uiPanels)
            panel.SetActive(false);

        uiPanels[index].SetActive(true);

        if (uiStack.Count > 1)
        {
            int FirstObj= uiStack.Pop();
            int SecondObj= uiStack.Pop();
            if (index == SecondObj)//如果前往上一个页面，交换栈顶上两元素
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
            uiStack.Push(index);  // 将当前面板推入堆栈
        }

    }

    public void OnBackButtonClicked()
    {
        if (uiStack.Count > 1)
        {
            uiStack.Pop();  // 弹出当前面板
            GameObject previousPanel = uiPanels[uiStack.Peek()];  // 获取上一个面板
            foreach (GameObject panel in uiPanels)
                panel.SetActive(false);

            previousPanel.SetActive(true);  // 显示上一个面板
        }
    }


}
