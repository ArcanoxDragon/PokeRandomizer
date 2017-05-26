using System;
using System.Linq;

namespace CtrDotNet.Pokemon.Structures.PersonalInfo
{
    public class PersonalInfoSM : PersonalInfoXY
    {
        public new const int SIZE = 0x54;
        public PersonalInfoSM(byte[] data)
        {
            if (data.Length != SIZE)
                return;
            this.data = data;

            this.TMHM = PersonalInfo.GetBits(this.data.Skip(0x28).Take(0x10).ToArray()); // 36-39
            this.TypeTutors = PersonalInfo.GetBits(this.data.Skip(0x38).Take(0x4).ToArray()); // 40
        }
        public override byte[] Write()
        {
            PersonalInfo.SetBits(this.TMHM).CopyTo(this.data, 0x28);
            PersonalInfo.SetBits(this.TypeTutors).CopyTo(this.data, 0x38);
            return this.data;
        }
        
        // No accessing for 3C-4B

        public int SpecialZ_Item { get { return BitConverter.ToUInt16(this.data, 0x4C); } set { BitConverter.GetBytes((ushort)value).CopyTo(this.data, 0x4C); } }
        public int SpecialZ_BaseMove { get { return BitConverter.ToUInt16(this.data, 0x4E); } set { BitConverter.GetBytes((ushort)value).CopyTo(this.data, 0x4E); } }
        public int SpecialZ_ZMove { get { return BitConverter.ToUInt16(this.data, 0x50); } set { BitConverter.GetBytes((ushort)value).CopyTo(this.data, 0x50); } }
        public bool LocalVariant { get { return this.data[0x52] == 1; } set { this.data[0x52] = (byte)(value ? 1 : 0); } }
    }
}
