using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionBtn : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resolutionText;
    [SerializeField] GameObject ConfirmBtn;
    [SerializeField] List<Resolution> resolutionList;
    [SerializeField] int index;

    private void Start()
    {
        ConfirmBtn.SetActive(false);
        for (int i = 0; i < resolutionList.Count; i++)
        {
            if (resolutionList[i].width == Screen.width && resolutionList[i].height == Screen.height)
            {
                resolutionText.text = resolutionList[i].ExportString();
                index = i;
            }
        }
    }

    public void OnRightClick()
    {
        if(index < resolutionList.Count -1)
        {
            index += 1;
            resolutionText.text = resolutionList[index].ExportString();

            if (resolutionList[index].width == Screen.width && resolutionList[index].height == Screen.height)
            {
                ConfirmBtn.SetActive(false);
            }
            else ConfirmBtn.SetActive(true);
        }
    }

    public void OnLeftClick()
    {
        if (index > 0)
        {
            index -= 1;
            resolutionText.text = resolutionList[index].ExportString();

            if (resolutionList[index].width == Screen.width && resolutionList[index].height == Screen.height)
            {
                ConfirmBtn.SetActive(false);
            }
            else ConfirmBtn.SetActive(true);
        }  
    }

    public void confirmBtnClick()
    {
        SaveLoadSystem.SaveGameSettingData(resolutionList[index]);
        Screen.SetResolution(resolutionList[index].width, resolutionList[index].height, false);
        ConfirmBtn.SetActive(false);
    }
}

[System.Serializable]
public class Resolution
{
    public int width;
    public int height; 

    public string ExportString()
    {
        return width.ToString() +"x"+ height.ToString();
    }
}
