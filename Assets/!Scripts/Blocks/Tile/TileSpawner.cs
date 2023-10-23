using System.Collections.Generic;
using TMPro;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

// public readonly struct OnTransitionTileLaunched { }
public class TileSpawner : MonoBehaviour
{
    private BlockSpawnConfig _blockSpawnConfig;
    private BuildingSpawnConfig _buildingSpawnConfig;
    private TrafficConfig _trafficSpawnConfig;

    [SerializeField] private List<Tile> _availableTiles = new List<Tile>();
    private List<Tile> _tiles;
    private Tile[] _tileSet;
    private Tile _transitionTile;
    
    private eBlockType _currentBlockType;
    private eBlockType _nextBlockType;
    
    private const int _maxBlockAmountInScene = 2;
    private const int _maxTileLaunch = 3;

    public void Launch(BlockSpawnConfig blockSpawnConfig, BuildingSpawnConfig buildingSpawnConfig,
        TrafficConfig trafficSpawnConfig, Block block,
        ref eBlockType previousBlockType, ref eBlockType nextBlockType,
        ref Vector3 pos, Quaternion rot,
        bool isFirstBlock = false)
    {
        _blockSpawnConfig = blockSpawnConfig;
        _buildingSpawnConfig = buildingSpawnConfig;
        _trafficSpawnConfig = trafficSpawnConfig;
        
        CreateTiles(block, ref previousBlockType, ref nextBlockType, ref pos, rot);
        Debug.Log(isFirstBlock);
        if (isFirstBlock)
        {
            for (int i = 1; i <= _maxTileLaunch; i++)
                LaunchFirstTiles(i);
        }
        else
        {
            // MessageBroker
            //     .Default
            //     .Receive<OnTransitionTileLaunched>()
            //     .Subscribe(message =>
            //     {
            //         for (int i = 1; i <= _maxTileLaunch; i++)
            //             LaunchFirstTiles(i);
            //     });
        }
            


        MessageBroker
            .Default
            .Receive<OnTileChanged>()
            .Subscribe(message =>
            {
                LaunchTilesTraffic(message.Tile);
                DisposeTile(message.Tile);
            });
    }

    private void CreateTiles(Block block, ref eBlockType previousBlockType, ref eBlockType nextBlockType,
        ref Vector3 pos, Quaternion rot)
    {
        _tiles = new List<Tile>();
        _currentBlockType = block.BlockType;

        switch (_currentBlockType)
        {
            case eBlockType.City:
                _tileSet = _blockSpawnConfig.CityTiles;
                break;
            case eBlockType.Desert:
                _tileSet = _blockSpawnConfig.DesertTiles;
                break;
            case eBlockType.Forest:
                _tileSet = _blockSpawnConfig.ForestTiles;
                break;
            case eBlockType.Highway:
                _tileSet = _blockSpawnConfig.HighwayTiles;
                break;
        }
        var crossroadCount = 0;

        for (int i = 0; i < block.TilesCount - 1; i++)
        {
            var randomIndex = Random.Range(0, _tileSet.Length);

            if (crossroadCount < _maxBlockAmountInScene)
            {
                _tiles.Add(Instantiate(_tileSet[randomIndex], pos, rot, transform));
                
                _tiles[i].LaunchBuildings(_buildingSpawnConfig);

                if (randomIndex == _blockSpawnConfig.CrossroadNumberInCity)
                    crossroadCount++;
            }
            else
            {
                // Crossroad index = 0
                randomIndex = Random.Range(_blockSpawnConfig.CrossroadNumberInCity, _tileSet.Length);

                _tiles.Add(Instantiate(_tileSet[randomIndex], pos, rot, transform));
                
                _tiles[i].LaunchBuildings(_buildingSpawnConfig);
            }

            pos.z += _blockSpawnConfig.OffsetZ;
        }

        previousBlockType = _currentBlockType;
        nextBlockType = block.GetBlockType(previousBlockType);

        _transitionTile = GetTransitionTile(_tiles, previousBlockType, nextBlockType, ref pos);

        pos.z += _blockSpawnConfig.OffsetZ;
    }

    private void LaunchFirstTiles(int index)
    {
        _tiles[index].LaunchTraffic(_trafficSpawnConfig, _currentBlockType);
        _availableTiles.Add(_tiles[index]);
    }

    private void LaunchTilesTraffic(Tile tile)
    {
        if (!_tiles.Contains(tile))
            return;
        
        var index = _tiles.IndexOf(tile) + _maxTileLaunch;
        // if (index == _tiles.Count)
        // {
        //     MessageBroker
        //         .Default
        //         .Publish(new OnTransitionTileLaunched());
        //     return;
        // }

        if (index > _tiles.Count - 1)
            return;
        
        if(_availableTiles.Contains(_tiles[index]))
                return;
        
        _tiles[index].LaunchTraffic(_trafficSpawnConfig, _nextBlockType);
        _availableTiles.Add(_tiles[index]);
       
    }


    private Tile GetTransitionTile(List<Tile> tiles, eBlockType previousBlockType, eBlockType nextBlockType, ref Vector3 pos)
    {
        var lastIndex = _tiles.Count - 1;
        switch (previousBlockType)
        {
            case eBlockType.City:
                switch (nextBlockType)
                {
                    case eBlockType.Desert:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.CityDesertTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Forest:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.CityForestTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Highway:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.CityHighwayTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                }
                break;
            case eBlockType.Desert:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertCityTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Forest:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertForestTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Highway:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.DesertHighwayTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                }
                break;
            case eBlockType.Forest:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestCityTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Desert:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestDesertTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Highway:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.ForestHighwayTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                }
                break;
            case eBlockType.Highway:
                switch (nextBlockType)
                {
                    case eBlockType.City:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayCityTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Desert:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayDesertTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                    case eBlockType.Forest:
                        tiles.Add(CreateTransitionTileAsGameObject(_blockSpawnConfig.HighwayForestTile, ref pos));
                        _tiles[lastIndex].LaunchBuildings(_buildingSpawnConfig);
                        break;
                }
                break;
        }

        return tiles[lastIndex];
    }
    
    private Tile CreateTransitionTileAsGameObject(Tile obj, ref Vector3 pos)
    {
        return Instantiate(obj, pos, obj.transform.rotation, transform);
    }

    public BoxCollider GetTransitionTileCollider()
    {
        return _transitionTile.GetComponent<BoxCollider>();
    }
    
    private void DisposeTile(Tile tile)
    {
        var index = _tiles.IndexOf(tile) - 2;
        if (index < 0)
            return;
        
        _availableTiles.Remove(_tiles[index]);
        Destroy(_tiles[index].gameObject);
        _tiles.RemoveAt(index);
        
    }
}