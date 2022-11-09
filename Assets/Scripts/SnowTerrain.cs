using UnityEngine;
using System;
using System.Collections;

public class SnowTerrain : MonoBehaviour
{

	//THIS WHOLE THING IS STRAIGHT UP PLAGIARISED.

	Terrain m_terrain;
	TerrainData m_beginData;
	TerrainData m_tempData;

	public float m_snowDepth = 0.007f;

	public int resolution { get { return m_tempData.heightmapResolution; } }

	public delegate float DigFunction(float x, float y, float current, float begin);

	// Use this for initialization
	void Start()
	{
		m_terrain = GetComponent<Terrain>();

		m_beginData = m_terrain.terrainData;
		m_tempData = Instantiate(m_beginData);
		m_terrain.terrainData = m_tempData;

		InvokeRepeating(nameof(Apply), 1f, 20f);

	}

    [Obsolete]
    void Apply()
	{
        m_terrain.terrainData.SyncHeightmap();
	}
	public bool InsideBounds(int coord) { return coord >= 0 && coord < m_tempData.heightmapResolution; }

	//Clamp 1Dimensional coordinate and size to fit on the heightmap
	public void ClampToBounds(ref int coord, ref int size, bool resize = true)
	{
		int oldCoord = coord;

		coord = Mathf.Clamp(coord, 0, resolution);

		if (resize)
			size -= (coord - oldCoord);

		size = Mathf.Min(size, resolution - coord);
	}

	//Clamp 2Dimensional position and size to fit on the heightmap
	public void ClampToBounds(ref int x, ref int y, ref int width, ref int height, bool resize = true)
	{
		ClampToBounds(ref x, ref width, resize);
		ClampToBounds(ref y, ref height, resize);
	}

	public void Hello()
    {
		Debug.Log("lol");
    }

	public void Dig(Vector3 world, Vector2 size)
	{
		int x, y;
		int width = (int)((size.x / m_beginData.size.x) * resolution);
		int height = (int)((size.y / m_beginData.size.z) * resolution);

		if (Project(world, out x, out y))
		{
			// Function is a just a simple paraboloid
			SetHeightFunc(x - width / 2 - 1, y - height / 2 - 1, width + 1, height + 1,
				(fx, fy, c, b) => Mathf.Min(c, b - Mathf.Max(m_snowDepth * (1f - (fx * fx + fy * fy)), 0f)));
		}
	}

	//Project a world point onto the terrain, and get the height-map pixel corresponding to that point
	//Returns if the projection lands on the plane
	public bool Project(Vector3 world, out int x, out int y)
	{
		Vector3 offset = world - transform.position;
		offset.x /= m_tempData.size.x;
		offset.z /= m_tempData.size.z;

		offset *= resolution;

		x = Mathf.RoundToInt(offset.x);
		y = Mathf.RoundToInt(offset.z);

		return (x >= 0 && x <= resolution &&
			y >= 0 && y <= resolution);
	}

	public void SetHeightFunc(int x, int y, int width, int height, DigFunction function)
	{
		ClampToBounds(ref x, ref y, ref width, ref height, true);

		//The 2-dimensional array is arranged [y, x], remember that
		float[,] beginHeightList = m_beginData.GetHeights(x, y, width, height);
		float[,] currentHeightList = m_tempData.GetHeights(x, y, width, height);
		float[,] newHeightList = new float[height, width];

		for (int i = 0; i < width; i++)
			for (int j = 0; j < height; j++)
			{
				float xFunc = ((float)i / width - 0.5f) * 2f;
				float yFunc = ((float)j / height - 0.5f) * 2f;

				float h = function(xFunc, yFunc, currentHeightList[j, i], beginHeightList[j, i]);

				//That's why I flip the values here
				newHeightList[j, i] = h;
			}

		m_tempData.SetHeightsDelayLOD(x, y, newHeightList);
	}
}