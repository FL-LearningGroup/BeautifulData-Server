namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BDS.CollectData;
    [Serializable]
    internal class FyYangPublicInfoFilter : IWorkFilter
    {
        private string _tagName;
        private string _tagId;
        private string _tagClass;

        public FyYangPublicInfoFilter()
        {
            _tagName = "";
            _tagId = "";
            TagClass = "";
        }
        public string TagName
        {
            get
            {
                return _tagName;
            }
            set
            {
                _tagName = value;
            }
        }

        public string TagId
        {
            get
            {
                return _tagId;
            }
            set
            {
                _tagId = value;
            }
        }
        public string TagClass
        {
            get
            {
                return _tagClass;
            }
            set
            {
                _tagClass = value;
            }
        }
    }

}
