using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JsonManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private TextAsset dropdownOptionsJson;

    public ItemsList itemsList = new ItemsList();

    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        Debug.Log("dropdownOptionsJson: " + dropdownOptionsJson);
        if (dropdownOptionsJson != null)
        {
            string json = dropdownOptionsJson.text;
            Debug.Log(json);
            ItemsList itemsList = JsonUtility.FromJson<ItemsList>(json);
            Debug.Log("ItemsList: " + itemsList);

            if (itemsList != null && itemsList.explanations != null)
            {
                List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();
                foreach (string item in itemsList.explanations)
                {
                    TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData(item);
                    dropdownOptions.Add(option);
                }

                // Clear and populate the dropdown with the options
                dropdown.ClearOptions();
                dropdown.AddOptions(dropdownOptions);
            }
            else
            {
                Debug.LogWarning("Failed to parse JSON or items list is empty.");
            }
        }
        else
        {
            Debug.LogError("No TextAsset assigned for dropdown options JSON file.");
        }
    }

    [System.Serializable]
    public class ItemsList
    {
        public string[] explanations;
    }
}