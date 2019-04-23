/// <summary>
///
///----------- ESPAÑOL -----------
/// 
/// Este script controla el funcionamiento de los botones incluyendo las ayudas, controla el funcionamiento del
/// juego escogiendo la palabra respectiva del nivel y la imagen.
/// 
///----------- ENGLISH -----------
/// 
/// This script controls the buttons, including the helps. Controls the functioning of
/// the game choosing the corresponding word and respective image.
/// 
/// </summary>

using UnityEngine;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEngine.UI;
using System.Collections;

public class Game_Controller : MonoBehaviour
{

    public string word = "Don't change";
    string playerAnswer = "";
    string[] separedWord;
    Vector2 position = new Vector2(0, 0);
    public GameObject buttonPrefab;
    public GameObject buttonAnswerPrefab;
    string[] allLetters = new string[16];
    string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    int Xposition;
    bool slashFounded = false;
    int positionSlash = 0;
    GameObject[] instanciedButtons;
    GameObject[] instanciedAnswerFields;
    ObscuredBool win = false;
    GameObject buttonNext;
    int lettersPressed = 0;
    bool lookingPicture = false;
    string[] playerAnswerArray;
    string buttonClicked;
    public GameObject AreYouSureWindow;
    Word_Database wordDB;

    GameObject coinsPlayerGame;
    GameObject coinsPlayerMenu;
    public GameObject particleCoins;

    public GameObject WatchVideoButton;

    public static bool addCoinsLog = false;
    bool quitCoinsLog = false;

    public GameObject congratulations;
    public GameObject congratsAllLevelsDone;

    GameObject cameraObj;
    public GameObject rateGameWindow;

    public string subjectTextOfButtonShare;
    public string bodyTextOfButtonShare;
    public int howOftenShowRateWindow = 5;
    public int howManyCoinsEarnByRate;


    private bool isFocus = false;

    private string shareSubject, shareMessage;
    private bool isProcessing = false;
    private string screenshotName;

    //CONVERT SPRITE TO TEXTURE FUNCTION
    Texture2D textureFromSprite(Sprite sprite)
	{
		if(sprite == null) 
		{
			Debug.LogError ("WORD WITHOUT IMAGE: Word #"+ObscuredPrefs.GetInt ("levelWord"));

		}


		if(sprite.rect.width != sprite.texture.width) 
		{
			Texture2D newText = new Texture2D((int)sprite.rect.width,(int)sprite.rect.height);
			Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
			                                             (int)sprite.textureRect.y, 
			                                             (int)sprite.textureRect.width, 
			                                             (int)sprite.textureRect.height );
			newText.SetPixels(newColors);
			newText.Apply();
			return newText;
			
		} else
			return sprite.texture;
		
	}



    void Start ()
    {
        StartLevel();
        cameraObj = GameObject.Find("Main Camera").gameObject;
        if (cameraObj.transform.position.x == 0)
        {
            cameraObj.GetComponent<Animation>().Play("startScene");
        }

    }
    public void StartLevel()
    {

        cameraObj = GameObject.Find("Main Camera").gameObject;
        findObjectsOfVariables();
        setImageAndWord ();

		if (word.Length > 16) 
		{

			allLetters = new string[24];

		}

		separedWord = new string[word.Length];

		if (word.Length <= 16) 
		{
			instanciedButtons = new GameObject[16];
		}
		else 
		{
			instanciedButtons = new GameObject[24];
		}

		instanciedAnswerFields = new GameObject[word.Length];

		if (word.Length > 23) 
		{

			Debug.LogError ("ERROR IN WORD: #"+(ObscuredPrefs.GetInt ("levelWord")+1)+", THE WORD CAN'T CONTAIN MORE THAN 22 LETTERS");

		} 
		else 
		{

			SepareWord ();
			veryLongWordCheck ();
			AnswerFields ();
		}

		RefreshBoardCoins();


    }

	void findObjectsOfVariables () 
	{
		coinsPlayerGame = GameObject.Find ("GAME").transform.Find("CoinsInGame").transform.Find("TotalCoinsPlayer").gameObject;
		coinsPlayerMenu = GameObject.Find ("MENU").transform.Find("CoinsInGame-Menu").transform.Find("TotalCoinsPlayerMenu").gameObject;
		buttonNext = GameObject.Find("ButtonNext").gameObject;
        GameObject.Find("GAME").transform.Find("WatchVideoButton").transform.Find("text").GetComponent<TextMesh>().text = "+" + GameObject.Find("VIDEO-ADS-CONTROL").GetComponent<AdsUnity>().coinsEarnedByVideo;
        wordDB = GameObject.Find ("DATABASES").transform.Find(RefreshQuizSelectionLevels.selectedCategory).GetComponent<Word_Database>();
	}

	void setImageAndWord ()
	{

        if (RefreshQuizSelectionLevels.levelSelected != -1)
        {
           switch (ObscuredPrefs.GetInt("numberLanguae"))
            {
                case 0:


                    word = wordDB.words_List[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 1:


                    word = wordDB.language1[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 2:


                    word = wordDB.language2[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 3:


                    word = wordDB.language3[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 4:


                    word = wordDB.language4[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 5:


                    word = wordDB.language5[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 6:


                    word = wordDB.language6[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 7:


                    word = wordDB.language7[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 8:


                    word = wordDB.language8[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 9:


                    word = wordDB.language9[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 10:


                    word = wordDB.language10[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 11:


                    word = wordDB.language11[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 12:


                    word = wordDB.language12[RefreshQuizSelectionLevels.levelSelected];

                    break;
                case 13:


                    word = wordDB.language13[RefreshQuizSelectionLevels.levelSelected];

                    break;


            }
           

            
	    	// PUT THE IMAGE OF THE LEVEL
		    GameObject.Find("GAME").transform.Find("Image").GetComponent<Renderer>().material.mainTexture = textureFromSprite(wordDB.image[RefreshQuizSelectionLevels.levelSelected]);
    
        }
    }



	void Update ()
	{

		ClicDetections ();

		ShowCoinsLogs();

	}


    //This function detect the click on the buttons and make the corresponding functions
    void ClicDetections()
    {

        if (Input.GetMouseButtonUp (0)) 
		{
			
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
			bool fieldFounded = false;
			
			if (hit.collider != null)
			{

				GameObject objectHit = hit.collider.gameObject;
				
				if(objectHit.name.Contains("buttonLeter")) 
				{
					if (!win)
					{
						for (int i = 0; i < word.Length; i++) 
						{
							if (!fieldFounded)
							{
								
								if (separedWord[i] != "/" && separedWord[i] != " ")
								{
									if(instanciedAnswerFields[i].transform.Find ("Letter").GetComponent<TextMesh>().text == "") 
									{
										instanciedAnswerFields[i].transform.Find ("Letter").GetComponent<TextMesh>().text = objectHit.gameObject.transform.Find("Letter").GetComponent<TextMesh>().text;
										instanciedAnswerFields[i].transform.Find ("Letter").transform.Find("LetterShadow").GetComponent<TextMesh>().text = objectHit.gameObject.transform.Find("Letter").GetComponent<TextMesh>().text;
										objectHit.GetComponent<Collider2D>().enabled = false;
										objectHit.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
										objectHit.transform.Find("Letter").GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, 0f);
										objectHit.transform.Find("Letter").transform.Find("LetterShadow").GetComponent<TextMesh>().color = new Color(1f, 1f, 1f, 0f);
										fieldFounded = true;
										lettersPressed += 1;
									}
								}
								
							}
						}
						
						ChecksWinOrLose();
					}

				}
				else if (objectHit.name.Contains("fieldAnswer")) 
				{
					if (!win)
					{
						for (int h = 0; h < instanciedButtons.Length; h++) 
						{
							
							if (!instanciedButtons[h].GetComponent<Collider2D>().enabled && instanciedButtons[h].transform.Find("Letter").GetComponent<TextMesh>().text == objectHit.transform.Find ("Letter").GetComponent<TextMesh>().text)
							{
								
								objectHit.transform.Find ("Letter").GetComponent<TextMesh>().text = "";
								objectHit.transform.Find ("Letter").Find("LetterShadow").GetComponent<TextMesh>().text = "";
								instanciedButtons[h].GetComponent<Collider2D>().enabled = true;
								instanciedButtons[h].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
								instanciedButtons[h].transform.Find("Letter").GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, 1f);
								instanciedButtons[h].transform.Find("Letter").transform.Find("LetterShadow").GetComponent<TextMesh>().color = new Color(0f, 0f, 0f, 0.4f);
								lettersPressed -= 1;
							}
						}
					}
				} 
				else if(objectHit.name == "ButtonNext")
				{


                    	AdsInterstitialControl.levelsPlayed++;
                    	AdsInterstitialControl.ShowInterstitialAd();

                    //-------------------------------------------------------------------

                    
                    if (RefreshQuizSelectionLevels.levelSelected+1 < wordDB.words_List.Length)
                    {

                        RefreshQuizSelectionLevels.levelSelected += 1;
                        Application.LoadLevel(Application.loadedLevelName);

                    }
                    else
                    {
                        cameraObj.transform.position = new Vector3(-11, 0, -10);
                        Application.LoadLevel(Application.loadedLevelName);
                    }



                }

                else if(objectHit.name == "ButtonOk") 
				{
					
					
					if(buttonClicked == "HelpSolveWord") 
					{
						
						HelpSolveWord();
						Destroy(GameObject.Find("AreYouSureWindow(Clone)").gameObject);	
						if (GameObject.Find("WatchVideoButton2") != null){
							Destroy(GameObject.Find("WatchVideoButton2").gameObject);	
						}

					}
					else if (buttonClicked == "HelpAddLetter") 
					{
						
						HelpAddLetter();
						Destroy(GameObject.Find("AreYouSureWindow(Clone)").gameObject);	
						if (GameObject.Find("WatchVideoButton2") != null){
							Destroy(GameObject.Find("WatchVideoButton2").gameObject);	
						}

					} 

					ChecksWinOrLose();
					
				}
				else if(objectHit.name == "ButtonNo") 
				{
					
					Destroy(GameObject.Find("AreYouSureWindow(Clone)").gameObject);	
					
				}

				
				
			}
			
		}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

            if (hit.collider != null)
            {

                GameObject objectHit = hit.collider.gameObject;

                if (objectHit.name == "Image")
                {

                    if (!lookingPicture)
                    {
                        objectHit.GetComponent<Animation>().Play("lookPicture");
                        lookingPicture = true;
                    }
                    else
                    {
                        objectHit.GetComponent<Animation>().Play("closePicture");
                        lookingPicture = false;
                    }

                }
                else if (objectHit.name == "GoMenuButton")
                {

                    cameraObj.transform.position = new Vector3(-11, 0, -10);
                    Application.LoadLevel(Application.loadedLevelName);

                }
                else if (objectHit.name == "ShareButton" || tag.Equals("ShareButton"))
                {

                    //AndroidDialogAndToastBinding.instance.toastShort("Si cargo");
                    StuckPixel.SPAndroidShare.ShareScreenBytes(subjectTextOfButtonShare, bodyTextOfButtonShare, false);
                    //OnShareButtonClick();
                }
                else if (objectHit.name == "HelpSolveWord")
                {

                    if (!win)
                    {

                        GameObject window = (Instantiate(AreYouSureWindow.gameObject, new Vector3(AreYouSureWindow.transform.position.x, AreYouSureWindow.transform.position.y, AreYouSureWindow.transform.position.z), transform.rotation)) as GameObject;


                        if (ObscuredPrefs.GetInt("coinsPlayer") >= wordDB.coinsToSolveWord)
                        {

                            //Solve puzzle for me
                            window.transform.Find("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 3] + "\n(-" + wordDB.coinsToSolveWord + " " + wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 5] + ")";

                        }
                        else
                        {

                            window.transform.Find("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 9];
                            Destroy(window.transform.Find("ButtonNo").gameObject);
                            Destroy(window.transform.Find("TextNo").gameObject);
                            window.transform.Find("ButtonOk").transform.position = new Vector3(0f, window.transform.Find("ButtonOk").transform.position.y, window.transform.Find("ButtonOk").transform.position.z);
                            window.transform.Find("TextOk").transform.position = new Vector3(0, window.transform.Find("TextOk").transform.position.y, window.transform.Find("TextOk").transform.position.z);

                            GameObject InsWatchVideoButton = (Instantiate(WatchVideoButton.gameObject, new Vector3(0, -0.50f, -9), transform.rotation)) as GameObject;
                            InsWatchVideoButton.name = "WatchVideoButton2";
                            InsWatchVideoButton.transform.Find("text").GetComponent<TextMesh>().text = "+" + GameObject.Find("VIDEO-ADS-CONTROL").GetComponent<AdsUnity>().coinsEarnedByVideo;
                        }

                        buttonClicked = objectHit.name;

                    }

                }
                else if (objectHit.name == "HelpAddLetter")
                {

                    if (!win)
                    {

                        GameObject window = (Instantiate(AreYouSureWindow.gameObject, new Vector3(AreYouSureWindow.transform.position.x, AreYouSureWindow.transform.position.y, AreYouSureWindow.transform.position.z), transform.rotation)) as GameObject;


                        if (ObscuredPrefs.GetInt("coinsPlayer") >= wordDB.coinsToShowOneLetter)
                        {

                            //Show one letter
                            window.transform.Find("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 4] + "\n(-" + wordDB.coinsToShowOneLetter + " " + wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 5] + ")";

                        }
                        else
                        {

                            window.transform.Find("TextInfo").GetComponent<TextMesh>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 9];
                            Destroy(window.transform.Find("ButtonNo").gameObject);
                            Destroy(window.transform.Find("TextNo").gameObject);
                            window.transform.Find("ButtonOk").transform.position = new Vector3(0f, window.transform.Find("ButtonOk").transform.position.y, window.transform.Find("ButtonOk").transform.position.z);
                            window.transform.Find("TextOk").transform.position = new Vector3(0, window.transform.Find("TextOk").transform.position.y, window.transform.Find("TextOk").transform.position.z);

                            GameObject InsWatchVideoButton = (Instantiate(WatchVideoButton.gameObject, new Vector3(0, -0.50f, -9), transform.rotation)) as GameObject; 
                            InsWatchVideoButton.name = "WatchVideoButton2";
                            InsWatchVideoButton.transform.Find("text").GetComponent<TextMesh>().text = "+" + GameObject.Find("VIDEO-ADS-CONTROL").GetComponent<AdsUnity>().coinsEarnedByVideo;

                        }

                        buttonClicked = objectHit.name;

                    }

                }

            }
        }


    }

    void ShowCoinsLogs ()
	{

		if (addCoinsLog)
		{
			GameObject objInstantiated;
			objInstantiated = Instantiate(particleCoins.gameObject, new Vector3 (coinsPlayerGame.transform.parent.transform.Find("Coin").transform.position.x, coinsPlayerGame.transform.parent.transform.Find("Coin").transform.position.y, -3.07f), transform.rotation) as GameObject;
			Destroy(objInstantiated, 3);
			addCoinsLog = false;

		}
		
		if (quitCoinsLog) 
		{
			GameObject objInstantiated;
			objInstantiated = Instantiate(particleCoins.gameObject, new Vector3 (coinsPlayerGame.transform.parent.transform.Find("Coin").transform.position.x, coinsPlayerGame.transform.parent.transform.Find("Coin").transform.position.y, -3.07f), transform.rotation) as GameObject;
			Destroy(objInstantiated, 3);
			quitCoinsLog = false;
			
		}

	}


	void HelpSolveWord ()
	{

		//Save the letters of the answer in an array
		if (ObscuredPrefs.GetInt ("coinsPlayer") >= wordDB.coinsToSolveWord) 
		{

			if (ObscuredPrefs.GetInt("sounds") == 1) 
			{
				cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
			}

						for (int j = 0; j < word.Length; j++) 
						{
			
								if (separedWord [j] != " " && separedWord [j] != "/") 
								{
				
										instanciedAnswerFields [j].transform.Find ("Letter").GetComponent<TextMesh> ().text = separedWord [j];
										instanciedAnswerFields [j].transform.Find ("Letter").Find("LetterShadow").GetComponent<TextMesh> ().text = separedWord [j];
									
								}
						
						}

			ObscuredPrefs.SetInt ("coinsPlayer", ObscuredPrefs.GetInt ("coinsPlayer") - wordDB.coinsToSolveWord);

			quitCoinsLog = true;


			RefreshBoardCoins ();

		}

	}

	
	void HelpAddLetter () 
	{
	

		if (lettersPressed < word.Length)
		{
			if (ObscuredPrefs.GetInt ("coinsPlayer") >= wordDB.coinsToShowOneLetter) 
			{

					if (ObscuredPrefs.GetInt("sounds") == 1)
					{
						cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().buyHelp);
					}

					int randomNumber = Random.Range (0, instanciedAnswerFields.Length);
	
					if (separedWord [randomNumber] != "/" && separedWord [randomNumber] != " ") 
					{
	
							if (instanciedAnswerFields [randomNumber].transform.Find ("Letter").GetComponent<TextMesh> ().text == "") 
							{
	
									instanciedAnswerFields [randomNumber].transform.Find ("Letter").GetComponent<TextMesh> ().text = separedWord [randomNumber];
									instanciedAnswerFields [randomNumber].transform.Find ("Letter").Find("LetterShadow").GetComponent<TextMesh> ().text = separedWord [randomNumber];
									instanciedAnswerFields [randomNumber].GetComponent<Collider2D>().enabled = false;

					
									ObscuredPrefs.SetInt ("coinsPlayer", ObscuredPrefs.GetInt ("coinsPlayer") - wordDB.coinsToShowOneLetter);
									instanciedAnswerFields [randomNumber].GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 1f, 0.9f);



									bool RandomletterFoundedInButtons = false;
									for (int i = 0; i < instanciedButtons.Length; i++) 
									{

									
											if (!RandomletterFoundedInButtons && instanciedButtons[i].GetComponent<Collider2D>().enabled == true && instanciedButtons[i].transform.Find("Letter").GetComponent<TextMesh>().text == instanciedAnswerFields[randomNumber].transform.Find("Letter").GetComponent<TextMesh>().text) 
											{
											
												instanciedButtons[i].GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 1f, 0.9f);
												instanciedButtons[i].GetComponent<Collider2D>().enabled = false;
												RandomletterFoundedInButtons = true;
								
											}
									

									}	
							




									lettersPressed += 1;
									ChecksWinOrLose ();

									quitCoinsLog = true;

									RefreshBoardCoins ();
	
							}
							else
							{
	
									HelpAddLetter ();
	
							}
	
	
	

	
					}
					else
					{
	
						HelpAddLetter ();
	
					}


			}
		}

	}



	void ChecksWinOrLose ()
	{

		bool lose = false;


		if (!win)
		{


		playerAnswerArray = new string[instanciedAnswerFields.Length];

		//Save the letters of the answer in an array
			for (int j = 0; j < word.Length; j++) 
			{

				if (separedWord[j] == " ")
				{
						
						playerAnswerArray[j] = " ";
						
				} 
				else if (separedWord[j] == "/") 
				{

					playerAnswerArray[j] = "/";

				}
				else
				{
					playerAnswerArray[j] = instanciedAnswerFields[j].transform.Find("Letter").GetComponent<TextMesh>().text;
				}
				
			}

		//-----------------------------------------	
			
			for (int y = 0; y < word.Length; y++) 
			{
				if (!win)
				{

					playerAnswer = string.Join ("", playerAnswerArray);


                    if (playerAnswer == word)
                    {

                        win = true;

                        GameObject instantiateCongratulations = Instantiate(congratulations.gameObject, new Vector3(congratulations.transform.position.x, congratulations.transform.position.y, congratulations.transform.position.z), congratulations.transform.rotation) as GameObject;
                        instantiateCongratulations.transform.Find("TextCoins").GetComponent<TextMesh>().text = "+" + wordDB.coinsWinedByWord;
                        if (!wordDB.showInfoLaterOfSolveWord)
                        {
                            Destroy(instantiateCongratulations.transform.Find("Canvas").gameObject);
                            Destroy(instantiateCongratulations, 2.5f);

                        }
                        else
                        {
                            instantiateCongratulations.transform.Find("Canvas").transform.Find("ButtonNext").transform.Find("Text").GetComponent<TextMesh>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 20];

                            switch (ObscuredPrefs.GetInt("numberLanguae"))
                            {
                                case 0:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord0[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 1:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord1[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 2:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord2[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 3:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord3[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 4:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord4[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 5:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord5[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 6:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord6[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 7:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord7[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 8:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord8[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 9:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord9[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 10:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord10[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 11:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord11[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 12:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord12[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                                case 13:
                                    instantiateCongratulations.transform.Find("Canvas").transform.Find("Text").GetComponent<Text>().text = wordDB.languageInfoWord13[RefreshQuizSelectionLevels.levelSelected];
                                    break;
                            }
                            
                        }


                        GameObject.Find("GAME").transform.Find("BoxAnswer").GetComponent<Animation>().Play("winAnswer");

                        if (ObscuredPrefs.GetInt("sounds") == 1)
                        {
                            cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().winQuiz);
                        }

                        //PUT ENABLED THE BUTTON TO NEXT WORD

                        buttonNext.GetComponent<Collider2D>().enabled = true;
                        buttonNext.GetComponent<SpriteRenderer>().color = new Vector4(0, 0.7f, 0, 1f);
                        buttonNext.transform.Find("Symbol").GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, 1f);

                        if (!wordDB.solvedWords[RefreshQuizSelectionLevels.levelSelected])
                        { 
                            ObscuredPrefs.SetInt("coinsPlayer", ObscuredPrefs.GetInt("coinsPlayer") + wordDB.coinsWinedByWord);
                            addCoinsLog = true;
                      
                        }
                        else
                        {
                            instantiateCongratulations.transform.Find("TextCoins").GetComponent<TextMesh>().text = "+" + 0;
                        }

                        wordDB.solvedWords[RefreshQuizSelectionLevels.levelSelected] = true;
                        PlayerPrefsArray.SetBoolArray("completedWords" + wordDB.name, wordDB.solvedWords);

                        
                        if (!ObscuredPrefs.HasKey("allLevelsDone" + wordDB.name))
                        {
                            int numberOfSolvedWords = 0;
                            for (int i = 0; i < wordDB.solvedWords.Length; i++)
                            {

                                if (wordDB.solvedWords[i])
                                {
                                    numberOfSolvedWords++;
                                }

                                if (numberOfSolvedWords == wordDB.solvedWords.Length)
                                {
                                    GameObject instantiatedCongratsAllLevelsDone = Instantiate(congratsAllLevelsDone.gameObject, new Vector3(congratsAllLevelsDone.transform.position.x, congratsAllLevelsDone.transform.position.y, congratsAllLevelsDone.transform.position.z), congratsAllLevelsDone.transform.rotation) as GameObject;
                                    ObscuredPrefs.SetInt("allLevelsDone" + wordDB.name, 1);
                                }

                            }

                            if (!ObscuredPrefs.HasKey("ratedGame"))
                            {
                                if (numberOfSolvedWords % howOftenShowRateWindow == 0)
                                {
                                    GameObject objRatedGame = Instantiate(rateGameWindow.gameObject, new Vector3(rateGameWindow.transform.position.x, rateGameWindow.transform.position.y, rateGameWindow.transform.position.z), rateGameWindow.transform.rotation) as GameObject;
                                  //  objRatedGame.transform.FindChild("Window").transform.FindChild("Text").GetComponent<Text>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 21] + GameObject.Find("Game_Controller").GetComponent<Game_Controller>().howManyCoinsEarnByRate + wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 22];
                                    objRatedGame.transform.Find("Window").transform.Find("ButtonRateNow").transform.Find("Text").GetComponent<Text>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 23];
                                    objRatedGame.transform.Find("Window").transform.Find("ButtonLater").transform.Find("Text").GetComponent<Text>().text = wordDB.uiTextsLang[ObscuredPrefs.GetInt("numberLanguae"), 24];
                                }
                            }
                        }

                        
                        RefreshBoardCoins();
					}
				}
				
			}

			
		}



		if (instanciedAnswerFields[word.Length - 1].transform.Find("Letter").GetComponent<TextMesh>().text != "" && !win && lettersPressed == word.Length) 
		{

			if (!lose) 
			{
		
				lose = true;

				GameObject.Find("BoxAnswer").GetComponent<Animation>().Play("loseAnswer");

				if (ObscuredPrefs.GetInt("sounds") == 1) 
				{
					cameraObj.GetComponent<AudioSource>().PlayOneShot(cameraObj.GetComponent<Sounds>().errorWord);
				}

				if (GameObject.Find("BoxAnswer").GetComponent<Animation>().isPlaying)
				{
					GameObject.Find("BoxAnswer").GetComponent<Animation>().Rewind("loseAnswer");
				}
			}
			
		}		
		
	}

	public void RefreshBoardCoins () 
	{
		
		coinsPlayerGame.GetComponent<TextMesh>().text = ObscuredPrefs.GetInt ("coinsPlayer").ToString();
		coinsPlayerMenu.GetComponent<TextMesh>().text = ObscuredPrefs.GetInt ("coinsPlayer").ToString();
	}


	void veryLongWordCheck ()
	{

		for (int i = 0; i < word.Length; i++) 
		{
				if(separedWord[i] == "/" && slashFounded == false)
				{
					positionSlash = i;
					slashFounded = true;
					lettersPressed += 1;
				}

			if (separedWord[i] == " ") 
			{
				
				lettersPressed += 1;
				
			}
		}
		if (positionSlash >= 12) 
		{
			Debug.LogError ("ERROR IN WORD: #"+(ObscuredPrefs.GetInt ("levelWord")+1)+", YOU JUST CAN ADD AN SLASH '/' BEFORE OF THE LETTER 12 OF YOUR WORD.");
		}

	}

	void SepareWord () 
	{

		//Separe word and save in the array separeWord[]
		for (int i = 0; i < word.Length; i++)
		{
			separedWord[i] = word[i].ToString();


			Xposition = i;

			//First fill with the letters of the words to the array
			CombineLetters();
		}

		//later fill other letters 
		ArraySpaceFilling ();

		//create the buttons in the scene
		InstanciateButtons ();
	}

	void CombineLetters () 
	{

		int randomNumber;

		if (word.Length <= 16) 
		{
			randomNumber = Random.Range (0, 16);

        }
		else 
		{
			randomNumber = Random.Range (0, 24);
		}

		if (allLetters [randomNumber] == null) 
		{

			allLetters [randomNumber] = separedWord[Xposition];

		}
		else
		{

			CombineLetters ();

		}
        

	}

	void ArraySpaceFilling ()
	{

		if (word.Length <= 16) 
		{

			for (int f = 0; f < 16; f++) 
			{

				//if the space in the array is null or it's an "space" or it's an slash
				if (allLetters[f] == null || allLetters[f] == " " || allLetters[f] == "/")
				{
					int randomNumber = Random.Range (0, 27);
					allLetters [f] = alphabet[randomNumber];
				}

			}

		} 
		else 
		{

			for (int f = 0; f < 24; f++) 
			{
				
				//if the space in the array is null or it's an "space" or it's an slash
				if (allLetters[f] == null || allLetters[f] == " " || allLetters[f] == "/")
				{
					int randomNumber = Random.Range (0, 27);
					allLetters [f] = alphabet[randomNumber];
				}
				
			}


		}


	}
	
	void InstanciateButtons ()
	{
			

		if (word.Length <= 16)
		{

				for (int r = 0; r < 16; r++) 
				{
					
					if (r == 0)
					{
						position = new Vector2(-2.373f, -2.742f);
					}
					else if (r == 8) 
					{
						position = new Vector2(-2.373f, -3.672f);
					}
					
					instanciedButtons[r] = (Instantiate(buttonPrefab.gameObject, new Vector2 (position.x, position.y), transform.rotation)) as GameObject;
					instanciedButtons[r].name = allLetters[r] + "_buttonLeter";
					instanciedButtons[r].transform.Find("Letter").GetComponent<TextMesh>().text = allLetters[r].ToString();
					position = new Vector2 (position.x + 0.68f, position.y);
	
	
				}

		}
		else
		{

			for (int r = 0; r < 24; r++) 
			{
				
				if (r == 0)
				{
					position = new Vector2(-2.373f, -2.16f);
				} 
				else if (r == 8)
				{
					position = new Vector2(-2.373f, -2.91f);
				}
				else if (r == 16)
				{
					position = new Vector2(-2.373f, -3.672f);
				}
				
				instanciedButtons[r] = (Instantiate(buttonPrefab.gameObject, new Vector2 (position.x, position.y), transform.rotation)) as GameObject;
				instanciedButtons[r].name = allLetters[r] + "_buttonLeter";
				instanciedButtons[r].transform.Find("Letter").GetComponent<TextMesh>().text = allLetters[r].ToString();
				position = new Vector2 (position.x + 0.68f, position.y);
				
				
			}

		}

		position = new Vector2(0, 0);

	}

	void AnswerFields ()
	{

	//---------------------------------- CENTER THE FIELDS -------------------------------- 

		float positionX = 0f;
		int moreLongWord = 0;


			if (word.Length - positionSlash < positionSlash) 
			{
				moreLongWord = positionSlash;
			} 
			else 
			{
				moreLongWord = word.Length - positionSlash;
			}



		if (slashFounded)
		{

					
			if (moreLongWord == 1) 
			{
				positionX = 0f;
			} 
			else if (moreLongWord == 2) 
			{
				positionX = -0.281f;
			}
			else if (moreLongWord == 3) 
			{
				positionX = -0.56f;
			} 
			else if (moreLongWord == 4) 
			{
				positionX = -0.845f;
			} 
			else if (moreLongWord == 5) 
			{
				positionX = -1.116f ;
			} 
			else if (moreLongWord == 6)
			{
				positionX = -1.409f;
			} 
			else if (moreLongWord == 7) 
			{
				positionX = -1.674f;
			} 
			else if (moreLongWord == 8) 
			{
				positionX = -1.958f;
			}
			else if (moreLongWord == 9) 
			{
				positionX = -2.24f;
			} 
			else if (moreLongWord == 10)
			{
				positionX = -2.519f;
			}
			else if (moreLongWord >= 11) 
			{
				positionX = -2.8f;
			}

			positionX += 0.274f;

			position = new Vector2 (positionX, -0.560f);

		}
		else 
		{

			moreLongWord -= 1;

			if (word.Length == 1)
			{
				positionX = -0.274f;
			}
			else if (word.Length == 2) 
			{
				positionX = -0.281f - 0.24f;
			}
			else if (word.Length == 3) 
			{
				positionX = -0.56f - 0.21f;
			}
			else if (word.Length == 4) 
			{
				positionX = -0.845f - 0.17f;
			}
			else if (word.Length == 5) 
			{
				positionX = -1.116f - 0.15f;
			}
			else if (word.Length == 6) 
			{
				positionX = -1.409f - 0.12f;
			}
			else if (word.Length == 7) 
			{
				positionX = -1.674f - 0.1f;
			}
			else if (word.Length == 8) 
			{
				positionX = -1.958f - 0.07f;
			}
			else if (word.Length == 9) 
			{
				positionX = -2.24f - 0.04f;
			}
			else if (word.Length == 10) 
			{
				positionX = -2.519f - 0.01f;
			}
			else if (word.Length >= 11) 
			{
				positionX = -2.8f;
			}

			positionX += 0.274f;
			position = new Vector2 (positionX, -1.758f);	

			if (word.Length >= 12)
			{
				
				position = new Vector2 (positionX, 0.266f);
				
			}

		}

		//--------------- END CENTER THE FIELDS -------------------------------------------- 

		int numberSlashes = 0;

		for (int i = 0; i < word.Length; i++)
		{

			if (separedWord[i] == "/")
			{
				position = new Vector2(positionX, -1.260f);
				numberSlashes += 1;
				if (numberSlashes >= 2)
				{
					Debug.LogError ("YOU CAN'T HAVE MORE THAN 1 SLASH '/' IN THE WORD");
				}
			}

			if (i == 11 && !slashFounded)
			{
				
				position = new Vector2(positionX, -0.42f);
				
			}
		
	


			if (separedWord[i] != " " && separedWord[i] != "/")
			{

				instanciedAnswerFields[i] = (Instantiate(buttonAnswerPrefab.gameObject, new Vector2 (position.x, position.y), transform.rotation)) as GameObject;
				instanciedAnswerFields[i].name = separedWord[i]+"_fieldAnswer";
				position = new Vector2 (position.x + 0.505f, position.y);

			}
			else if (separedWord[i] != "/")
			{

				position = new Vector2 (position.x + 0.505f, position.y);

			}


		}

	

	}

    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public void OnShareButtonClick()
    {
      
        screenshotName = "fireblock_highscore.png";
        shareSubject = "I challenge you to beat my high score in Fire Block";
        shareMessage = "I challenge you to beat my high score in Fire Block. " +
        ". Get the Fire Block app from the link below. \nCheers\n" +
        "\nhttp://onelink.to/fireblock";

        ShareScreenshot();
    }


    private void ShareScreenshot()
    {

#if UNITY_ANDROID
        if (!isProcessing)
        {
            StartCoroutine(ShareScreenshotInAnroid());
        }

#else
		Debug.Log("No sharing set up for this platform.");
#endif
    }



#if UNITY_ANDROID
    public IEnumerator ShareScreenshotInAnroid()
    {

        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();

        string screenShotPath = Application.persistentDataPath + "/" + screenshotName;
        ScreenCapture.CaptureScreenshot(screenshotName, 1);
        yield return new WaitForSeconds(0.5f);

        if (!Application.isEditor)
        {
            //Create intent for action send
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //create image URI to add it to the intent
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);

            //put image and string extra
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            intentObject.Call<AndroidJavaObject>("setType", "image/png");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your high score");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
#endif
}