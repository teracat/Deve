﻿using Deve.Criteria;
using Deve.Internal.Model;

namespace Deve.Internal.Criteria
{
    public class CriteriaClient : CriteriaClientBasic
    {
        public ClientStatus? Status { get; set; }

        public CriteriaClient()
        {
        }

        public CriteriaClient(CriteriaClientBasic other)
            : base(other)
        {
        }
    }
}
