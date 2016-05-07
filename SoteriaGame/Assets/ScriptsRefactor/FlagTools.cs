using UnityEngine;
using System.Collections;
using System;

public static class FlagTools
{
	//=================//
	// FOR SCENE FLAGS //
	//=================//
	public static SceneFlags Scene_MakeFlag( params SceneFlags[] flagsToAdd )
	{
		SceneFlags resultFlag = SceneFlags.EMPTY_FLAG;
		
		for( int i = 0; i < flagsToAdd.Length; i++ )
		{
			resultFlag = resultFlag | flagsToAdd[0];
		}
		
		return resultFlag;
	}
	public static void Scene_AddFlag( ref SceneFlags stateToEdit, SceneFlags flagToAdd )
	{
		stateToEdit = stateToEdit | flagToAdd;
	}
	public static void Scene_RemoveFlag( ref SceneFlags stateToEdit, SceneFlags flagToRemove )
	{
		stateToEdit = stateToEdit ^ flagToRemove;
	}
	public static bool Scene_CheckFlag( SceneFlags stateToCheck, SceneFlags flags )
	{
		SceneFlags result = stateToCheck & flags;
		return ( result == flags );
	}
	
	//=================//
	// FOR WORLD FLAGS //
	//=================//
	public static WorldFlags World_MakeFlag( params WorldFlags[] flagsToAdd )
	{
		WorldFlags resultFlag = WorldFlags.EMPTY_FLAG;
		
		for( int i = 0; i < flagsToAdd.Length; i++ )
		{
			resultFlag = resultFlag | flagsToAdd[0];
		}
		
		return resultFlag;
	}
	public static void World_AddFlag( ref WorldFlags stateToEdit, WorldFlags flagToAdd )
	{
		stateToEdit = stateToEdit | flagToAdd;
	}
	public static void World_RemoveFlag( ref WorldFlags stateToEdit, WorldFlags flagToRemove )
	{
		stateToEdit = stateToEdit ^ flagToRemove;
	}
	public static bool World_CheckFlag( WorldFlags stateToCheck, WorldFlags flags )
	{
		WorldFlags result = stateToCheck & flags;
		return ( result == flags );
	}
	
}