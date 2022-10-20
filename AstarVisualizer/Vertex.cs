﻿using System.Diagnostics.CodeAnalysis;

using SFML.Graphics;
using SFML.System;

namespace AstarVisualizer;

public class Vertex
{
    private Vector2f _position;
    private readonly CircleShape _circle;
    private readonly Dictionary<Vertex, Edge> _edges = new();

    public Vector2f Position
    {
        get => _position;
        set
        {
            _position = value;
            _circle.Position = value;
            foreach (var edge in _edges.Values)
            {
                edge.Update();
            }
        }
    }

    public float Radius
    {
        get => _circle.Radius;
    }

    public IReadOnlyCollection<Vertex> Connections => _edges.Keys;
    public IReadOnlyCollection<Edge> Edges => _edges.Values;

    public Vertex(Vector2f position, float radius)
    {
        _position = position;
        _circle = new CircleShape(radius)
        {
            Origin = new Vector2f(radius, radius),
            Position = _position,
            FillColor = Color.White,
            OutlineColor = Color.Black,
            OutlineThickness = 2
        };
    }

    /// <summary>
    /// Connects this vertex to another vertex and produces an edge.
    /// </summary>
    /// <param name="other"></param>
    /// <param name="edge">
    /// </param>
    /// <returns><see langword="true"/> if the connection was created, or <see langword="false"/> if it already exists.</returns>
    public bool Connect(Vertex other, [NotNullWhen(true)] out Edge? edge)
    {
        edge = new Edge(this, other);

        if (!_edges.TryAdd(other, edge))
            return false;

        // This should not happen, so throw an exception.
        if (!other._edges.TryAdd(this, edge))
            throw new InvalidOperationException("Failed to connect vertices.");

        return true;
    }

    /// <summary>
    /// Disconnect this vertex from another vertex.
    /// </summary>
    /// <param name="other">The other vertex to remove the connection from.</param>
    /// <returns><see langword="true"/> if the connection was removed, or <see langword="false"/> if it does not exist.</returns>
    public bool Disconnect(Vertex other, out Edge? edge)
    {
        if (!_edges.Remove(other, out edge))
            return false;

        // This should not happen, so throw an exception.
        if (!other._edges.Remove(this))
            throw new InvalidOperationException("Failed to disconnect vertices.");

        return true;
    }

    public void Draw(RenderTarget g)
    {
        g.Draw(_circle);
    }
}