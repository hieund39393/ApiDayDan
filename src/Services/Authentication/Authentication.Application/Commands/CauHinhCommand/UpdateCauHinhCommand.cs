using Authentication.Infrastructure.AggregatesModel.CauHinhAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.CauHinhCommand
{
    public class UpdateCauHinhCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string GiaTri { get; set; }
    }

    public class UpdateCauHinhCommandHandler : IRequestHandler<UpdateCauHinhCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCauHinhCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCauHinhCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _unitOfWork.CauHinhBieuGiaRepository
                .FindOneAsync(x => x.Id == request.Id);

            if (checkExist == null) throw new EvnException("Cấu hình không tồn tại");

            checkExist.GiaTri = request.GiaTri;

            _unitOfWork.CauHinhBieuGiaRepository.Update(checkExist);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
