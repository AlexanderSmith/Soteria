using UnityEngine;
using System.Collections;

public class Cat : ISerializable
{

	public double d;
	public float f;
	public char c;

		// Use this for initialization
		void Start ()
		{
			//apparently can't access chars in the inspector
			c = 'c';
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

	public override PersistentDataID PersistentDataId
	{
		get
		{
			return PersistentDataID.E_CAT;
		}
	}
	
	public override void Serialize(PersistentDataWriter writer)
	{
		writer.Write (d);
		writer.Write (f);
		writer.Write (c);
	}
	
	public override void Deserialize(PersistentDataReader reader)
	{
		
	}

	public override string ToString ()
	{
		return "d: " + d.ToString() + "\n" +
			   "f: " + f.ToString () + "\n" +
			   "c: " + c.ToString ();
		
	}
}

