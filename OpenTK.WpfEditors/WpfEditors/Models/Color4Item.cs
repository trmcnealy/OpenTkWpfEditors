using System;
using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

public class Color4Item
{
    [CLSCompliant(false)]
    public Color4 Color { get; set; }
    public string Name  { get; set; }
    
    [CLSCompliant(false)]
    public Color4Item(Color4 color,
                      string name)
    {
        Color = color;
        Name  = name;
    }

    public override bool Equals(object? obj)
    {
        if(obj is not Color4Item ci)
        {
            return false;
        }

        return ci.Color.Equals(Color) && ci.Name.Equals(Name);
    }

    public override int GetHashCode()
    {
        return Color.GetHashCode() ^ Name.GetHashCode();
    }
}
