using UnityEngine;
using System.Collections;

/*
 * struct PersistentDataHdr
 * 
 * Quick struct to represent header for each object serialized
 * in the data file.  The serialized PersistentDataID is used to
 * identify where a particular objects data starts/begins
 */
public struct PersistentDataHdr
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
	public static PersistentDataHdr GetEmptyHdr()
	{
		PersistentDataHdr hdr;

		hdr.m_PersistentItemID = PersistentDataID.E_MISSING_ID;
		hdr.m_NumItems = 0;
		hdr.m_SaveDataSize = 0;

		return hdr;
	}

	/*
	 * Because of how C# handles alignment, can't call sizeof on a struct,
	 * so I just do this
	 */
	public static int GetSizeOfHdr()
	{
		return sizeof(PersistentDataID) + sizeof(int) * 2; // == 12
	}
}
