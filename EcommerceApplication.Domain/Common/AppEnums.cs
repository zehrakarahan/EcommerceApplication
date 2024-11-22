using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EcommerceApplication.Domain.Common
{
    public class AppEnums
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum DisCountType
        {
            FixedAmount=1,
            PersantageRace=2

        }
        

    }
}
