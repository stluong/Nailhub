﻿namespace Generic.Core.Model.Identity
{
    public class ApplicationMessage
    {
        public virtual string Body
        {
            get;
            set;
        }

        public virtual string Destination
        {
            get;
            set;
        }

        public virtual string Subject
        {
            get;
            set;
        }

    }
}
