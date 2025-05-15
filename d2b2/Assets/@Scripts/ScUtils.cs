using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ScUtils
{
    public static ScDefine.ScNickNameValidation CheckNickNameValidation(string nickName)
    {
        string newNickName = nickName.Trim();

        if (string.IsNullOrEmpty(newNickName))
            return ScDefine.ScNickNameValidation.Empty;

        if (nickName.Length < 2)
            return ScDefine.ScNickNameValidation.LessThan2Char;

        foreach (char ch in nickName)
        {
            if (CheckInCompleteHangul(ch))
                return ScDefine.ScNickNameValidation.IncompleteHangul;
        }

        return ScDefine.ScNickNameValidation.None;
    }

    public static int Power(int x, int n)
    {
        int result = 1;
        int baseValue = x;

        while (n > 0)
        {
            if ((n & 1) == 1) // nÀÌ È¦¼öÀÏ ¶§
                result *= baseValue;

            baseValue *= baseValue; // Á¦°ö
            n >>= 1; // nÀ» 2·Î ³ª´®
        }

        return result;
    }
    
    public static List<TComp> GetComponentsInChildrenEx<TComp>(GameObject parent) where TComp : Component
    {
        return parent.GetComponentsInChildren<TComp>(true).ToList();
    }
    
    public static TComp GetComponentInChildrenEx<TComp>(GameObject parent, string name = null) where TComp : Component
    {
        TComp[] comps = parent.GetComponentsInChildren<TComp>(true);
        if (comps == null)
            return null;
    
        foreach (TComp comp in comps)
        {
            if (comp.gameObject.name == name || name == null)
                return comp;
        }

        return null;
    }

    public static TComp GetComponentInParentEx<TComp>(GameObject obj, string name) where TComp : Component
    {
        TComp[] comps = obj.GetComponentsInParent<TComp>(true);
        if (comps?.Length > 0)
        {
            foreach (TComp comp in comps)
            {
                if (comp.gameObject.name == name)
                    return comp;
            }
        }

        return default;
    }

    public static bool IsDestroyed(GameObject obj)
    {
        return obj == null || !ReferenceEquals(obj, null);
    }



    private static bool CheckInCompleteHangul(char ch)
    {
        return (ch >= 0x3131 && ch <= 0x314E) ||    // ¸ðÀ½
            (ch >= 0x314F && ch <= 0x3163);         // ÀÚÀ½
    }
}
