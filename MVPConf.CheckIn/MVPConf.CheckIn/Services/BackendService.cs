using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVPConf.CheckIn.Services
{
    class BackendService : IBackendService
    {        
        private IApiService Endpoint { get; }

        public BackendService(HttpClient client)
        {
            Endpoint = RestService.For<IApiService>(client);
        }
        
        public async Task<AttendeeResult> GetAttendees()
        {
            try
            {
                return await Endpoint.GetData<AttendeeResult>(new Request()
                {
                    Key = Constants.KEY,
                    Action = Constants.PARTICIPANTES
                }).ConfigureAwait(false);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<SpeakSessionResult> GetSpeakSessions()
        {
            try
            {
                return await Endpoint.GetData<SpeakSessionResult>(new Request()
                {
                    Key = Constants.KEY,
                    Action = Constants.PALESTRAS
                }).ConfigureAwait(false);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<ScheduleResult> GetSchedules()
        {
            try
            {
                return await Endpoint.GetData<ScheduleResult>(new Request()
                {
                    Key = Constants.KEY,
                    Action = Constants.AGENDAS
                }).ConfigureAwait(false);
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
