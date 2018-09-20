using Microsoft.Extensions.Options;
using RestApiModel.Model;
using RestApiModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chayns.Backend.Api.Credentials;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Repository;


namespace RestApiModel.Helper
{
    public class MessageHelper : IMessageHelper
    {
        public ChaynsApiKey _backendApiSettings;
        public MessageHelper(IOptions<ChaynsApiKey> backendApiSettings)
        {
            _backendApiSettings = backendApiSettings.Value;
        }
        public bool SendIntercom(string message)
        {
            var secret = new SecretCredentials(_backendApiSettings.Secret, 430000);
            var intercomRepository = new IntercomRepository(secret);
            var intercomData = new IntercomData(151212)
            {
                Message = message,
                UserIds = new List<int>
                {
                    908022, 124
                }
            };
            var result = intercomRepository.SendIntercomMessage(intercomData);
            return result.Status.Success;
        }
    }
}
