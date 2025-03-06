/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

namespace FancyScrollView.Example08
{
    class ItemData
    {
        public int Index { get; }
        public PrescriptionMessage PrescriptionMessage=new PrescriptionMessage();

        public ItemData(int index, PrescriptionMessage Message)
        {
            this.Index = index;
            PrescriptionMessage= Message;
        }

    }
}
