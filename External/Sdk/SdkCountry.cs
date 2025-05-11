﻿using Deve.Model;
using Deve.Criteria;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    internal class SdkCountry : SdkBaseGet<Country, Country, CriteriaCountry, ISdk>, IDataCountry
    {
        public SdkCountry(ISdk sdk)
            : base(ApiConstants.PathCountry, sdk)
        {
        }
    }
}
