/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example08
{
    class Cell : FancyGridViewCell<ItemData, Context>
    {
        [SerializeField] Text message = default;
        [SerializeField] Image image = default;
        [SerializeField] Button button = default;
        public PrescriptionMessage PrescriptionMessage=new PrescriptionMessage();

        public override void Initialize()
        {
            //button.onClick.AddListener(() => Context.OnCellClicked?.Invoke(Index));
            button.onClick.AddListener(() =>
            {
                //Context.OnItemClicked?.Invoke(PrescriptionMessage);
                Prescription_Description.Instance.OnItemClicked?.Invoke(PrescriptionMessage);
            });

        }

        public override void UpdateContent(ItemData itemData)//编辑Cell
        {
            string date = itemData.PrescriptionMessage.Patient.Date; // "2025-02-18 14:30:45"
            string[] parts = date.Split(' '); // 根据空格分割，获取日期部分和时间部分
            string formattedDate;
            if (parts.Length > 0)
            {
                formattedDate = parts[0]; // 保留日期部分 (2025-02-18)
            }
            else
            {
                formattedDate = date; // 如果没有空格，保留原样
            }
            string[] part2 = formattedDate.Split('-');

            if (part2.Length > 1)
            {
                formattedDate = part2[0] + "-\n" + string.Join("-", part2, 1, part2.Length - 1);
            }
            else
            {
                formattedDate = date; // 没有 "-"，保持原样
            }
            
           
            message.text = itemData.PrescriptionMessage.Patient.Name+"\n"+ formattedDate;
            PrescriptionMessage = itemData.PrescriptionMessage;
            var selected = Context.SelectedIndex == Index;
            image.color = selected
                ? new Color32(0, 255, 255, 100)
                : new Color32(255, 255, 255, 77);

            date = null;
            parts = null;
            formattedDate= null;
        }

        protected override void UpdatePosition(float normalizedPosition, float localPosition)
        {
            base.UpdatePosition(normalizedPosition, localPosition);

            var wave = Mathf.Sin(normalizedPosition * Mathf.PI * 2) * 65;
            transform.localPosition += Vector3.right * wave;
        }
    }
}
