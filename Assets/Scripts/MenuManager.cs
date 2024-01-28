using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    public TMP_InputField inputField;
    // Start is called before the first frame update
    public void StartGame()
    {
        if (inputField.text != null)
        {
            DataPersist.Instance.nameInput = inputField.text;
        }
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
