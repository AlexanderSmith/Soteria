using UnityEngine;
using System;
using System.IO;

/*
 * Probably going to convert this to byte[] buffer (probably 1K)
 * to so we can do one single read/write flush
 */

public class PersistentDataWriter
{
	private BinaryWriter m_InternalWriter;
	private int m_NumItemsWritten;
	private int m_ItemTotalInBytes;

	public PersistentDataWriter()
	{
		m_InternalWriter = null;
		m_NumItemsWritten = 0;
		m_ItemTotalInBytes = 0;
	}

	public BinaryWriter InternalWriter
	{
		set
		{
			//one-time set
			if(m_InternalWriter == null)
				m_InternalWriter = value;
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

	public void SeekToLatestHdr()
	{
		int sizeofHdr = PersistentDataHdr.GetSizeOfHdr ();
		m_InternalWriter.Seek (-1 * (m_ItemTotalInBytes + sizeofHdr), SeekOrigin.Current);
	}

	private void Reset()
	{
		int sizeofHdr = PersistentDataHdr.GetSizeOfHdr ();
		m_InternalWriter.Seek ((m_ItemTotalInBytes + sizeofHdr), SeekOrigin.Current);

		m_NumItemsWritten = 0;
		m_ItemTotalInBytes = 0;
	}

	public void Dipose()
	{
		if (m_InternalWriter != null)
			m_InternalWriter.Close ();
	}

	public void WritePersistentDataHdr(PersistentDataHdr hdr, bool reset)
	{
		m_InternalWriter.Write((int) hdr.m_PersistentItemID);
		m_InternalWriter.Write (hdr.m_NumItems);
		m_InternalWriter.Write (hdr.m_SaveDataSize);

		if(reset)
			this.Reset();
	}

	public void Write(int x)
	{
		m_InternalWriter.Write (x);
		m_ItemTotalInBytes += sizeof(int);

		++m_NumItemsWritten;
	}

	public void Write(float x)
	{
		m_InternalWriter.Write (x);
		m_ItemTotalInBytes += sizeof(float);

		++m_NumItemsWritten;
	}

	public void Write(double x)
	{
		m_InternalWriter.Write (x);
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
		m_InternalWriter.Write (s);
		//m_ItemTotalInBytes += ??

		++m_NumItemsWritten;
	}

	public void Write(Vector2 v)
	{
		m_InternalWriter.Write (v.x);
		m_InternalWriter.Write (v.y);
		m_ItemTotalInBytes += sizeof(float) * 2;

		++m_NumItemsWritten;
	}

	public void Write(Vector3 v)
	{
		m_InternalWriter.Write (v.x);
		m_InternalWriter.Write (v.y);
		m_InternalWriter.Write (v.z);

		m_ItemTotalInBytes += sizeof(float) * 3;

		++m_NumItemsWritten;
	}

	public void Write(Vector4 v)
	{
		m_InternalWriter.Write (v.x);
		m_InternalWriter.Write (v.y);
		m_InternalWriter.Write (v.z);
		m_InternalWriter.Write (v.w);

		m_ItemTotalInBytes += sizeof(float) * 4;

		++m_NumItemsWritten;
	}

	//fuck you..
	public void Write(Matrix4x4 m)
	{
		m_InternalWriter.Write (m.m00);
		m_InternalWriter.Write (m.m01);
		m_InternalWriter.Write (m.m02);
		m_InternalWriter.Write (m.m03);
		m_InternalWriter.Write (m.m10);
		m_InternalWriter.Write (m.m11);
		m_InternalWriter.Write (m.m12);
		m_InternalWriter.Write (m.m13);
		m_InternalWriter.Write (m.m20);
		m_InternalWriter.Write (m.m21);
		m_InternalWriter.Write (m.m22);
		m_InternalWriter.Write (m.m23);
		m_InternalWriter.Write (m.m30);
		m_InternalWriter.Write (m.m31);
		m_InternalWriter.Write (m.m32);
		m_InternalWriter.Write (m.m33);

		m_ItemTotalInBytes += sizeof(float) * 16;

		++m_NumItemsWritten;
	}

	public void Write(Quaternion q)
	{
		m_InternalWriter.Write (q.x);
		m_InternalWriter.Write (q.y);
		m_InternalWriter.Write (q.z);
		m_InternalWriter.Write (q.w);

		m_ItemTotalInBytes += sizeof(float) * 4;

		++m_NumItemsWritten;
	}

	public void Write(byte[] b)
	{
		m_InternalWriter.Write (b);

		m_ItemTotalInBytes += b.Length;

		++m_NumItemsWritten;
	}

}

