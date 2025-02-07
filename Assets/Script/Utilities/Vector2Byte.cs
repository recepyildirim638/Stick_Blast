using System;

[Serializable]
public struct Vector2Byte
{
    public byte x;
    public byte y;

    public Vector2Byte(byte x, byte y)
    {
        this.x = x;
        this.y = y;
    }
    public static Vector2Byte operator +(Vector2Byte a, Vector2Byte b)
    {
        return new Vector2Byte((byte)(a.x + b.x), (byte)(a.y + b.y));
    }

    public static Vector2Byte operator -(Vector2Byte a, Vector2Byte b)
    {
        return new Vector2Byte((byte)(a.x - b.x), (byte)(a.y - b.y));
    }

    public static bool operator == (Vector2Byte a, Vector2Byte b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator != (Vector2Byte a, Vector2Byte b)
    {
        return !(a == b);
    }
    public override string ToString()
    {
        return $"({x}, {y})";
    }
}
