using UnityEngine;
using System;
using System.Text;
using System.IO;

/*
 * Probably going to convert this to byte[] buffer (probably 1K)
 * to so we can do one single read/write flush
 */

public class PersistentDataWriter
{
	//left operand = # of KBs
	private const int MAX_BUFFER_SIZE = 2 * 1024;
	
	private BinaryWriter m_InternalWriter;
	//private Byte[] m_Buffer;
	private int m_NumItemsWritten;
	private int m_ItemTotalInBytes;
	private int m_BufferPosition;
	public PersistentDataWriter()
	{
		m_InternalWriter = null;
		//m_Buffer = null;
		m_NumItemsWritten = 0;
		m_ItemTotalInBytes = 0;
		m_BufferPosition = 0;
	}

	private BinaryWriter InternalWriter
	{
		get
		{
			//one-time set
			if(m_InternalWriter == null)
			{
				m_InternalWriter = new BinaryWriter(
								   new MemoryStream(new byte[MAX_BUFFER_SIZE]));
			}

			return m_InternalWriter;
		}
	}

	public int NumItemsWritten
	{
		get
		{
			return m_NumItemsWritten;
		}
	}

	public int ItemTotalInBytes
	{
		get
		{
			return m_ItemTotalInBytes;
		}
	}

	public byte[] AsArray()
	{
		MemoryStream byteStream = InternalWriter.BaseStream as MemoryStream;

		return byteStream.ToArray ();
	}
	public void SeekToLatestHdr()
	{
		int sizeofHdr = PersistentDataItemHdr.GetSizeOfHdr ();
		m_InternalWriter.Seek (-1 * (m_ItemTotalInBytes + sizeofHdr), SeekOrigin.Current);
	}

	private void Reset()
	{
		//int sizeofHdr = PersistentDataItemHdr.GetSizeOfHdr ();
		m_InternalWriter.Seek ((m_ItemTotalInBytes), SeekOrigin.Current);

		m_BufferPosition += m_ItemTotalInBytes;
		m_NumItemsWritten = 0;
		m_ItemTotalInBytes = 0;
	}

	public void Dipose()
	{
		if (InternalWriter != null)
		{

			InternalWriter.Close ();
		}
	}

	public void WritePersistentDataFileHdr(PersistentDataFileHdr hdr, bool reset)
	{
		this.Write (hdr.m_CurrChapter);
		this.Write (hdr.m_Checkpoint);
		this.Write ((int)hdr.m_Id);

		//if(reset)
			//this.Reset();

		m_ItemTotalInBytes = 0;
		m_NumItemsWritten = 0;
	}

	public void WritePersistentDataHdr(PersistentDataItemHdr hdr, bool reset)
	{
		InternalWriter.Write((int)hdr.m_PersistentItemID);
		InternalWriter.Write(hdr.m_NumItems);
		InternalWriter.Write(hdr.m_SaveDataSize);

		if(reset)
		{
			this.Reset();
		}

		else
		{
			m_ItemTotalInBytes = 0;
			m_NumItemsWritten = 0;
		}
	}

	public void Write(int x)
	{
		InternalWriter.Write (x);
		m_ItemTotalInBytes += sizeof(int);

		++m_NumItemsWritten;
	}

	public void Write(float x)
	{
		InternalWriter.Write (x);
		m_ItemTotalInBytes += sizeof(float);

		++m_NumItemsWritten;
	}

	public void Write(bool b)
	{
		InternalWriter.Write (b);
		m_ItemTotalInBytes += sizeof(bool);
		++m_NumItemsWritten;
	}

	public void Write(char c)
	{
		InternalWriter.Write (c);
		m_ItemTotalInBytes += sizeof(char);
		++m_NumItemsWritten;
	}

	public void Write(double x)
	{
		InternalWriter.Write (x);
		m_ItemTotalInBytes += sizeof(double);

		++m_NumItemsWritten;
	}

	/*
	 * TODO
	 * Number of bytes written when serializing
	 * a string is still to be determined
	 * 
	 * According the length of the string is prefixed
	 * along with the actual string, but I'm still not
	 * a 100% on what that means in memory
	 */
	public void Write(string s)
	{
		UTF8Encoding utf8 = new UTF8Encoding();

		byte[] strBytes = utf8.GetBytes (s);

		InternalWriter.Write (strBytes);

		++m_NumItemsWritten;
		m_ItemTotalInBytes += strBytes.Length;
	}

	public void Write(Vector2 v)
	{
		InternalWriter.Write (v.x);
		InternalWriter.Write (v.y);
		m_ItemTotalInBytes += sizeof(float) * 2;

		++m_NumItemsWritten;
	}

	public void Write(Vector3 v)
	{
		InternalWriter.Write (v.x);
		InternalWriter.Write (v.y);
		InternalWriter.Write (v.z);

		m_ItemTotalInBytes += sizeof(float) * 3;

		++m_NumItemsWritten;
	}

	public void Write(Vector4 v)
	{
		InternalWriter.Write (v.x);
		InternalWriter.Write (v.y);
		InternalWriter.Write (v.z);
		InternalWriter.Write (v.w);

		m_ItemTotalInBytes += sizeof(float) * 4;

		++m_NumItemsWritten;
	}

	//fuck you..
	public void Write(Matrix4x4 m)
	{
		InternalWriter.Write (m.m00);
		InternalWriter.Write (m.m01);
		InternalWriter.Write (m.m02);
		InternalWriter.Write (m.m03);
		InternalWriter.Write (m.m10);
		InternalWriter.Write (m.m11);
		InternalWriter.Write (m.m12);
		InternalWriter.Write (m.m13);
		InternalWriter.Write (m.m20);
		InternalWriter.Write (m.m21);
		InternalWriter.Write (m.m22);
		InternalWriter.Write (m.m23);
		InternalWriter.Write (m.m30);
		InternalWriter.Write (m.m31);
		InternalWriter.Write (m.m32);
		InternalWriter.Write (m.m33);

		m_ItemTotalInBytes += sizeof(float) * 16;

		++m_NumItemsWritten;
	}

	public void Write(Quaternion q)
	{
		InternalWriter.Write (q.x);
		InternalWriter.Write (q.y);
		InternalWriter.Write (q.z);
		InternalWriter.Write (q.w);

		m_ItemTotalInBytes += sizeof(float) * 4;

		++m_NumItemsWritten;
	}

	public void Write(byte[] b)
	{
		InternalWriter.Write (b);

		m_ItemTotalInBytes += b.Length;

		++m_NumItemsWritten;
	}

}

