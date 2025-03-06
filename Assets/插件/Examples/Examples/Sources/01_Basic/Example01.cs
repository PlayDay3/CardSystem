/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace FancyScrollView.Example01
{
    class Example01 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;
        public int Range;
        public List<Sprite> imageList;


        void Start()
        {
            var items = Enumerable.Range(0, Range)
                .Select(i => new ItemData($"Cell {i}", imageList[i]))
                .ToArray();

            scrollView.UpdateData(items);
        }
    }
}
