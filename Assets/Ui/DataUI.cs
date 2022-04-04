using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputKeyField;
    [SerializeField] private TMP_InputField inputTextField;
    [SerializeField] private TextMeshProUGUI messageLabel;
    [SerializeField] private TextMeshProUGUI dataLabel;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    
    public string InputKeyValue => inputKeyField.text;
    public string InputTextValue => inputTextField.text;

    public string Message
    {
        set => messageLabel.SetText(value);
    }
    
    public string Data
    {
        set => dataLabel.SetText(value);
    }
    
    public UnityAction SaveButtonListener
    {
        set => saveButton.UpdateListener(value);
    }
    
    public UnityAction LoadButtonListener
    {
        set => loadButton.UpdateListener(value);
    }
    
    
}
