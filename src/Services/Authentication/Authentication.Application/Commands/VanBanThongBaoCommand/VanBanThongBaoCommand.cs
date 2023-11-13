using Authentication.Infrastructure.AggregatesModel.ActionsAggregate;
using Authentication.Infrastructure.AggregatesModel.CauHinhAggregate;
using Authentication.Infrastructure.Repositories;
using EVN.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Commands.CauHinhCommand
{
    public class VanBanThongBaoCommand : IRequest<bool>
    {
        public int Quy { get; set; }
        public int Nam { get; set; }
        public string GhiChu { get; set; }
        public IFormFile File { get; set; }
    }

    public class VanBanThongBaoCommandHandler : IRequestHandler<VanBanThongBaoCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public VanBanThongBaoCommandHandler(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> Handle(VanBanThongBaoCommand request, CancellationToken cancellationToken)
        {
            var checkExist = await _unitOfWork.VanBanThongBaoRepository.GetAny(x => x.Quy == request.Quy && x.Nam == request.Nam);
            if (checkExist) throw new EvnException("Văn bản đã tồn tại");

            var data = new VanBanThongBao();

            data.GhiChu = request.GhiChu;
            data.Quy = request.Quy;
            data.Nam = request.Nam;
            if (request.File != null)
            {
                string uploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath + "/VanBan");
                if (!Directory.Exists(uploadDirectory))
                {
                    Directory.CreateDirectory(uploadDirectory);
                }
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.File.FileName);
                string filePath = Path.Combine(uploadDirectory, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    request.File.CopyTo(fileStream);
                }
                data.Url = $"/VanBan/{uniqueFileName}";
            }

            _unitOfWork.VanBanThongBaoRepository.Add(data);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
