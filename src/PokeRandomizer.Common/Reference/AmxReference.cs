namespace PokeRandomizer.Common.Reference
{
	public class AmxReference
	{
		public AmxReference( int fileNumber, AmxNames name )
		{
			this.FileNumber = fileNumber;
			this.Name       = name;
		}

		public int      FileNumber { get; set; }
		public AmxNames Name       { get; set; }
	}
}