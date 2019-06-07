using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.Configurations;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.System;
using IrpsApi.Framework.System.Repositories;
using Microsoft.Extensions.Options;

namespace IrpsApi.Api.Controllers
{
    public class DocumentApiControllerBase : RequiresAuthenticationApiControllerBase
    {
        private readonly IOptionsMonitor<MediaBaseUrlSettings> _settings;
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogRepository _logRepository;

        public DocumentApiControllerBase(IOptionsMonitor<MediaBaseUrlSettings> settings, IDocumentRepository documentRepository, ILogRepository logRepository)
        {
            _settings = settings;
            _documentRepository = documentRepository;
            _logRepository = logRepository;
        }

        protected async Task<DocumentModel> GetDocumentAsync(IAccount account, string documentTypeId, ExpandOptions expandOptions, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.GetByDocumentTypeAsync(account.Id, documentTypeId, cancellationToken);

            if (document == null)
            {
                return null;
            }

            var path = string.Empty;
            var format = document.MimeType.Split('/')[1];
            if (!string.IsNullOrEmpty(document.DocumentUrl))
            {
                var fileName = document.DocumentUrl.Split('/')[1];
                var fullFileName = $"{fileName}.{format}";
                path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles\\Images", fullFileName);
            }

            // اگر به هر دلیلی آواتار در آدرس ذخیره عکس‌ها نباشد، آن را ذخیره می‌کنیم و رکورد را آپدیت می‌کنیم
            if (string.IsNullOrEmpty(path) || !System.IO.File.Exists(path))
            {
                try
                {
                    var fileName = $"{Guid.NewGuid()}{account.UserCode}";
                    var fullFileName = $"{fileName}.{format}";
                    var url = $"{_settings.CurrentValue.BaseUrl}/{fileName}";
                    path = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles\\Images", fullFileName);
                    using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                        await fileStream.WriteAsync(document.Data, 0, document.Data.Length, cancellationToken);

                    document.DocumentUrl = url;
                    await _documentRepository.UpdateAsync(document, cancellationToken);
                }
                catch (Exception exception)
                {
                    _logRepository.InsertLog("Irps.API", LogLevelIds.Error, null, "Accounts.PersonProfile.GetPersonProfileAvatarAsync", "Description", "خطا در دریافت آدرس آواتار کاربر.", "Exception", exception.ToString(), "IP", RemoteIpAddress);
                }
            }

            return await document.ToDocumentModelAsync(GetExpandOptions(expandOptions), cancellationToken);
        }
    }
}
