using UnityEngine;
using System.Collections;

public class RemoveCards : Reaction
{
	private string _musicDistrictCard = "EggShells";
	private string _theaterDistrictCard = "Chameleon";
	private string _observatoryDistrictCard = "StarChart";
	
	public override void execute()
	{
		GameDirector.instance.RemoveCards(_musicDistrictCard, _theaterDistrictCard, _observatoryDistrictCard);
	}
}