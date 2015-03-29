using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class PersistentDataReader
{
	private BinaryReader m_InternalReader;
	private string m_Filename;
	private byte[] m_CurrHdrData;
	private int m_CurrHdrDataOffset;
	public PersistentDataReader(string filename)
	{
		m_Filename = filename;
		m_InternalReader = null;
		m_CurrHdrData = null;
		m_CurrHdrDataOffset = 0;

		SetInternalReader();
	}

	private BinaryReader InternalReader
	{
		get
		{
			if(m_InternalReader == null)
			{
				//one big fat read
				byte[] file = File.ReadAllBytes(m_Filename);
				m_InternalReader = new BinaryReader(new MemoryStream(file));
			}

			return m_InternalReader;
		}
	}

	private void SetInternalReader()
	{
		if(m_InternalReader == null)
		{
			//one big fat read
			byte[] file = File.ReadAllBytes(m_Filename);
			m_InternalReader = new BinaryReader(new MemoryStream(file));
		}
	}

	public byte[] CurrHdrData
	{
		set
		{
			m_CurrHdrData = value;
		}
	}

	public void ResetOffsets()
	{
		m_CurrHdrDataOffset = 0;
	}

	public PersistentDataFileHdr GetPersistentFileHdr()
	{
		PersistentDataFileHdr hdr = PersistentDataFileHdr.GetEmptyHdr();

		int id = InternalReader.ReadInt32 ();
		hdr.m_Id = (PersistentDataFileID)id;
		hdr.m_NumDataItemHdrs = InternalReader.ReadInt32 ();

		int chapterLen = InternalReader.ReadInt32 ();
		hdr.m_CurrChapter = new string(InternalReader.ReadChars (chapterLen));

		int checkpointLen = InternalReader.ReadInt32 ();
		hdr.m_Checkpoint = new string(InternalReader.ReadChars (checkpointLen));

		return hdr;
	}

	public List<PersistentDataItemHdr> GetPersistentDataHdrs()
	{
		return null;
	}

	public bool GetNextPersistentDataHdr(out PersistentDataItemHdr hdr)
	{
		bool success = true;

		hdr = PersistentDataItemHdr.GetEmptyHdr();

		if(InternalReader.PeekChar() < 0)//eof
			success = false;

		else
		{
			int intID = InternalReader.ReadInt32();
			hdr.m_PersistentItemID = (PersistentDataID) intID;
			hdr.m_NumItems = InternalReader.ReadInt32();
			hdr.m_SaveDataSize = InternalReader.ReadInt32();

			hdr.m_HdrData = InternalReader.ReadBytes(hdr.m_SaveDataSize);
		}

		return success;
	}
	public void Dispose()
	{
		if (InternalReader != null)
			InternalReader.Close();
	}

	public int ReadInt32()
	{
		//return InternalReader.ReadInt32 ();
		int x = BitConverter.ToInt32 (m_CurrHdrData, m_CurrHdrDataOffset);
		m_CurrHdrDataOffset += sizeof(int);

		return x;
	}
	
	public float ReadFloat()
	{
		//return InternalReader.ReadSingle ();
		float x = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset);
		m_CurrHdrDataOffset += sizeof(float);
		
		return x;
	}

	public bool ReadBool()
	{
		bool val = BitConverter.ToBoolean (m_CurrHdrData, m_CurrHdrDataOffset);
		m_CurrHdrDataOffset += sizeof(bool);

		return val;
	}

	public char ReadChar()
	{
		char c = Convert.ToChar (m_CurrHdrData [m_CurrHdrDataOffset]);
		m_CurrHdrDataOffset += 1; //explain later
		//return InternalReader.ReadChar ();
		return c;
	}
	
	public double ReadDouble()
	{
		double x = BitConverter.ToDouble (m_CurrHdrData, m_CurrHdrDataOffset);
		//return InternalReader.ReadDouble ();
		m_CurrHdrDataOffset += sizeof(double);
		return x;
	}

	public string ReadString()
	{
		//int length = InternalReader.ReadInt32();
		//char[] str = InternalReader.ReadChars (length);
		//return new string (str);

		int length = BitConverter.ToInt32 (m_CurrHdrData, m_CurrHdrDataOffset);
		m_CurrHdrDataOffset += sizeof(int);


		string str = Encoding.Default.GetString(m_CurrHdrData, m_CurrHdrDataOffset, length);
		m_CurrHdrDataOffset += str.Length;

		return str;
	}

	public Vector2 ReadVec2()
	{
		//return new Vector2(InternalReader.ReadSingle(), InternalReader.ReadSingle());
		float x = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset);
		float y = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float));
		m_CurrHdrDataOffset += sizeof(float) * 2;

		return new Vector2 (x, y);
	}

	public Vector3 ReadVec3()
	{
		//return new Vector3(InternalReader.ReadSingle(), InternalReader.ReadSingle(), InternalReader.ReadSingle());

		float x = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset);
		float y = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float));
		float z = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 2);

		m_CurrHdrDataOffset += sizeof(float) * 3;
		
		return new Vector3 (x, y, z);
	}

	public Vector4 ReadVec4()
	{
		//return new Vector4(InternalReader.ReadSingle(), InternalReader.ReadSingle(), InternalReader.ReadSingle(), InternalReader.ReadSingle());
		float x = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset);
		float y = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float));
		float z = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 2);
		float w = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 3);
		m_CurrHdrDataOffset += sizeof(float) * 4;
		
		return new Vector4 (x, y, z, w);
	}

	public Matrix4x4 ReadMat4x4()
	{
		//Matrix4x4 struct has no constructors
		Matrix4x4 m;

		m.m00 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset);
		m.m01 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float));
		m.m02 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 2);
		m.m03 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 3);
		m.m10 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 4);
		m.m11 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 5);
		m.m12 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 6);
		m.m13 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 7);
		m.m20 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 8);
		m.m21 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 9);
		m.m22 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 10);
		m.m23 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 11);
		m.m30 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 12);
		m.m31 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 13);
		m.m32 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 14);
		m.m33 = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 15);

		m_CurrHdrDataOffset += sizeof(float) * 16;
		return m;
	}

	public Quaternion ReadQuat()
	{
		//return new Quaternion(InternalReader.ReadSingle(), InternalReader.ReadSingle(), InternalReader.ReadSingle(), InternalReader.ReadSingle());
		float x = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset);
		float y = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float));
		float z = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 2);
		float w = BitConverter.ToSingle (m_CurrHdrData, m_CurrHdrDataOffset + sizeof(float) * 3);
		m_CurrHdrDataOffset += sizeof(float) * 4;
		
		return new Quaternion (x, y, z, w);
	}

	public byte[] ReadBytes()
	{
		int buffSize = BitConverter.ToInt32 (m_CurrHdrData, m_CurrHdrDataOffset);
		m_CurrHdrDataOffset += sizeof(int);

		byte[] subArray = new byte[buffSize];
		Array.Copy (m_CurrHdrData, subArray, buffSize);

		return subArray;
	}
}

