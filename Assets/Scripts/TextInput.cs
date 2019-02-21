using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour 
{
	public InputField inputField;

	GameController controller;

	void Awake()
	{
		controller = GetComponent<GameController> ();
        //Listens until user presses Return key or another UI element
		inputField.onEndEdit.AddListener (AcceptStringInput);
	}

	void AcceptStringInput(string userInput)
	{
		userInput = userInput.ToLower();
		controller.LogStringWithReturn("> "+userInput);

		char[] delimiterCharacters = { ' ' };
		string[] separatedInputWords = userInput.Split (delimiterCharacters);

		for (int i = 0; i < controller.inputActions.Length; i++) 
		{
			InputAction inputAction = controller.inputActions [i];
			if (inputAction.keyWord == separatedInputWords [0]) 
			{
				inputAction.RespondToInput (controller, separatedInputWords);
			}
		}

		InputComplete();

	}

	void InputComplete()
	{
		controller.DisplayLoggedText();
        //Put the cursor back on the input field automatically
		inputField.ActivateInputField();
		inputField.text = null;
	}

}
