using Externalio.Model;
using Externalio.Model.Configuration;

namespace Externalio.Parser
{
	public class ConcatenateDuplicatedKeysIniDataParser : IniDataParser
	{
		public ConcatenateDuplicatedKeysIniDataParser()
			: this(new ConcatenateDuplicatedKeysIniParserConfiguration())
		{
		}

		public ConcatenateDuplicatedKeysIniDataParser(ConcatenateDuplicatedKeysIniParserConfiguration parserConfiguration)
			: base(parserConfiguration)
		{
		}

		public new ConcatenateDuplicatedKeysIniParserConfiguration Configuration
		{
			get => (ConcatenateDuplicatedKeysIniParserConfiguration) base.Configuration;
			set => base.Configuration = value;
		}

		protected override void HandleDuplicatedKeyInCollection(string key, string value, KeyDataCollection keyDataCollection, string sectionName)
		{
			keyDataCollection[key] += Configuration.ConcatenateSeparator + value;
		}
	}
}