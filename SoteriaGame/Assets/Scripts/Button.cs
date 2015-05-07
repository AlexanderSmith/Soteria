using UnityEngine;
using System.Collections;

/// <summary>
/// Button type -> just an enum for keeping track of which keys we use.
/// </summary>
public enum ButtonType
{
	LeftArrow = 0,
	RightArrow,
	UpArrow,
	DownArrow
}
/// <summary>
/// Button type and action related to the key pressed, oldaction is needed for the swap (keeping track of its original use).
/// </summary>
public class Button
{
	private ButtonType _type;
	private Command _action;

	//for later use!
	private Command _OldAction;

	private Button() {}

	public Button(ButtonType inType, Command inCommand)
	{
		this._type = inType;
		this._action = inCommand;
	}

	public void execute(Object inActor = null) //Shouled be player but can be anything else!
	{
		if (inActor == null)
			_action.execute();
		else
			_action.execute(inActor);
	}

	public ButtonType getButtonType()
	{
		return _type;
	}
}
