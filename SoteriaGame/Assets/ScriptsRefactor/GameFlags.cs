using UnityEngine;
using System.Collections;
using System.FlagsAttribute;

namespace GameEnums
{
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
	[System.Flags]
	public enum SceneFlags
	{
		EMPTY_FLAG			= 0x00000000,

		FLAG_1				= 0x00000001,
		FLAG_2				= 0x00000010,
		FLAG_3				= 0x00000100,
		FLAG_4				= 0x00001000,
		FLAG_5				= 0x00010000,
		FLAG_6				= 0x00100000,
		FLAG_7				= 0x01000000,
		FLAG_8				= 0x10000000,

		SF_FORCE_DWORD 		= 0x7FFFFFFF
	};

	//========================================================//
	//	WORLD_STATE FLAGS										
	//========================================================//
	[System.Flags]
	public enum WorldFlags
	{
		EMPTY_FLAG			= 0x00000000,
		
		TOKEN				= 0x00000001,
		LANTERN				= 0x00000002,
		COMPASS				= 0x00000004,
		SUIT				= 0x00000008,

		CARD_1				= 0x00000010,
		CARD_2				= 0x00000020,
		CARD_3				= 0x00000040,
//		SOMECARDSTUFF		= 0x00000080,

//		0x1 0x2 x4 0x8 x10 0x20
//		FROM_TOKEN			= 64,
//		FROM_MUSIC			= 128,
//		FROM_OBS			= 256,
//		CLEAR_LOCators 		=
//		...
//
//	-Go to Hub
//		if( worldFlag && FROM_TOKEN == FROM_TOKEN )
//			spawn in token entrance
//		elsif( worldFlag && FROM_MUSIC == FROM_MUSIC )
//			spwawn in Music Entrance

		WF_FORCE_DWORD 		= 0x7FFFFFFF
	};
};