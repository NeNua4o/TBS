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

        public static List<BMapCell> Intersection (this List<BMapCell> array, List<BMapCell> intersectionWith)
        {
            var result = new List<BMapCell>();
            for (int sourceCounter = 0; sourceCounter < array.Count; sourceCounter++)
                for (int intersectCounter = 0; intersectCounter < intersectionWith.Count; intersectCounter++)
                    if (array[sourceCounter].Axial == intersectionWith[intersectCounter].Axial)
                        result.Add(array[sourceCounter]);
            return result;
        }
    }
}
