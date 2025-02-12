using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour {

	public Canvas startMenu;
	public Button lowBuildings;
	public Button midBuildings;
	public Button tallBuildings;
	public Button exitButton;

	Scene lowScene;
	Scene midScene;
	Scene tallScene;

	float timeSinceStart;
	float timer;
	bool timerBool = false;

	List<Button> buttonList = new List<Button>();
	ColorBlock originalColors;
	ColorBlock flippedColors;
	int activeButton;

	string ActiveChoice;

	public Button anyButton;

	// Use this for initialization
	void Start () {

	
		originalColors = anyButton.colors;
		flippedColors = anyButton.colors;
		flippedColors.normalColor = anyButton.colors.highlightedColor;
		flippedColors.highlightedColor = anyButton.colors.normalColor;

		buttonList.Add(midBuildings);		// 0
		buttonList.Add(lowBuildings);		// 1

		buttonList.Add(tallBuildings); 		// 2
		buttonList.Add(exitButton);			// 3

		startMenu = GetComponent<Canvas>();

	}


	void ShowMenu(){
		startMenu.enabled = true;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;

		timer = Time.time - timeSinceStart;
		timerBool = true;
		activeButton = 1;
	}
		


	void Update(){

		if (Input.GetKeyDown(KeyCode.Escape)){
			if (!startMenu.enabled){
				buttonList[activeButton].colors = flippedColors;

				ShowMenu();
				startMenu.planeDistance = 0.5f;

			}
			else if (startMenu.enabled){

				HideMenu();

			}
		}

		if (Input.GetKeyDown(KeyCode.R)){
			RestartLevel();	
		}

		if (startMenu.enabled){

			if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0){

				if (Input.GetKeyDown(KeyCode.DownArrow)){
					buttonList[activeButton].colors = originalColors;
					activeButton = (activeButton + 1) % buttonList.Count;
					buttonList[activeButton].colors = flippedColors;

				}
				if (Input.GetKeyDown(KeyCode.UpArrow)){
					buttonList[activeButton].colors = originalColors;
					activeButton = Modulo(activeButton-1, buttonList.Count);//(activeButton - 1) % buttonList.Count;
					buttonList[activeButton].colors = flippedColors;

				}

			}

			if (Input.GetKeyDown(KeyCode.Return)){

				if (activeButton == 0){			// Lowbuildings //

					SceneManager.LoadScene("Demo");
				}								// Lowbuildings

				else if (activeButton == 1){
					SceneManager.LoadScene("DemoLow");			
				
				}

				else if (activeButton == 2){
					SceneManager.LoadScene("DemoHi");			

				}

				else if (activeButton == 3){
					ExitGame();
				}
			}


			if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0){
				for (int c = 0; c < buttonList.Count; c++){
					buttonList[c].colors = originalColors;
				}
			}


		}

	}

	void HideMenu(){
		startMenu.enabled = false;
		ActiveChoice = "";
		Cursor.visible = false;
		//transform.GetComponentInParent<BlenderControl>().enabled = true;

		Cursor.lockState = CursorLockMode.Locked;

		timerBool = false;
	}


	int Modulo(int a, int b){
		return ((a %= b) < 0) ? a+b : a;
	}

	void ContinuousRise(){


		if (timer < 10f){
			int lastSec = (int)timer;
			timer += Time.deltaTime;
			if (lastSec < (int)timer){	// Gör nåt varje sekund

			}

		}

	}

	public void ResumePress(){

		startMenu.enabled = false;
		Cursor.visible = false;
		//transform.GetComponentInParent<BlenderControl>().enabled = true;
		Cursor.lockState = CursorLockMode.Locked;

	}

	public void RestartLevel(){
		SceneManager.LoadScene("MultiplyTest");
	}

	public void ExitGame(){
		startMenu.enabled = false;
		Debug.Log("Exited Game!");
		Application.Quit();
	}





}
