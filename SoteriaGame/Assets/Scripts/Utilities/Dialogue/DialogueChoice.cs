using UnityEngine;
using System.Collections;

public class DialogueChoice : MonoBehaviour {

	public string NPCText;
	public string [] ChoicesText;
	public DialogueChoice[] DialoguePrefabs;
	public int size;
	public bool isEnd = false;
}
