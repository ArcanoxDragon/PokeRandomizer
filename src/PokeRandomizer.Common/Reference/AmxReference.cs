namespace PokeRandomizer.Common.Reference
{
	public class AmxReference
	{
		public AmxReference(int fileNumber, AmxNames name)
		{
			FileNumber = fileNumber;
			Name = name;
		}

		public int      FileNumber { get; set; }
		public AmxNames Name       { get; set; }
	}
}