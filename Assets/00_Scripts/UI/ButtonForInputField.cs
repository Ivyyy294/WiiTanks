using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonForInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField hostPortInputField;
    [SerializeField] private TMP_InputField joinIpInputField;
    [SerializeField] private TMP_InputField joinPortInputField;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_Text cannotConnectToHostText;
    [SerializeField] private TMP_Text cannotConnectToClientText;
    [SerializeField] private TMP_Text invalidInputText;

	//Lara Values
	[SerializeField] UnityEvent startGame;

    private NetworkManager networkManager;
    
    //public GameObject targetObject;
    //public List<GameObject> targetObject = new List<GameObject>();
    void Start()
    {
        networkManager = NetworkManager.Me;
        /*Button toggleButton = GetComponent<Button>();
        toggleButton.onClick.AddListener(ToggleGameObject);*/
        cannotConnectToClientText.gameObject.SetActive(false);
        cannotConnectToHostText.gameObject.SetActive(false);
        invalidInputText.gameObject.SetActive(false);
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
                invalidInputText.gameObject.SetActive(true);
                StartCoroutine(HideTextDelay(invalidInputText, 2f));
                return;
            }

            var hasStartedConnectedToHost = networkManager.StartHost(port);
            if (hasStartedConnectedToHost)
            {
                Debug.Log("I am the host");
                StartGame();
            }
            else
            {
                Debug.Log("Can't Connect - Host");
                cannotConnectToHostText.gameObject.SetActive(true);
                StartCoroutine(HideTextDelay(cannotConnectToHostText, 2f));
            }
        }
        else
        {
            invalidInputText.gameObject.SetActive(true);
            StartCoroutine(HideTextDelay(invalidInputText, 2f));
            Debug.Log("Invalid Port Format");
        }
    }

    private void StartClient()
    {
        string ip = joinIpInputField.text;
        if (ip == "")
        {
            Debug.Log("No Input");
            invalidInputText.gameObject.SetActive(true);
            StartCoroutine(HideTextDelay(invalidInputText, 2f));
            return;
        }

        if (int.TryParse(joinPortInputField.text, out int port))
        {
            Debug.Log($"Attempting to connect to {ip}:{port}");
            var hasStartedConnectedToClient = networkManager.StartClient(ip, port);
            if (hasStartedConnectedToClient)
            {
                Debug.Log("Connected successfully");
                StartGame();
            }
            else
            {
                Debug.Log("Can't Connect - Client");
                cannotConnectToClientText.gameObject.SetActive(true);
                StartCoroutine(HideTextDelay(cannotConnectToClientText, 2f));
            }
        }
        else
        {
            invalidInputText.gameObject.SetActive(true);
            StartCoroutine(HideTextDelay(invalidInputText, 2f));
            Debug.Log("Invalid Format Client");
        }
    }
    
	private void StartGame()
	{
		if (startGame != null)
			startGame.Invoke();
	}

    private IEnumerator HideTextDelay(TMP_Text text, float delay)
    {
        yield return new WaitForSeconds(delay);
        text.gameObject.SetActive(false);
    }
}
