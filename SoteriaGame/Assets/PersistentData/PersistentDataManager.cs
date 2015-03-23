using UnityEngine;
using System.Collections.Generic;
using System.IO;

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

	private List<PersistentDataFile> m_CurrSaveFiles;
	private List<ISerializable> m_Serializables;
	private PersistentDataFile m_SaveFileInUse; 

	private void Awake()
	{
		m_CurrSaveFiles = this.PrivGetSaveFiles ();
		m_Serializables = new List<ISerializable> ();
		m_SaveFileInUse = null;
	}

	private List<PersistentDataFile> PrivGetSaveFiles()
	{
		//this function will return a new list regardless,
		//even if its Count == 0, I don't want to deal will nulls
		//and we'll have to initialize one down the road regardless

		List<PersistentDataFile> fileList = new List<PersistentDataFile>();

		DirectoryInfo info = new DirectoryInfo (PERSISTENTDATA_PATH);

		FileInfo[] files = info.GetFiles();


		if (files.Length > 0) 
		{
			foreach (FileInfo f in files)
			{
				//until there's a real naming convention
				//in place for save files, this works for now
				if(Path.GetExtension(f.FullName) == PersistentDataManager.PERSISTENTDATA_EXT)
				{
					fileList.Add(new PersistentDataFile(f.CreationTime, f.FullName, (int)f.Length));
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
	public static void SetCurrentSaveFile()
	{
		List<PersistentDataFile> files = Instance.m_CurrSaveFiles;

		if (files.Count > 0)
			Instance.m_SaveFileInUse = files [0];
	}

	public static void Load(int id)
	{
		if (id >= PERSISTENTDATA_MAX_FILE_COUNT)
			Logger.LogError ("Save File Errors", 
			                 "Persistent Data File with ID " + id.ToString () + 
			                 "! Should not have ID greater than " + 
			                 PERSISTENTDATA_MAX_FILE_COUNT.ToString () + "!", false);

		List<PersistentDataFile> files = Instance.m_CurrSaveFiles;

		PersistentDataFile loadFile = files.Find (file => file.ID == id);

		if (loadFile == null)
			Logger.LogError ("Save File Errors", 
			                 "Persistent Data File with ID " + id.ToString () + " not found at " + 
			                 PERSISTENTDATA_PATH, false);

		else
		{
			PersistentDataReader reader;
			loadFile.GetPersitentDataReader(out reader);
			ISerializable[] serializables = (ISerializable[]) FindObjectsOfType(typeof(ISerializable));

			foreach(ISerializable s in serializables)
			{
				s.Deserialize(reader);
			}

			reader.Dispose();
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

		//new game?
		if (Instance.m_SaveFileInUse == null) 
		{
			saveFile = PersistentDataFile.CreateNewPersistentDataFile (PERSISTENTDATA_PATH, PERSISTENTDATA_EXT);
			Instance.m_CurrSaveFiles.Add (saveFile);
			Instance.m_SaveFileInUse = saveFile;
		} 
		else 
		{
			saveFile = Instance.m_SaveFileInUse;
		}

		PersistentDataWriter writer;

		saveFile.GetPersitentDataWriter (out writer);


		ISerializable[] serializables = (ISerializable[]) FindObjectsOfType(typeof(ISerializable));
		foreach (ISerializable s in serializables) 
		{
			s.Serialize(writer);
		}

		writer.Dipose ();
		saveFile.Dispose ();
	}

	protected override void VOnDestroy()
	{
		//currently here for quick dirty testing
		//PersistentDataManager.Save ();
	}
}

