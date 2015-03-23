using UnityEngine;
using System.Collections;

public class CoolPlayerDataTest : ISerializable
{
	public Vector3 m_Position;
	public string m_NameStr;
	public int m_ID;

	public override PersistentDataID PersistentDataId
	{
		get
		{
			return PersistentDataID.E_COOL_PLAYER_DATA;
		}
	}

	public override void Serialize(PersistentDataWriter writer)
	{
		writer.Write (m_Position);
		writer.Write (m_NameStr);
		writer.Write (m_ID);
	}

	public override void Deserialize(PersistentDataReader reader)
	{

	}

	public override string ToString ()
	{
		return "CoolPlayerDataTest:\n" +
			   "\tPosition: " + m_Position.ToString () + "\n" +
			   "\tName: " + m_NameStr + "\n" +
			   "\tID: " + m_ID.ToString() + "\n";
	}
}

