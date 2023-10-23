using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Tilemaps;

public readonly struct OnTileChanged
{
    public readonly Tile Tile;

    public OnTileChanged(Tile tile)
    {
        Tile = tile;
    }
}
public class TileObserver : MonoBehaviour
{
    private Tile _lastTile;
    private Collider _collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Tile>(out var tile))
        {
            if (_lastTile != tile)
            {
                MessageBroker
                    .Default
                    .Publish(new OnTileChanged(tile));
            }
        }
    }
}
