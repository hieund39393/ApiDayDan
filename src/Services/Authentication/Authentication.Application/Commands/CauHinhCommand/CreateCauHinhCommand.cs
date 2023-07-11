using Authentication.Infrastructure.AggregatesModel.CauHinhAggregate;
using Authentication.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.CauHinhCommand
{
    public class CreateCauHinhCommand : IRequest<bool>
    {
        public int PhanLoai { get; set; }
        public string TenCauHinh { get; set; }
        public string GiaTri { get; set; }
        public int Quy { get; set; }
        public int Nam { get; set; }
    }

    public class CreateCauHinhCommandHandler : IRequestHandler<CreateCauHinhCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCauHinhCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CreateCauHinhCommand request, CancellationToken cancellationToken)
        {
            var data = new CauHinhBieuGia
            {
                PhanLoaiCap = request.PhanLoai,
                TenCauHinh = request.TenCauHinh,
                GiaTri = request.GiaTri,
                Quy = request.Quy,
                Nam = request.Nam
            };
            _unitOfWork.CauHinhBieuGiaRepository.Add(data);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
