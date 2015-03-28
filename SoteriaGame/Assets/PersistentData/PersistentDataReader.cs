using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class PersistentDataReader
{
	private BinaryReader m_InternalReader;


	public PersistentDataReader()
	{
		m_InternalReader = null;
	}

	public BinaryReader InternalReader
	{
		set
		{
			if(m_InternalReader == null)
				m_InternalReader = value;
		}
	}

	public List<PersistentDataItemHdr> GetPeristentDataHdrs()
	{
		return null;
	}

	public void Dispose()
	{
		if (m_InternalReader != null)
			m_InternalReader.Close();
	}

	public int ReadInt32()
	{
		return m_InternalReader.ReadInt32 ();
	}
	
	public float ReadFloat()
	{
		return m_InternalReader.ReadSingle ();
	}

	public bool ReadBool()
	{
		return m_InternalReader.ReadBoolean ();
	}

	public char ReadChar()
	{
		return m_InternalReader.ReadChar ();
	}
	
	public double ReadDouble()
	{
		return m_InternalReader.ReadDouble ();
	}

	public string ReadString()
	{
		return m_InternalReader.ReadString ();
	}

	public Vector2 ReadVec2()
	{
		return new Vector2(m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle());
	}

	public Vector3 ReadVec3()
	{
		return new Vector3(m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle());
	}

	public Vector4 ReadVec4()
	{
		return new Vector4(m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle());
	}

	public Matrix4x4 ReadMat4x4()
	{
		//Matrix4x4 struct has no constructors
		Matrix4x4 m;

		m.m00 = m_InternalReader.ReadSingle();
		m.m01 = m_InternalReader.ReadSingle();
		m.m02 = m_InternalReader.ReadSingle();
		m.m03 = m_InternalReader.ReadSingle();
		m.m10 = m_InternalReader.ReadSingle();
		m.m11 = m_InternalReader.ReadSingle();
		m.m12 = m_InternalReader.ReadSingle();
		m.m13 = m_InternalReader.ReadSingle();
		m.m20 = m_InternalReader.ReadSingle();
		m.m21 = m_InternalReader.ReadSingle();
		m.m22 = m_InternalReader.ReadSingle();
		m.m23 = m_InternalReader.ReadSingle();
		m.m30 = m_InternalReader.ReadSingle();
		m.m31 = m_InternalReader.ReadSingle();
		m.m32 = m_InternalReader.ReadSingle();
		m.m33 = m_InternalReader.ReadSingle();

		return m;
	}

	public Quaternion ReadQuat()
	{
		return new Quaternion(m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle(), m_InternalReader.ReadSingle());
	}

	public byte[] ReadBytes()
	{
		int buffSize = m_InternalReader.ReadInt32();
		return m_InternalReader.ReadBytes (buffSize);
	}
}

