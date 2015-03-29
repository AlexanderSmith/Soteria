using UnityEngine;
using System.Collections;

public class ManagerTest : MonoBehaviour
{
	//private string m_PrevToolTip;
	// Use this for initialization

	private Dog m_Dog;
	private Cat m_Cat;
	private Fish m_Fish;

	private bool m_DataSaved;

	void Start ()
	{
		PersistentDataManager.Init();

		m_Dog = GetComponent<Dog>();
		m_Cat = GetComponent <Cat>();
		m_Fish = GetComponent <Fish>();
		m_DataSaved = false;
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
		GUI.Box (new Rect (10, 10, 200, 180), "Save/Load Menu");
		GUI.Box (new Rect (250, 10, 300, 180), "Current Selected File");

		Dog ();
		Cat ();
		Fish ();

		SetSaveLoadMenuLabels ();
		SetCurrSelectedFileLabels ();

		if(GUI.Button(new Rect(20, 40, 80, 20), "Save Slot 1"))
		{
			PersistentDataManager.SetCurrentSaveSlot(PersistentDataFileID.E_SAVE_FILE_0);
		}

		if(GUI.Button(new Rect(20, 70, 80, 20), "Save Slot 2"))
		{
			PersistentDataManager.SetCurrentSaveSlot(PersistentDataFileID.E_SAVE_FILE_1);
		}

		if(GUI.Button(new Rect(20, 100, 80, 20), "Save Slot 3"))
		{
			PersistentDataManager.SetCurrentSaveSlot(PersistentDataFileID.E_SAVE_FILE_2);
		}

		if(m_DataSaved)
			GUI.Label (new Rect(250, 100, 80, 30), "New Save Created!");

	}

	private void SetSaveLoadMenuLabels()
	{
		bool isEmptySlot0 = PersistentDataManager.IsSaveSlotEmpty (PersistentDataFileID.E_SAVE_FILE_0);
		bool isEmptySlot1 = PersistentDataManager.IsSaveSlotEmpty (PersistentDataFileID.E_SAVE_FILE_1);
		bool isEmptySlot2 = PersistentDataManager.IsSaveSlotEmpty (PersistentDataFileID.E_SAVE_FILE_2);

		GUI.Label (new Rect (120, 40, 80, 20), isEmptySlot0 ? "Empty" : "Loadable");
		GUI.Label (new Rect (120, 70, 80, 20), isEmptySlot1 ? "Empty" : "Loadable");
		GUI.Label (new Rect (120, 100, 80, 20), isEmptySlot2 ? "Empty" : "Loadable");
	}

	private void SetCurrSelectedFileLabels()
	{
		PersistentDataSaveSlot currSaveSlot = PersistentDataManager.GetCurrentSaveSlot ();

		if(currSaveSlot.SaveSlotID == PersistentDataFileID.E_SAVE_FILE_NA)
		{
			GUI.Label (new Rect (250, 30, 150, 20), "No File Selected!");
		}

		else if(!currSaveSlot.ExistingSaveFile)
		{
			GUI.Label (new Rect (250, 30, 300, 40), "No Data! Create New Save In This Slot?\n" + currSaveSlot.SaveSlotID.ToString()/*We can print something prettier later..*/ );

			if(GUI.Button(new Rect(250, 80, 80, 30), "Yes"))
			{
				PersistentDataManager.SetCurrentSaveSlot(currSaveSlot.SaveSlotID);
				PersistentDataManager.Save();
				m_DataSaved = true;

			}
		}

		else if(currSaveSlot.ExistingSaveFile)
		{
			GUI.Label (new Rect (250, 30, 300, 40), "Save File Found! Would You Like To Load?\n" + currSaveSlot.SaveSlotID.ToString()/*We can print something prettier later..*/ );
			
			if(GUI.Button(new Rect(250, 80, 80, 30), "Yes"))
			{
				PersistentDataManager.SetCurrentSaveSlot(currSaveSlot.SaveSlotID);
				PersistentDataManager.Load();
				//m_DataSaved = true;
				
			}
		}
	}

	private void Dog()
	{
		GUI.Box (new Rect (10, 350, 200, 180), "Dog Data");
		GUI.Label (new Rect (50, 400, 150, 50), m_Dog.ToString ());

	}

	private void Cat()
	{
		GUI.Box (new Rect (250, 350, 200, 180), "Cat Data");
		GUI.Label (new Rect (250, 400, 150, 50), m_Cat.ToString ());
	}

	private void Fish()
	{
		GUI.Box (new Rect (500, 350, 400, 180), "Fish Data");
		GUI.Label (new Rect (500, 400, 400, 180), m_Fish.ToString ());
	}

}

