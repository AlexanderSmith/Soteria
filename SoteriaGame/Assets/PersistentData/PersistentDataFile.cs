using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public enum PersistentDataFileID
{
	E_SAVE_FILE_0,
	E_SAVE_FILE_1,
	E_SAVE_FILE_2,

	E_SAVE_FILE_NA = -1
}

public class PersistentDataFile
{
	private List<PersistentDataItemHdr> m_DataHdrs;
	private PersistentDataFileHdr m_FileHdr;
	private PersistentDataFileID m_Id;
	private DateTime m_LastModified;
	private string m_FileName;
	private int m_FileSize;

	public PersistentDataFile(DateTime timeCreated, string path, int filesize)
	{
		m_DataHdrs = new List<PersistentDataItemHdr> ();
		m_LastModified = timeCreated;
		m_FileName = path;
		m_FileSize = filesize;

		m_FileHdr = PrivGetFileHdr ();
	}

	public PersistentDataFileID ID
	{
		get
		{
			return m_FileHdr.m_Id;
		}
	}


	public DateTime LastModified
	{
		get
		{
			return m_LastModified;
		}
	}

	public string FileName
	{
		get
		{
			return m_FileName;
		}
	}

	private PersistentDataFileHdr PrivGetFileHdr()
	{
		PersistentDataFileHdr hdr = PersistentDataFileHdr.GetEmptyHdr ();

		FileInfo fileInfo = new FileInfo (this.FileName);

		if(fileInfo.Length > 0)
		{
			PersistentDataReader reader = new PersistentDataReader(this.FileName);
			//reader.InternalReader = new BinaryReader(file);
			
			//temporary is for debugging purposes
			//Monodevelop on my machine won't let me view enum
			//values in the debugger for some stupid reason
			
			//int intID = reader.ReadInt32();
			//id = (PersistentDataFileID) intID;
			hdr = reader.GetPersistentFileHdr();
			reader.Dispose();
		}
		
		else
		{
			PersistentDataSaveSlot currSaveSlot = PersistentDataManager.GetCurrentSaveSlot();
			hdr.m_Id = currSaveSlot.SaveSlotID;
		}

		return hdr;
	}

	private PersistentDataFileID PrivGetID()
	{
		FileInfo fileInfo = new FileInfo (this.FileName);
		PersistentDataFileID id = PersistentDataFileID.E_SAVE_FILE_NA;

		if(fileInfo.Length > 0)
		{
			PersistentDataReader reader = new PersistentDataReader(this.FileName);
			//reader.InternalReader = new BinaryReader(file);

			//temporary is for debugging purposes
			//Monodevelop on my machine won't let me view enum
			//values in the debugger for some stupid reason

			//int intID = reader.ReadInt32();
			//id = (PersistentDataFileID) intID;
			this.m_FileHdr = reader.GetPersistentFileHdr();
			reader.Dispose();
		}

		else
		{
			PersistentDataSaveSlot currSaveSlot = PersistentDataManager.GetCurrentSaveSlot();
			id = currSaveSlot.SaveSlotID;
		}

		return id;
	}

	private void PrivWriteToDisk(PersistentDataWriter writer)
	{	
		FileStream saveFile = File.Open (this.FileName, FileMode.Truncate);

		using (saveFile)
		{
			byte[] buffer = writer.AsArray();
			saveFile.Write (buffer, 0, buffer.Length);
		}
	}

	public void Dispose()
	{
	}

	public void GetPersistentDataReader(out PersistentDataReader reader)
	{

		reader = new PersistentDataReader(this.FileName);
		//if(m_DataHdrs == null)
			//m_DataHdrs = reader.GetPersistentDataHdrs();

	}

	public void GetPersistentDataWriter(out PersistentDataWriter writer)
	{
		writer = null;

		FileStream saveFile = File.Open (this.FileName, FileMode.Open);

		using (saveFile)
		{
			if (saveFile != null) 
			{
				writer = new PersistentDataWriter();
				//writer.InternalWriter = new BinaryWriter(saveFile);
			}
		}
	}

	public void Save(ISerializable[] serializables, string chapter, string checkpoint)
	{
		//data header for entire file
		PersistentDataFileHdr fileHdr = PersistentDataFileHdr.GetEmptyHdr ();
		fileHdr.m_CurrChapter = chapter;
		fileHdr.m_Checkpoint = checkpoint;
		fileHdr.m_NumDataItemHdrs = serializables.Length;
		fileHdr.m_Id = this.ID;

		//data header for each individual serializable
		PersistentDataItemHdr dataItemHdr = PersistentDataItemHdr.GetEmptyHdr ();

		PersistentDataWriter writer;
		this.GetPersistentDataWriter (out writer);

		writer.WritePersistentDataFileHdr (fileHdr, true);

		foreach (ISerializable s in serializables) 
		{
			writer.WritePersistentDataHdr(dataItemHdr, false);
			s.Serialize(writer);
			writer.SeekToLatestHdr();

			dataItemHdr.m_PersistentItemID = s.PersistentDataId;
			dataItemHdr.m_NumItems = writer.NumItemsWritten;
			dataItemHdr.m_SaveDataSize = writer.ItemTotalInBytes;
			writer.WritePersistentDataHdr(dataItemHdr, true);
		}

		PrivWriteToDisk(writer);
	}

	public void Load(ISerializable[] serializables)
	{
		PersistentDataReader reader;
		this.GetPersistentDataReader (out reader);

		this.m_FileHdr = reader.GetPersistentFileHdr ();

		PersistentDataItemHdr dataItemHdr;


		for(int i = 0; i < this.m_FileHdr.m_NumDataItemHdrs && reader.GetNextPersistentDataHdr(out dataItemHdr); ++i)
		{
			m_DataHdrs.Add(dataItemHdr);
		}

		foreach (PersistentDataItemHdr hdr in m_DataHdrs)
		{
			reader.CurrHdrData = hdr.m_HdrData;

			ISerializable[] matches = Array.FindAll<ISerializable>(serializables,s => s.PersistentDataId == hdr.m_PersistentItemID);

			foreach(ISerializable s in matches)
			{
				s.Deserialize(reader);
				reader.ResetOffsets();
			}
		}

		reader.Dispose ();
	}

	public static PersistentDataFile CreateNewPersistentDataFile(string path, string ext, PersistentDataFileID id)
	{
		StringBuilder builder = new StringBuilder ();

		//can think of a better name convention later
		builder.Append (path);
		builder.Append ("/Soteria_GameSave_");
		builder.Append (id.ToString ());
		builder.Append (ext);

		FileStream newFile = File.Create (builder.ToString ());

		FileInfo info = new FileInfo (newFile.Name);

		newFile.Close ();
		return new PersistentDataFile (info.CreationTime, info.FullName, (int) info.Length);
	}

}

