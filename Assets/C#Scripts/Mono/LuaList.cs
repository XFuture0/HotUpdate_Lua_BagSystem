using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public static class LuaList 
{
    [CSharpCallLua]
    public static List<Type> CSharpCallLuaList = new List<Type> ();
}
