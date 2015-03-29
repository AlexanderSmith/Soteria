using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PersistentDataSaveSlot
{
	private PersistentDataFileID m_Id;
	private bool m_bFileExists;

	public PersistentDataSaveSlot(PersistentDataFileID id, bool fileExists)
	{
		m_Id = id;
		m_bFileExists = fileExists;
	}

	public PersistentDataFileID SaveSlotID
	{
		get
		{
			return m_Id;
		}
	}

	public bool ExistingSaveFile
	{
		get
		{
			return m_bFileExists;
		}
	}

}

/*
 * class PersistentDataManager <Singleton>
 * 
 * Users of this game's save/load system will primarily work with
 * this class.
 * 
 * Real work is done in the public static Save/(upcoming)Load functions
 */

public class PersistentDataManager : Singleton<PersistentDataManager>
{
	//Application.persistentDataPath is platform dependent.
	private static readonly string PERSISTENTDATA_PATH = Application.persistentDataPath;

	//the extension I'm giving the save files, subject to change
	private static readonly string PERSISTENTDATA_EXT = ".dat";

	//number of "save slots," subject to change
	private static readonly int PERSISTENTDATA_MAX_FILE_COUNT = 3;

	private Dictionary<PersistentDataFileID, PersistentDataFile> m_CurrSaveFiles;
	private List<ISerializable> m_Serializables;
	private PersistentDataFileID m_SaveFileSlotInUse; 

	private void Awake()
	{
		m_CurrSaveFiles = this.PrivGetSaveFiles ();
		m_Serializables = new List<ISerializable> ();
		m_SaveFileSlotInUse = PersistentDataFileID.E_SAVE_FILE_NA;
	}

	private Dictionary<PersistentDataFileID, PersistentDataFile> PrivGetSaveFiles()
	{
		//this function will return a new list regardless,
		//even if its Count == 0, I don't want to deal will nulls
		//and we'll have to initialize one down the road regardless

		Dictionary<PersistentDataFileID, PersistentDataFile> fileList = new Dictionary<PersistentDataFileID, PersistentDataFile>();

		fileList [PersistentDataFileID.E_SAVE_FILE_0] = null;
		fileList [PersistentDataFileID.E_SAVE_FILE_1] = null;
		fileList [PersistentDataFileID.E_SAVE_FILE_2] = null;

		DirectoryInfo info = new DirectoryInfo (PERSISTENTDATA_PATH);

		FileInfo[] files = info.GetFiles();


		if (files.Length > 0) 
		{
			PersistentDataFile newFile = null;
			foreach (FileInfo f in files)
			{
				//until there's a real naming convention
				//in place for save files, this works for now
				if(Path.GetExtension(f.FullName) == PersistentDataManager.PERSISTENTDATA_EXT)
				{
					newFile = new PersistentDataFile(f.CreationTime, f.FullName, (int)f.Length);

					if(newFile.ID == PersistentDataFileID.E_SAVE_FILE_NA)
					{
						//handle..
					}

					else
					{
						fileList[newFile.ID] = newFile;
					}
				}
			}		
		}
		return fileList;
	}

	/*
	 * Init
	 * 
	 * Due to how the these Singleton's initialize
	 * and attach themselves to GameObjects automatically,
	 * These objects won't actually come alive (call Awake/Start)
	 * until the Instance property has been reference at least once.
	 * 
	 * Eventually Init will actually do something useful, but right
	 * now just calling Instance to get the thing to work is enough
	 */
	public static void Init()
	{
		//tell the script to wake the fuck up
		PersistentDataManager man = Instance;
	}

	/*
	 * IsSaveSlotEmpty
	 * 
	 * PersistentDataFileID id
	 * 
	 * Returns true if "save slot" corrsponding to passed in ID
	 * already holds a save file, false otherwise
	 * 
	 */
	public static bool IsSaveSlotEmpty(PersistentDataFileID id)
	{
		bool success = Instance.m_CurrSaveFiles [id] == null ? true : false;

		return success;
	}

	/*
	 * GetCurrentSaveSlot
	 * 
	 * Returns enum of currently selected save slot if there is one
	 * otherwise, return NA
	 * 
	 */
	public static PersistentDataSaveSlot GetCurrentSaveSlot()
	{
		bool fileExists = false;

		if(Instance.m_SaveFileSlotInUse != PersistentDataFileID.E_SAVE_FILE_NA)
		{
			if(Instance.m_CurrSaveFiles[Instance.m_SaveFileSlotInUse] != null)
				fileExists = true;
		}

		return new PersistentDataSaveSlot(Instance.m_SaveFileSlotInUse, fileExists);
	}

	/*
	 * SetCurrentSaveFile
	 * 
	 * Allows someone to set the current save file
	 * 
	 * No params yet as it's still to early in the game
	 * 
	 * Eventually it will be some cool function
	 * to set the current save file being played.
	 * Maybe some info passed on a gui event callback?
	 */
	public static void SetCurrentSaveSlot(PersistentDataFileID id)
	{
		if (id == PersistentDataFileID.E_SAVE_FILE_NA)
			Logger.LogError ("Save File Errors", 
			                 "Persistent Data File with ID " + PersistentDataFileID.E_SAVE_FILE_NA.ToString () + 
			                 "! Not a valid arugment!", false);

		Instance.m_SaveFileSlotInUse = id;
	}

	public static void Load()
	{
		if (Instance.m_SaveFileSlotInUse == PersistentDataFileID.E_SAVE_FILE_NA)
			Logger.LogError ("Save File Errors", 
			                 "Persistent Data File with ID " + PersistentDataFileID.E_SAVE_FILE_NA.ToString () + 
			                 "! Not a valid arugment!", false);

		Dictionary<PersistentDataFileID, PersistentDataFile> files = Instance.m_CurrSaveFiles;

		PersistentDataFile loadFile = files [Instance.m_SaveFileSlotInUse];

		if (loadFile == null)
			Logger.LogError ("Save File Errors", 
			                 "Persistent Data File with ID " + Instance.m_SaveFileSlotInUse.ToString () + " not found at " + 
			                 PERSISTENTDATA_PATH, false);

		else
		{
			//PersistentDataReader reader;
			//loadFile.GetPersitentDataReader(out reader);
			ISerializable[] serializables = (ISerializable[]) FindObjectsOfType(typeof(ISerializable));

			loadFile.Load(serializables);
			loadFile.Dispose();
		}
	}

	/*
	 * Save
	 * 
	 * This is the main save function.  If the current save data reference
	 * is null (you started a new game) then it will create a new save data.
	 * Otherwise, this will overwrite the save data for the save you're playing on.
	 * 
	 * Save goes through and retrieves all objects that inherited the ISerializable abstract class
	 * (Retrieval is a slow function, but it's not like we're saving often
	 * so who cares).
	 * 
	 * It will then loop through the ISerialable array and call Serialize() (user defines what they wanna save)
	 * on each.
	 */
	public static void Save()
	{
		PersistentDataFile saveFile = null;

		PersistentDataManager pDataMan = Instance;

		//empty save slot?
		if (pDataMan.m_CurrSaveFiles[pDataMan.m_SaveFileSlotInUse]== null) 
		{
			saveFile = PersistentDataFile.CreateNewPersistentDataFile (PERSISTENTDATA_PATH, PERSISTENTDATA_EXT, pDataMan.m_SaveFileSlotInUse);
			pDataMan.m_CurrSaveFiles[pDataMan.m_SaveFileSlotInUse] = saveFile;
			//Instance.m_CurrSaveFiles.Add (saveFile);
			//Instance.m_SaveFileInUse = saveFile;
		} 
		else 
		{
			saveFile = pDataMan.m_CurrSaveFiles[pDataMan.m_SaveFileSlotInUse];
		}

		//PersistentDataWriter writer;

//		saveFile.GetPersitentDataWriter (out writer);


		ISerializable[] serializables = (ISerializable[]) FindObjectsOfType(typeof(ISerializable));
		saveFile.Save(serializables, "Chapter 1", "Checkpoint 1");
		saveFile.Dispose ();
	}

	protected override void VOnDestroy()
	{
		//currently here for quick dirty testing
		//PersistentDataManager.Save ();
	}
}

