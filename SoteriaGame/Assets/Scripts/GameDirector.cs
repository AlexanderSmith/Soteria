 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDirector: MonoBehaviour {

    public int tilewidth    = 100;
    public int tileheight   = 100;
    public int LevelWidth   = 2;
    public int LevelHeight  = 2;

    public GameObject Player;
	public string texturename;
    private Vector2 currentTile = Vector2.zero;
	private Vector2 prevTile = Vector2.zero;
	private Texture2D mainTexture;

    private List<TileStruct> tileList;

	struct TileStruct
	{
		public Texture2D texture;
		public GameObject tile;

		public TileStruct(GameObject tile, Texture2D texture)
		{
			this.tile = tile;
			this.texture = texture;
		}
	};

	struct EnemyStruct
	{
		public GameObject enemy;
		public Vector2 tile;
		public EnemyStruct(GameObject go, Vector2 vect) 
		{
			this.enemy = go;
			this.tile = vect;
		}
	};

	private List<EnemyStruct> enemyList;
	
	void Awake () 
    {
        tileList = new List<TileStruct>();
		enemyList = new List<EnemyStruct>();
		//loadMainTexture();
        //GenerateTiles();
		//spawnEnemy ();
	}
	void loadMainTexture()
	{
		mainTexture = Resources.Load(texturename) as Texture2D;
	}

	private GameObject CreateTileObject(int x, int y, int width, int height, Rect TexRect, ref Texture2D destTex)
	{
		GameObject go = Instantiate(Resources.Load("Tile_1")) as GameObject; //Load dynamiclly using Tile_x_y for name;
		go.SetActive(true);
		go.transform.position = Vector3.zero;
		go.transform.Translate(-tilewidth/2 + (x * tilewidth * -1), 0.0f, -tileheight/2 + (y * tileheight * -1));
	
		Color[] pix = mainTexture.GetPixels( (int)TexRect.x, (int)TexRect.y, (int)TexRect.width, (int)TexRect.height);
		destTex = new Texture2D((int)TexRect.width, (int)TexRect.height);
		
		destTex.SetPixels(pix);
		destTex.Apply();
		go.GetComponent<Renderer>().material.mainTexture = destTex;

		return go;
	}
	void GenerateTiles()
	{
		Rect TexRec;
		if (LevelWidth > 0 && LevelHeight > 0)
		{
			for (int y = 0; y < LevelHeight; y++)
			{
				//if ( (y * tileheight) <= mainTexture.height)
				//{
					for (int x = 0; x < LevelWidth; x++)
					{
						//if ( (x * tilewidth) <= mainTexture.width)
						//{
							Texture2D text = null;

							float pixelwidth = tilewidth ;
							float pixelheight = tileheight;

							TexRec = new Rect(x * pixelwidth, y * pixelheight, pixelwidth, pixelheight);

							GameObject go = CreateTileObject(x, y, tilewidth, tileheight, TexRec, ref text);
							TileStruct newTile = new TileStruct(go, text);

							tileList.Add(newTile);
						//}
					}
				//}
			}
		}
	}
	void Update () 
    {
        findCurrentTile();
		startTileEvent();
    }

    void findCurrentTile()
    {
        Vector3 CurrentPosition =  Player.GetComponent<Transform>().transform.position;

        CurrentPosition.x = (int)(CurrentPosition.x / tilewidth);
        CurrentPosition.z = (int)(CurrentPosition.z / tileheight);

		this.prevTile = this.currentTile;

        this.currentTile.x = CurrentPosition.x;
        this.currentTile.y = CurrentPosition.z;
    }

	void startTileEvent()
	{
		if (!prevTile.Equals (currentTile)) 
		{
			if (Random.Range(1,100) % 3 == 0)
				spawnEnemy ();
		}
	}

	void spawnEnemy()
	{
	
		foreach (EnemyStruct enemy in enemyList) 
		{
			if (enemy.tile.Equals(this.currentTile))
			{
			//	Debug.Log ("SAME TILE");
				return;
			}
		}

		GameObject go = Instantiate(Resources.Load("Enemy")) as GameObject;
		go.SetActive(true);
		go.transform.position = Vector3.zero;
		go.transform.Translate((currentTile.x + 1)* tilewidth - 16.0f, 0.5f, (currentTile.y + 1) * tileheight - 20.0f);

		EnemyStruct Enemy = new EnemyStruct (go, this.currentTile);

		enemyList.Add (Enemy);

		foreach (EnemyStruct enemy in enemyList.ToArray()) 
		{
			//Debug.Log ("ENEMY!!");
			if (!enemy.tile.Equals(this.currentTile))
			{
			//	Debug.Log ("LEFT NOW DIE");
				enemyList.Remove(enemy);
				Destroy(enemy.enemy);
			}
		}
	}

}
