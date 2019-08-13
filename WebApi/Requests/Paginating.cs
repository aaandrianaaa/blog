using Newtonsoft.Json;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Requests
{

    public class Paginating

    {
        int _page;

        [JsonProperty(PropertyName = "page")]
        public int Page
        {
            get => _page;
            set
            {
                if (value >= 1)
                    _page = value - 1;
            }

        }

        int _limit = 10;

        [JsonProperty(PropertyName = "limit")]
        public int Limit
        {
            get => _limit;
            set
            {
                if (value >= 0)
                    _limit = value;
            }

        } 


    }
}
