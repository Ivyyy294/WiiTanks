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
        if (int.TryParse(hostPortInputField.text, out int port))
        {
            if (port == 0)
            {
                Debug.Log("No Input");
                return;
            }

            var hasStartedConnectedToHost = networkManager.StartHost(port);
            if (hasStartedConnectedToHost)
            {
                Debug.Log("I am the host");
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Can't Connect - Host");
            }
        }
        else
        {
            Debug.Log("Invalid Port Format");
        }
    }

    private void StartClient()
    {
        string ip = joinIpInputField.text;
        if (ip == "")
        {
            Debug.Log("No Input");
            return;
        }

        if (int.TryParse(joinPortInputField.text, out int port))
        {
            Debug.Log($"Attempting to connect to {ip}:{port}");
            var hasStartedConnectedToClient = networkManager.StartClient(ip, port);
            if (hasStartedConnectedToClient)
            {
                Debug.Log("Connected successfully");
                SceneManager.LoadScene(2);
            }
            else
            {
                Debug.Log("Can't Connect - Client");
            }
        }
        else
        {
            Debug.Log("Invalid Format Client");
        }
    }
    
}
