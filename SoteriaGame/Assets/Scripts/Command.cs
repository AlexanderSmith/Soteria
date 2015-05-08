﻿using UnityEngine;
using System.Collections;

public abstract class Command 
{
	public abstract void execute(Object actor);
	public abstract void execute ();
}

public class LeftCommand : Command
{
	public override void execute (Object actor)
	{
		((Player)actor).DOSOMETHING();
	}

	public override void execute () {}
}

public class RightCommnad : Command
{
	public override void execute (Object actor)
	{
		((Player)actor).DOSOMETHING();
	}

	public override void execute () {}
}

public class UpCommand : Command
{
	public override void execute (Object actor)
	{
		((Player)actor).DOSOMETHING();
	}

	public override void execute () {}
}

public class DownCommand : Command
{
	public override void execute (Object actor)
	{
		((Player)actor).DOSOMETHING();
	}

	public override void execute () {}
}