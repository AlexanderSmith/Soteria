using UnityEngine;
using System.Collections;

public class Fish : ISerializable
{
	public Matrix4x4 m;
	public Vector4 v;
	public Quaternion q;

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
			return PersistentDataID.E_FISH;
		}
	}
	
	public override void Serialize(PersistentDataWriter writer)
	{
		writer.Write (m);
		writer.Write (v);
		writer.Write (q);
	}
	
	public override void Deserialize(PersistentDataReader reader)
	{
		
	}

	public override string ToString ()
	{
		return "m: " + m.ToString() + "\n" +
			   "v: " + v.ToString () + "\n" +
			   "q: " + q.ToString ();
		
	}
}

