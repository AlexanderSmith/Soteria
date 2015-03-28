using UnityEngine;
using System.Collections;

/*
 * ISerializable IS-A MonoBehaviour
 * 
 * If you wish for you class to be serializable,
 * have it derive from ISerializable, trying to inherit
 * this and MonoBehviour is a compilation error
 */
public abstract class ISerializable : MonoBehaviour
{
	/*
	 * PersistentDataID
	 * 
	 * PersistentDataId : property
	 * 
	 * Users must specify what ID their Serializable object will take
	 * I don't expect for this enum list to get very big at all, so using
	 * a compile-time constant is quick and easy.
	 * 
	 * Your ID gets serialized along with the object data, so we know what
	 * to retrieve from the file and what object to inflate
	 */

	public abstract PersistentDataID PersistentDataId 
	{
		get;
	}

	/*
	 * Serialize
	 * 
	 * Use this function to serialize data members 
	 * 
	 * Currently there's support for the serialization of common 
	 * primitive types, strings, and Unity math classes(Vectors, Matrices, Quats)
	 * 
	 * Support for more types can be easily added upon request.
	 * 
	 * Use the overloaded Write() function of the passed in PersistentDataWriter
	 * in order to write your data to the current save file
	 * 
	 * Just call Write()..I could make another wrapper to abstract this further
	 * but we're all programmers here. Don't prematurely call Dispose() on the writer object..
	 */
	public abstract void Serialize(PersistentDataWriter writer);

	/*
	 * DeSerialize
	 * 
	 * Use this function to deserialize data members 
	 * 
	 * Currently there's support for the deserialization of common 
	 * primitive types, strings, and Unity math classes(Vectors, Matrices, Quats)
	 * 
	 * Support for more types can be easily added upon request.
	 * 
	 * Use the Read'X'() functions of the passed in PersistentDataReader
	 * in order to read your data from the current save file
	 */
	public abstract void Deserialize(PersistentDataReader reader);
}

