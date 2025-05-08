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
                return ScDefine.ScNickNameValidation.InCompleteHangul;
        }

        return ScDefine.ScNickNameValidation.None;
    }



    private static bool CheckInCompleteHangul(char ch)
    {
        return (ch >= 0x3131 && ch <= 0x314E) ||    // 모음
            (ch >= 0x314F && ch <= 0x3163);         // 자음
    }
}
