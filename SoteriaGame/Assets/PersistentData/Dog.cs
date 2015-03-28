using UnityEngine;
using System.Collections;

public class Dog : ISerializable
{
	public string Name;
	public int m_Tag;
	public bool m_IsBarking;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	public override PersistentDataID PersistentDataId
	{
		get
		{
			return PersistentDataID.E_DOG;
		}
	}
	
	public override void Serialize(PersistentDataWriter writer)
	{
		writer.Write (Name);
		writer.Write (m_Tag);
		writer.Write (m_IsBarking);
	}
	
	public override void Deserialize(PersistentDataReader reader)
	{
		
	}
	public override string ToString ()
	{
		return "Name: " + Name + "\n" +
			   "Tag: " + m_Tag.ToString () + "\n" +
			   "Barking: " + m_IsBarking.ToString ();

	}
}

