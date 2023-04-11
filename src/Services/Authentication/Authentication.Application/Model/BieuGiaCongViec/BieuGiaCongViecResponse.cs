﻿using EVN.Core.SeedWork.ExtendEntities;

namespace Authentication.Application.Model.BieuGiaCongViec
{
    public class BieuGiaCongViecResponse : BaseExtendEntities // kế thừa BaseExtendEntities dể phân trang, sea
    {
        public Guid Id { get; set; }
        public Guid? IdBieuGia { get; set; }
        public Guid? IdCongViec { get; set; }
        public string TenBieuGia { get; set; }
        public string TenCongViec { get; set; }
    }
}