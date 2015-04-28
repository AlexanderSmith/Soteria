using UnityEngine;
using System.Collections;

public abstract class Command 
{
	public abstract void execute(Object player);
}

public class LeftCommand : Command
{
	public override void execute (Object player)
	{
		((Player)player).DOSOMETHING();
	}
}

public class RightCommnad : Command
{
	public override void execute (Object player)
	{
		((Player)player).DOSOMETHING();
	}
}

public class UpCommand : Command
{
	public override void execute (Object player)
	{
		((Player)player).DOSOMETHING();
	}
}

public class DownCommand : Command
{
	public override void execute (Object player)
	{
		((Player)player).DOSOMETHING();
	}
}