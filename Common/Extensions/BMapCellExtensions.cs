using Common.Models;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class BMapCellExtensions
    {
        public static BMapCell WitchIs(this List<BMapCell> array, BMapCell item)
        {
            for (int i = 0; i < array.Count; i++)
            {
                if (array[i].Axial == item.Axial)
                    return array[i];
            }
            return null;
        }

        
    }
}
