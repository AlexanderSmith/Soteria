using UnityEngine;
using System.Collections;
using System;


	//========================================================//
	//	SCENES IN GAME										
	//========================================================//
	public enum SceneName
	{
		MAIN_MENU		= 0,
		CREDITS			= 1,

		HARBOR			= 2,
		HARBOR_RESPAWN	= 3,

		HUB_1			= 4,
		HUB_2			= 5,
		HUB_3			= 6,
		HUB_4			= 7,

		MUSIC_1			= 8,
		MUSIC_2			= 9,
		MUSIC_3			= 10,
		MUSIC_4			= 11,
		MUSIC_PUZZLE	= 12,
		
		THEATER_1		= 13,
		THEATER_2		= 14,
		THEATER_3		= 15,
		THEATER_4		= 16,
		// THEATER_PUZZLE == PuppetPuzzle
		THEATER_PUZZLE	= 17,

		OBS_1			= 18,
		OBS_2			= 19,
		OBS_3			= 20,
		OBS_4			= 21,
		OBS_PUZZLE		= 22,

		SEWERS			= 23,

		TUTORIAL		= 24,
		
		NO_SCENE			= 0x7FFFFFFE,
		S_FORCE_DWORD		= 0x7FFFFFFF
	};


	//========================================================//
	//	SCENE_STATE FLAGS										
	//========================================================//
	[Flags]
	public enum SceneFlags
	{
		EMPTY_FLAG			= 0x00000000,

		HEX_00000001				= 0x00000001,
		HEX_00000010				= 0x00000010,
		HEX_00000100				= 0x00000100,
		HEX_00001000				= 0x00001000,
		HEX_00010000				= 0x00010000,
		HEX_00100000				= 0x00100000,
		HEX_01000000				= 0x01000000,
		HEX_10000000				= 0x10000000,

		SF_FORCE_DWORD 		= 0x7FFFFFFF
	};

	//========================================================//
	//	WORLD_STATE FLAGS										
	//========================================================//
	[Flags]
	public enum WorldFlags
	{
		EMPTY_FLAG			= 0x00000000,
		
		GAME_PHASE1			= 0x00000001,
		GAME_PHASE2			= 0x00000002,
		GAME_PHASE3			= 0x00000004,
		GAME_PHASE4			= 0x00000008,

		HUB_PHASE1			= 0x00000010,
		HUB_PHASE2			= 0x00000020,
		HUB_PHASE3			= 0x00000040,
		HUB_PHASE4			= 0x00000080,
		
		ITEM_TOKEN			= 0x00000100,
		ITEM_LANTERN		= 0x00000200,
		ITEM_COMPASS		= 0x00000400,
		ITEM_SUIT			= 0x00000800,

		FROM_PUZZLE			= 0x00001000,
		FROM_MUSIC			= 0x00002000,
		FROM_THEATER		= 0x00004000,
		FROM_OBS			= 0x00008000,

		TOKEN_USED			= 0x00010000,
		WRONG_CARD			= 0x00020000,
		FULL_ON_CARDS		= 0x00040000,
		TUTORIAL_COMPLETE	= 0x00080000,
		
//		MUSIC_ACTIVATED		= 0x00010000,
//		THEATER_ACTIVATED	= 0x00020000,
//		OBS_ACTIVATED		= 0x00040000,
		
		MUSIC_WITH_SUIT		= 0x00100000,
		THEATER_WITH_SUIT	= 0x00200000,
		OBS_WITH_SUIT		= 0x00400000,
		
		MUSIC_DEFEATED		= 0x01000000,
		THEATER_DEFEATED	= 0x02000000,
		OBS_DEFEATED		= 0x04000000,
//		...
//
//	-Go to Hub
//		if( worldFlag && FROM_TOKEN == FROM_TOKEN )
//			spawn in token entrance
//		elsif( worldFlag && FROM_MUSIC == FROM_MUSIC )
//			spwawn in Music Entrance

		WF_FORCE_DWORD 		= 0x7FFFFFFF
	};