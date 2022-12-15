using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PokeRandomizer.Utility
{
	static class ReflectionUtil
	{
		/// <summary>
		/// Tries to find a custom attribute of the specified type on any of the interfaces implemented by the declaring type
		/// </summary>
		/// <typeparam name="T">The type of Attribute to search for</typeparam>
		[SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
		[SuppressMessage("ReSharper", "LoopCanBePartlyConvertedToQuery")]
		public static T GetImplementedCustomAttribute<T>(this MemberInfo info) where T : Attribute
		{
			const BindingFlags SearchFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
			var type = info.DeclaringType;

			if (type == null)
				return null;

			foreach (var i in type.GetInterfaces())
			{
				var members = i.GetMember(info.Name, info.MemberType, SearchFlags);

				if (members.Length == 0)
					continue;

				foreach (var member in members)
				{
					var att = member.GetCustomAttribute<T>();

					if (att != null)
						return att;
				}
			}

			return null;
		}
	}
}