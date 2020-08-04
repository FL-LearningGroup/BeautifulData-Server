using System;
using System.Collections.Generic;
using System.Text;
using BDS.CollectData;

namespace BDS.Pipeline.FuYang
{
    public class FyYangPublicInfoFilter : IWorkFilter
    {
        private string tagName;
        private string tagId;
        private string tagClass;

        public FyYangPublicInfoFilter()
        {
            this.tagName = "";
            this.tagId = "";
            this.TagClass = "";
        }
        public string TagName
        {
            get
            {
                return this.tagName;
            }
            set
            {
                this.tagName = value;
            }
        }

        public string TagId
        {
            get
            {
                return this.tagId;
            }
            set
            {
                this.tagId = value;
            }
        }
        public string TagClass
        {
            get
            {
                return this.tagClass;
            }
            set
            {
                this.tagClass = value;
            }
        }
    }

}
