namespace Deve
{
    public class DataOptions
    {
        /// <summary>
        /// Preferred language.
        /// </summary>
        public string LangCode { get; set; } = Constants.DefaultLangCode;

        /// <summary>
        /// Origin device Id.
        /// </summary>
        public string OriginId { get; set; } = string.Empty;
    }
}
