using System;

namespace CardGrid.Types
{
    [Flags]
    public enum CardType
    {
        Action = 1 << 0,        // 1
        Artfulness = 1 << 1,    // 2
        Empowerment = 1 << 2    // 4
    }
}