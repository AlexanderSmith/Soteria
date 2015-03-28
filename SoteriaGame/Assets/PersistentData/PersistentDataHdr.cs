using UnityEngine;
using System.Collections;

/*
 * struct PeristentDataFileHdr
 * 
 * Quick struct to represent header for each persistent data file
 * This can hold info describing what the player was doing on this
 * selected save the last time he saved it
 * 
 * e.g. Checkpoint location, current chapter, thumbnails, etc
 * Data in this struct subject to change.
 * 
 */
public struct PersistentDataFileHdr
{
	//this is all dummy info just for prototyping
	public string m_CurrChapter;
	public string m_Checkpoint;
	public PersistentDataFileID m_Id;

	public PersistentDataFileHdr(string chapter, string checkpoint, PersistentDataFileID id)
	{
		this.m_CurrChapter = chapter;
		this.m_Checkpoint = checkpoint;
		this.m_Id = id;
	}

	//no default struct ctors in C#..
	public static PersistentDataFileHdr GetEmptyHdr()
	{
		PersistentDataFileHdr hdr;

		hdr.m_CurrChapter = "";
		hdr.m_Checkpoint = "";
		hdr.m_Id = PersistentDataFileID.E_SAVE_FILE_NA;

		return hdr;
	}
}

/*
 * struct PersistentDataHdr
 * 
 * Quick struct to represent header for each object serialized
 * in the data file.  The serialized PersistentDataID is used to
 * identify where a particular objects data starts/begins
 */
public struct PersistentDataItemHdr
{
	public PersistentDataID m_PersistentItemID;
	public int m_NumItems;
	public int m_SaveDataSize;	

	/*
	 * C# prefers to just zero everything out for structs
	 * so default ctors are not allowed for structs
	 * 
	 * This is my workaround
	 */
	public static PersistentDataItemHdr GetEmptyHdr()
	{
		PersistentDataItemHdr hdr;

		hdr.m_PersistentItemID = PersistentDataID.E_MISSING_ID;
		hdr.m_NumItems = 0;
		hdr.m_SaveDataSize = 0;

		return hdr;
	}

	/*
	 * Because of how C# handles alignment, can't call sizeof on a struct,
	 * so I just do this.  We do member-wise serialization of the struct
	 * so this works fine.
	 */
	public static int GetSizeOfHdr()
	{
		return sizeof(PersistentDataID) + sizeof(int) * 2; // == 12
	}
}
