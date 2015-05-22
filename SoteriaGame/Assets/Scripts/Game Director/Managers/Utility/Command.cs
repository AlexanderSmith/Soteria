using UnityEngine;
using System.Collections;




public abstract class Command 
{
	public abstract void execute(Object actor);
	public abstract void execute ();

	protected void MovePlayer(Player inPlayer)
	{
		if(	GameDirector.instance.GetCurrentGameState() != GameStates.Encounter || 
		   	GameDirector.instance.GetEncounterState() == EncounterManager.EncounterState.ActiveLight)
		{

			inPlayer.Move();
		}
	}
}

public class LeftCommand : Command
{
	public override void execute (Object actor)
	{
		base.MovePlayer(((GameObject)actor).GetComponent<Player>());
	}

	public override void execute () {}
}

public class RightCommnad : Command
{
	public override void execute (Object actor)
	{
		base.MovePlayer(((GameObject)actor).GetComponent<Player>());
	}

	public override void execute () {}
}

public class UpCommand : Command
{
	public override void execute (Object actor)
	{
		base.MovePlayer(((GameObject)actor).GetComponent<Player>());
	}

	public override void execute () {}
}

public class DownCommand : Command
{
	public override void execute (Object actor)
	{
		base.MovePlayer(((GameObject)actor).GetComponent<Player>());
	}

	public override void execute () {}
}

public class SpaceCommand : Command
{
	public override void execute (Object actor)
	{
		/*
		GameObject Player = (GameObject) actor;
		Vector3 temp = Player.transform.localScale;
		Player.transform.localScale = Vector3.one;
		
		float modifier =  1 + ( 0.1f * GameDirector.instance.GetQTECount ());
		Player.transform.localScale = Vector3.Slerp(temp, new Vector3(( modifier ),
		                                                            ( modifier ), 
		                                                            ( modifier )), Time.deltaTime );*/
	}

	public override void execute () {}
}