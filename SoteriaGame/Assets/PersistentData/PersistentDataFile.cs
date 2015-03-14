using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;

public class PersistentDataFile
{
	private List<PersistentDataHdr> m_DataHdrs;
	private DateTime m_FileCreated;
	private string m_FileName;
	private int m_FileSize;
	private int m_Id;

	public PersistentDataFile(DateTime timeCreated, string path, int filesize, int id)
	{
		m_DataHdrs = new List<PersistentDataHdr> ();
		m_FileCreated = timeCreated;
		m_FileName = path;
		m_FileSize = filesize;
		m_Id = id;
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

	public void Open()
	{
	}

	public void Dispose()
	{
	}

	public void GetPersitentDataReader(out PersistentDataReader reader)
	{
		reader = null;
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

	public static PersistentDataFile CreateNewPersistentDataFile(string path, string ext, int id)
	{
		StringBuilder builder = new StringBuilder ();

		//can think of a better name convention later
		builder.Append (path);
		builder.Append ("/Soteria_GameSave_" + id.ToString ());
		builder.Append (ext);

		FileStream newFile = File.Create (builder.ToString ());

		FileInfo info = new FileInfo (newFile.Name);

		newFile.Close ();
		return new PersistentDataFile (info.CreationTime, info.FullName, (int) info.Length, id);
	}

}

