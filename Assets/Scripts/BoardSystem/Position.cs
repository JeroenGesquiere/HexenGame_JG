using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public struct Position
{
    private readonly int _q;
    private readonly int _r;
    public int Distance;

    public int Q => _q;
    public int R => _r;

    public Position (int q, int r)
    {
        _q = q;
        _r = r;

        Distance = ( (int)(MathF.Abs(_q)  + (int)( MathF.Abs ( _q + _r ) ) + (int) ( MathF.Abs ( _r) ) ) / 2 ) ;
    }

    public override string ToString()
    {
        return $"position({Q},{R})";

    }
}

