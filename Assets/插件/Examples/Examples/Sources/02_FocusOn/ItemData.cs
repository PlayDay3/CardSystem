/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using UnityEngine;

namespace FancyScrollView.Example02
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
