using System.IO;

namespace CtrDotNet.Pokemon.Structures
{
    public abstract class EggMoves
    {
        public int Count;
        public int[] Moves;
        public int FormTableIndex;

        public abstract byte[] Write();
    }
    public class EggMoves6 : EggMoves
    {
        public EggMoves6(byte[] data)
        {
            if (data.Length < 2 || data.Length % 2 != 0)
            { this.Count = 0; this.Moves = new int[0]; return; }
            using (BinaryReader br = new BinaryReader(new MemoryStream(data)))
            {
                this.Moves = new int[this.Count = br.ReadUInt16()];
                for (int i = 0; i < this.Count; i++)
                    this.Moves[i] = br.ReadUInt16();
            }
        }
        public static EggMoves[] getArray(byte[][] entries)
        {
            EggMoves[] data = new EggMoves[entries.Length];
            for (int i = 0; i < data.Length; i++)
                data[i] = new EggMoves6(entries[i]);
            return data;
        }
        public override byte[] Write()
        {
            this.Count = this.Moves.Length;
            if (this.Count == 0) return new byte[0];
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write((ushort)this.Count);
                for (int i = 0; i < this.Count; i++)
                    bw.Write((ushort)this.Moves[i]);

                return ms.ToArray();
            }
        }
    }
    public class EggMoves7 : EggMoves
    {
        public EggMoves7(byte[] data)
        {
            if (data.Length < 2 || data.Length % 2 != 0)
            { this.Count = 0; this.Moves = new int[0]; return; }
            using (BinaryReader br = new BinaryReader(new MemoryStream(data)))
            {
                this.FormTableIndex = br.ReadUInt16();
                this.Count = br.ReadUInt16();
                this.Moves = new int[this.Count];
                for (int i = 0; i < this.Count; i++)
                    this.Moves[i] = br.ReadUInt16();
            }
        }
        public static EggMoves[] getArray(byte[][] entries)
        {
            EggMoves[] data = new EggMoves[entries.Length];
            for (int i = 0; i < data.Length; i++)
                data[i] = new EggMoves7(entries[i]);
            return data;
        }
        public override byte[] Write()
        {
            this.Count = this.Moves.Length;
            if (this.Count == 0) return new byte[0];
            using (MemoryStream ms = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(ms))
            {
                bw.Write((ushort)this.FormTableIndex);
                bw.Write((ushort)this.Count);
                for (int i = 0; i < this.Count; i++)
                    bw.Write((ushort)this.Moves[i]);

                return ms.ToArray();
            }
        }
    }
}
