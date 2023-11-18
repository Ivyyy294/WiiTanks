using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField] GameObject menu = null;
	[SerializeField] GameObject lobby = null;
	[SerializeField] GameObject level = null;

	private void Start()
	{
		if (menu != null)
			menu.SetActive(true);

		if (lobby != null)
			lobby.SetActive (false);

		if (level != null)
			level?.SetActive (false);
	}

	public void ShowLobby()
	{
		if (menu != null)
			menu.SetActive(false);

		if (lobby != null)
			lobby.SetActive (true);

		if (level != null)
			level?.SetActive (false);
	}

	public void ShowLevel()
	{
		if (menu != null)
			menu.SetActive(false);

		if (lobby != null)
			lobby.SetActive (false);

		if (level != null)
			level?.SetActive (true);
	}
}
