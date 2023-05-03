using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Model
{
    public class ApiResultData<T>
    {
        [DataMember]
        public string Data { get; set; }
        public bool Suc { get; set; }
        public string Msg { get; set; }
        public T GetData
        {
            get
            {
                return JsonConvert.DeserializeObject<T>(Data);
            }
        }
    }
}
