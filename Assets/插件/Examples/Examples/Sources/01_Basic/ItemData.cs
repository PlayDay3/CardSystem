/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example01
{
    class ItemData
    {
        public string Message { get; }
        public Sprite Image;

        public ItemData(string message, Sprite image)
        {
            Message = message;
            Image = image;
        }
    }
}
