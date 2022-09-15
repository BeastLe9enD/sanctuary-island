using System;

using URandom = Unity.Mathematics.Random;

namespace Utils
{
    public static class RamdomUtils
    {
        public static URandom GetRandom() => new URandom((uint)DateTime.Now.Ticks);
    }
}