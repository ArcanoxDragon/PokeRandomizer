using System.Threading.Tasks;

namespace PokeRandomizer.Utility
{
	public interface ILogger
	{
		Task WriteAsync(string text);
		Task WriteLineAsync(string text = "");
		Task FlushAsync();
	}
}