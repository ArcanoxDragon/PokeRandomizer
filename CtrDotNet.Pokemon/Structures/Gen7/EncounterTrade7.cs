using System;

namespace CtrDotNet.Pokemon.Structures.Gen7
{
    public class EncounterTrade7 : Structures.EncounterStatic
    {
        public const int SIZE = 0x34;

        public readonly byte[] Data;
        public EncounterTrade7(byte[] data)
        {
            this.Data = data;
        }
        public override int Species
        {
            get { return BitConverter.ToUInt16(this.Data, 0x0); }
            set { BitConverter.GetBytes((ushort)value).CopyTo(this.Data, 0x0); }
        }
        public int Form { get { return this.Data[0x4]; } set { this.Data[0x4] = (byte)value; } }
        public int Level { get { return this.Data[0x5]; } set { this.Data[0x5] = (byte)value; } }
        public int[] IVs
        {
            get
            {
                return new int[]
                {
                        (sbyte) this.Data[0x6], (sbyte) this.Data[0x7], (sbyte) this.Data[0x8], (sbyte) this.Data[0x9], (sbyte) this.Data[0xA], (sbyte) this.Data[0xB]
                };
            }
            set
            {
                if (value.Length != 6)
                    return;
                for (int i = 0; i < 6; i++)
                    this.Data[i + 0x6] = (byte)Convert.ToSByte(value[i]);
            }
        }
        public int Ability { get { return this.Data[0xC]; } set { this.Data[0xC] = (byte)value; } }
        public int Nature { get { return this.Data[0xD]; } set { this.Data[0xD] = (byte)value; } }
        public int Gender { get { return this.Data[0xE]; } set { this.Data[0xE] = (byte)value; } }
        public int TID { get { return BitConverter.ToUInt16(this.Data, 0x10); } set { BitConverter.GetBytes((ushort)value).CopyTo(this.Data, 0x10); } }
        public int SID { get { return BitConverter.ToUInt16(this.Data, 0x12); } set { BitConverter.GetBytes((ushort)value).CopyTo(this.Data, 0x12); } }
        public uint ID { get { return BitConverter.ToUInt32(this.Data, 0x10); } set { BitConverter.GetBytes((ushort)value).CopyTo(this.Data, 0x10); } }
        public override int HeldItem
        {
            get
            {
                int item = BitConverter.ToInt16(this.Data, 0x14);
                if (item < 0) item = 0;
                return item;
            }
            set
            {
                if (value == 0) value = -1;
                BitConverter.GetBytes((short)value).CopyTo(this.Data, 0x14);
            }
        }
        public int trGender { get { return this.Data[0x1A]; } set { this.Data[0x1A] = (byte)value; } }
        
        public int TradeRequestSpecies
        {
            get { return BitConverter.ToUInt16(this.Data, 0x2C); }
            set { BitConverter.GetBytes((ushort)value).CopyTo(this.Data, 0x2C); }
        }
    }
}
