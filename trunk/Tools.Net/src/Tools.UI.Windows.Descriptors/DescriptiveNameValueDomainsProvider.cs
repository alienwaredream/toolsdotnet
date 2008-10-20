using System.Collections;

namespace Tools.UI.Windows.Descriptors
{
    public class DescriptiveNameValueDomainsProvider : IDomainsProvider<DescriptiveNameValue<string>>
    {
        #region Globals

        private MarksPresentationType _marksPresentationType = MarksPresentationType.Encoded;

        #endregion

        #region Properties

        public IDictionary MarkValues { get; set; }

        public MarksPresentationType MarksPresentationType
        {
            get { return _marksPresentationType; }
            set { _marksPresentationType = value; }
        }

        #endregion

        public DescriptiveNameValueDomainsProvider()
            : this(new Hashtable())
        {
        }

        public DescriptiveNameValueDomainsProvider
            (
            IDictionary markValues
            )
        {
            MarkValues = markValues;
        }

        #region IDomainsProvider<DescriptiveNameValue<string>> Members

        public string[] GetDomainValues(DescriptiveNameValue<string> dnv)
        {
            return new string[3]
                       {
                           dnv.Name,
                           dnv.Description,
                           getValue(dnv.Value)
                       };
        }

        public string[] GetDomainNames()
        {
            return new string[3]
                       {
                           "Name",
                           "Description",
                           "Value"
                       };
        }

        public DescriptiveNameValue<string> GetNewDefaultInstance()
        {
            return new DescriptiveNameValue<string>
                (
                "Name",
                "Value",
                "Description"
                );
        }

        #endregion

        private string getValue(string source)
        {
            if (_marksPresentationType == MarksPresentationType.Encoded)
                return source;
            //string dateTimeDecodedValue = ScriptParams.DecodePathTimeMarks(source, DateTime.UtcNow);
            //return ScriptParams.ParseToString
            //(
            //this._markValues,
            //dateTimeDecodedValue
            //);
            return null;
        }
    }
}