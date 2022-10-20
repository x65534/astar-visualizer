﻿using SFML.System;

namespace AstarVisualizer;

/// <summary>
/// Represents a line segment.
/// </summary>
public struct Line
{
    public Vector2f PointA;
    public Vector2f PointB;

    public Line()
    {
        PointA = new();
        PointB = new();
    }

    public Line(Vector2f pointA, Vector2f pointB)
    {
        PointA = pointA;
        PointB = pointB;
    }

    /// <summary>
    /// Calculates the point at which two lines intersect.
    /// </summary>
    /// <remarks>
    /// Reference: <see href="https://en.wikipedia.org/wiki/Line–line_intersection#Given_two_points_on_each_line_segment"/>
    /// </remarks>
    /// <param name="lineA">The first line.</param>
    /// <param name="lineB">The second line.</param>
    /// <param name="intersection">
    /// When the method returns, contains the point at which the two lines intersect, if there is an intersection.
    /// </param>
    /// <returns><see langword="true"/> if the lines intersect, otherwise <see langword="false"/>.</returns>
    public static bool Intersects(Line lineA, Line lineB, out Vector2f intersection)
    {
        float
            x1 = lineA.PointA.X, y1 = lineA.PointA.Y,
            x2 = lineA.PointB.X, y2 = lineA.PointB.Y,
            x3 = lineB.PointA.X, y3 = lineB.PointA.Y,
            x4 = lineB.PointB.X, y4 = lineB.PointB.Y;

        float t =
            ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / 
            ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));
        float u =
            ((x1 - x3) * (y1 - y2) - (y1 - y3) * (x1 - x2)) /
            ((x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4));

        if ((0 <= t && t <= 1) && (0 <= u && u <= 1))
        {
            intersection = new Vector2f(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
            return true;
        }
        else
        {
            intersection = default;
            return false;
        }
    }
}