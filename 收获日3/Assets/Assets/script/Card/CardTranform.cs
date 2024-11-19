using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardTranform
{
    public Vector3 Positon;
    public Quaternion rotation;
    public CardTranform(Vector3 vector3,Quaternion Rotation)
    {
        Positon=vector3;
        rotation=Rotation;
    }

}
