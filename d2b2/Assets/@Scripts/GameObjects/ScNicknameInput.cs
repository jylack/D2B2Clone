using TMPro;
using UnityEngine;

public class ScNicknameInput : MonoBehaviour
{
    private void Start()
    {
        var inputField = GetComponent<TMP_InputField>();
        inputField.onValidateInput += OnValidateInput;
    }



    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (char.IsWhiteSpace(addedChar))
            return '\0';

        return addedChar;
    }
}
