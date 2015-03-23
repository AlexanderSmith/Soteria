using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public enum PersistentDataFileID
{
	E_SAVE_FILE_0,
	E_SAVE_FILE_1,
	E_SAVE_FILE_2
}

public class PersistentDataFile
{
	private List<PersistentDataHdr> m_DataHdrs;
	private DateTime m_FileCreated;
	private string m_FileName;
	private int m_FileSize;
	private int m_Id;

	public PersistentDataFile(DateTime timeCreated, string path, int filesize)
	{
		m_DataHdrs = new List<PersistentDataHdr> ();
		m_FileCreated = timeCreated;
		m_FileName = path;
		m_FileSize = filesize;
		m_Id = PrivGetID();
	}

	public int ID
	{
		get
		{
			return m_Id;
		}
	}


	public DateTime DateTimeCreated
	{
		get
		{
			return m_FileCreated;
		}
	}

	public string FileName
	{
		get
		{
			return m_FileName;
		}

		set
		{
			//one-time set
			if(m_FileName == null)
				m_FileName = value;
		}
	}

	private int PrivGetID()
	{
		return 0;
	}

	public void Open()
	{
	}

	public void Dispose()
	{
	}

	public void GetPersitentDataReader(out PersistentDataReader reader)
	{
		reader = null;
		FileStream loadFile = File.Open (this.FileName, FileMode.Open);

		if (loadFile != null)
		{
			reader = new PersistentDataReader();
			reader.InternalReader = new BinaryReader(loadFile);

			if(m_DataHdrs == null)
				m_DataHdrs = reader.GetPeristentDataHdrs();
		}
	}

	public void GetPersitentDataWriter(out PersistentDataWriter writer)
	{
		writer = null;

		FileStream saveFile = File.Open (this.FileName, FileMode.Open);
		
		if (saveFile != null) 
		{
			writer = new PersistentDataWriter();
			writer.InternalWriter = new BinaryWriter(saveFile);
		}
	}

	public void SaveSerializables(ISerializable[] serializables)
	{
		PersistentDataHdr hdr = PersistentDataHdr.GetEmptyHdr ();

		PersistentDataWriter writer;
		this.GetPersitentDataWriter (out writer);

		foreach (ISerializable s in serializables) 
		{
			writer.WritePersistentDataHdr(hdr, false);
			s.Serialize(writer);
			writer.SeekToLatestHdr();

			hdr.m_PersistentItemID = s.PersistentDataId;
			hdr.m_NumItems = writer.NumItemsWritten;
			hdr.m_SaveDataSize = writer.ItemTotalInBytes;
			writer.WritePersistentDataHdr(hdr, true);
		}
	}

	public void LoadSerializables(ISerializable[] serializables)
	{
	}

	public static PersistentDataFile CreateNewPersistentDataFile(string path, string ext)
	{
		StringBuilder builder = new StringBuilder ();

		//can think of a better name convention later
		builder.Append (path);
		builder.Append ("/Soteria_GameSave_");// + id.ToString ());
		builder.Append (ext);

		FileStream newFile = File.Create (builder.ToString ());

		FileInfo info = new FileInfo (newFile.Name);

		newFile.Close ();
		return new PersistentDataFile (info.CreationTime, info.FullName, (int) info.Length);
	}

}

