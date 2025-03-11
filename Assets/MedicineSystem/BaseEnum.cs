using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum BaseEnum 
{
    granule,//粒
    slice,//片
    box,//盒
    bag,//袋
    bottle,
    粒,片,盒,袋,瓶,支,包,箱,件
}

public enum Useway
{
    口服,
    静脉注射,
    外用,
    ivdrip40gtt_min,
    ivdrip15gtt_min,
    ivdrip60gtt_min,
}

public enum Times
{
    Once,
    Twice,
    thrice,
    一,
    两,
    三,
}
public enum doseunit
{
    mg,
    g,
    kg,
    ml,
}
public enum RowEnum
{
    display,
    edit,

}
