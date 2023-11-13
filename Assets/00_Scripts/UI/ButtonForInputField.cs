using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonForInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField hostPortInputField;
    [SerializeField] private TMP_InputField joinIpInputField;
    [SerializeField] private TMP_InputField joinPortInputField;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;

    private NetworkManager networkManager;
    
    //public GameObject targetObject;
    //public List<GameObject> targetObject = new List<GameObject>();
    void Start()
    {
        networkManager = NetworkManager.Me;
        /*Button toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(ToggleGameObject);*/
        
        hostButton.onClick.AddListener(StartHost);
        joinButton.onClick.AddListener(StartClient);
    }

    /*void ToggleGameObject()
    {
        foreach (var objects in targetObject)
        {
            objects.SetActive(!objects.activeSelf);
        }
    }*/

    private void StartHost()
    {
        int port = int.Parse(hostPortInputField.text);
        networkManager.StartHost(port);
        Debug.Log("I am the host");
    }

    private void StartClient()
    {
        string ip = joinIpInputField.text;
        int port = int.Parse(joinPortInputField.text);
        networkManager.StartClient(ip, port);
        //Debug.Log(ip);
        //Debug.Log(port);
        Debug.Log(joinPortInputField);
        SceneManager.LoadScene(2);

    }
}
