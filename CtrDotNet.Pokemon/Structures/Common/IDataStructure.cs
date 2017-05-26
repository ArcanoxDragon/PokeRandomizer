using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrDotNet.Pokemon.Structures.Common
{
	public interface IDataStructure
	{
		void Read( byte[] data );
		byte[] Write();
	}
}