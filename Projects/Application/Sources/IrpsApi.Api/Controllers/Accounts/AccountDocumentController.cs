using System;
using System.Threading;
using System.Threading.Tasks;
using IrpsApi.Api.Configurations;
using IrpsApi.Api.ExpandOptionsHelpers;
using IrpsApi.Api.Models.Accounts;
using IrpsApi.Framework.Accounts;
using IrpsApi.Framework.Accounts.Repositories;
using IrpsApi.Framework.System.Repositories;
using Mabna.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace IrpsApi.Api.Controllers.Accounts
{
    public class AccountDocumentController : DocumentApiControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IDocumentRepository _documentRepository;

        public AccountDocumentController(IAccountRepository accountRepository, IDocumentRepository documentRepository, IOptionsMonitor<MediaBaseUrlSettings> settings, ILogRepository logRepository) : base(settings, documentRepository, logRepository)
        {
            _accountRepository = accountRepository;
            _documentRepository = documentRepository;

            ExpandEngines.Add("account", _accountRepository.GetAsync);
        }

        /// <summary>
        /// Get document/avatar by account id.
        /// </summary>
        /// <response code="404">invalid_account_id, document_not_found</response>  
        /// <response code="403">forbidden</response>
        [HttpGet]
        [Route("accounts/{account_id}/document/type/{type_id}")]
        [SwaggerResponse(200, type : typeof(DocumentModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<DocumentModel>> GetDocumentAsync([FromRoute(Name = "account_id")]string accountId, [FromRoute(Name = "type_id")]string typeId, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            var document = await GetDocumentAsync(account, typeId, expandOptions, cancellationToken);
            if (document == null)
                return NotFound("document_not_found", "There is no document for the account.");

            return Ok(document);
        }

        /// <summary>
        /// Add new document/avatar.
        /// </summary>
        /// <response code="404">invalid_account_id</response>  
        /// <response code="422">missing_document_type, invalid_mime_type, missing_file</response>  
        /// <response code="403">forbidden</response>
        [HttpPost]
        [Route("accounts/{account_id}/document")]
        [SwaggerResponse(200, type : typeof(DocumentModel))]
        [SwaggerResponse(404)]
        [SwaggerResponse(422)]
        [SwaggerResponse(403)]
        public async Task<ActionResult<DocumentModel>> AddDocumentAsync([FromRoute(Name = "account_id")]string accountId, [FromBody]InputDocumentModel documentModel, [FromQuery(Name = "_expand")]ExpandOptions expandOptions, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (accountId != Session.AccountId)
                return Forbid();

            var account = await _accountRepository.GetAsync(accountId, cancellationToken);
            if (account == null)
                return NotFound("invalid_account_id");

            if (documentModel.Type == null || documentModel.Type.Id == DocumentTypeIds.None)
                return UnprocessableEntity(new UnprocessableEntityException("missing_document_type", "Document Type Not Defined!"));

            if (documentModel.Type.Id == DocumentTypeIds.Avatar && !(string.Equals(documentModel.MimeType, "image/jpeg", StringComparison.OrdinalIgnoreCase)
                                                                  || string.Equals(documentModel.MimeType, "image/jpg", StringComparison.OrdinalIgnoreCase)
                                                                  || string.Equals(documentModel.MimeType, "image/png", StringComparison.OrdinalIgnoreCase)))
                return UnprocessableEntity(new UnprocessableEntityException("invalid_mime_type", "Avatar Mime Type Is Incorrect."));

            if (documentModel.Data == null)
                return UnprocessableEntity(new UnprocessableEntityException("missing_file", "File is missing."));

            var document = _documentRepository.Create();
            document.AccountId = accountId;
            document.DateTime = DateTime.Now;
            document.TypeId = documentModel.Type.Id;
            document.Title = documentModel.Title;
            document.TitleEn = documentModel.TitleEn;
            document.MimeType = documentModel.MimeType;
            document.Data = documentModel.Data;
            document.Note = documentModel.Note;
            document.FileName = documentModel.FileName;

            document = await _documentRepository.SaveAsync(document, cancellationToken);
            return Ok(await document.ToDocumentModelAsync(GetExpandOptions(expandOptions), cancellationToken));
        }
    }
}