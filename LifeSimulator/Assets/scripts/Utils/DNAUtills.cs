using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;

namespace DNAUtils
{

    [System.Serializable]
    public class GeneEntry
    {
        public PhysicsU.Directions Direction;
        public CellType Type;
    }

    public enum CellType
    {
        None, BirthCell, BoosterCell, BaseCell, AndCell, OrCell,
        NotCell, XorCell, OnCell, DownGateCell, UpGateCell, LeftGateCell, RightGateCell, FilterCell,
        MouthCell, PowerPlantCell, SolarPanelCell
    }

    public static class DNAU {
        public static List<Gene> GeneEntries2GeneList(GeneEntry[] GeneEntries)
        {
            List<Gene> Genes = new List<Gene>();
            foreach (GeneEntry e in GeneEntries)
            {
                Genes.Add(new Gene(e.Direction, e.Type));
            }
            return Genes;
        }
    }
}
