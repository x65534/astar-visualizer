﻿using SFML.Graphics;

namespace AstarVisualizer;

public class Edge
{
    private readonly LineShape _lineShape = new()
    {
        FillColor = Theme.EdgeFill,
        Weight = 6
    };

    public Vertex A { get; }
    public Vertex B { get; }

    public float Distance => _lineShape.Distance;

    public float Weight
    {
        get => _lineShape.Weight;
        set => _lineShape.Weight = value;
    }

    public Color Color
    {
        get => _lineShape.FillColor;
        set => _lineShape.FillColor = value;
    }

    public Edge(Vertex a, Vertex b)
    {
        A = a;
        B = b;
        Update();
    }

    public void Update()
    {
        _lineShape.PointA = A.Position;
        _lineShape.PointB = B.Position;
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(_lineShape);
    }
}
