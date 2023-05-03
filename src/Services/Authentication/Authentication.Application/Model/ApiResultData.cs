using Authentication.Application.Model.ChiTietBieuGia;
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

    public class ApiResultData2
    {
        public bool Suc { get; set; }
        public string Msg { get; set; }
        public List<ApiBaoGiaResponse> Data { get; set; }
    }

    public class ApiResultLoginSSO
    {
        public string Code { get; set; }
        public string ParamCode { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public ApiResultLoginSSOData Data { get; set; }
    }

    public class ApiResultLoginSSOData
    {
        public string ServiceTicket { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public IdentitySSO Identity { get; set; }
    }

    public class IdentitySSO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public string AppCode { get; set; }
        public string AppId { get; set; }
        public string Email { get; set; }
        public string Ns_id { get; set; }
        public string DepId { get; set; }
        public string StaffCode { get; set; }
        public string PositionName { get; set; }
        public string Phone { get; set; }
    }
}
