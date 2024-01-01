using Godot;
using System;
using System.Collections.Generic;

public partial class TileMap : Godot.TileMap
{

	[Export]
	public int width = 350;
	[Export]
	public int height = 350;
	public FastNoiseLite fastNoiseLite = new();

	public class TileDictionary
	{
		public static Vector2I WATER = new(7, 4);
		public static Vector2I SAND = new(7, 1);
		public static Vector2I GRASS = new(8, 2);
		public static Vector2I GRASSIER_GRASS = new(9, 6);
		public static Vector2I ROCK = new(16, 10);
		public static Vector2I ROCKIER_ROCK = new(5, 3);
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		generate_world();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}


	public void generate_world()
	{
		RandomNumberGenerator rng = new();

		List<Vector2I> tilesList = new();

		tilesList.Add(TileDictionary.WATER);
		tilesList.Add(TileDictionary.SAND);
		tilesList.Add(TileDictionary.GRASSIER_GRASS);
		tilesList.Add(TileDictionary.GRASS);
		tilesList.Add(TileDictionary.ROCK);
		tilesList.Add(TileDictionary.ROCKIER_ROCK);

		rng.Randomize();
		fastNoiseLite.Seed = rng.RandiRange(0, 500);

		fastNoiseLite.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;

		fastNoiseLite.FractalOctaves = tilesList.Count;

		fastNoiseLite.FractalGain = 0;

		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float absNoise = Math.Abs(fastNoiseLite.GetNoise2D(x, y));

				int tileToPlace = (int)Math.Floor((absNoise * tilesList.Count));
				SetCell(0, new(x, y), 0, tilesList[tileToPlace]);

			}
		}

	}

}
