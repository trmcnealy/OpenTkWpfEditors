using OpenTK.Mathematics;

namespace OpenTK.WpfEditors;

internal struct HsvColor4
{
    public float H;
    public float S;
    public float V;
    public float A;

    public HsvColor4(float h,
                     float s,
                     float v,
                     float a)
    {
        H = h;
        S = s;
        V = v;
        A = a;
    }
}
