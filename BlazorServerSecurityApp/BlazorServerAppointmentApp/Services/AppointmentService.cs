using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Appointment.Shared.DTO;
using AutoMapper;
using BlazorServerAppointmentApp.Data.Interfaces;
using BlazorServerAppointmentApp.Model;
using Newtonsoft.Json;

namespace BlazorServerAppointmentApp.Data
{
    public class AppointmentService : IAppointmentService
    {
        private HttpClient HttpClient;
        private AppSettingsModel Settings;
        private IMapper Mapper;

        public AppointmentService(HttpClient httpClient, AppSettingsModel settings, IMapper mapper)
        {
            HttpClient = httpClient;
            Settings = settings;
            Mapper = mapper;
        }

        public async Task<bool> CheckAppointmentAvailability(AppointmentViewModel appointmentViewModel)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/appointments/check");
            var dto = Mapper.Map<AppointmentDTO>(appointmentViewModel);
            var json = JsonConvert.SerializeObject(dto);

            var response = await HttpClient.PostAsJsonAsync(uri, dto).ConfigureAwait(false);
            // var response = 
            //     await HttpClient.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(dto), 
            //         Encoding.UTF8, "application/json"));

            return await response.Content.ReadAsAsync<bool>();
        }

        public async Task CreateAppointment(AppointmentViewModel appointmentViewModel)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/appointments");
            var dto = Mapper.Map<AppointmentDTO>(appointmentViewModel);
            await HttpClient.PostAsJsonAsync(uri, dto);
        }

        public async Task<List<DoctorViewModel>> GetDoctors()
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/doctors");
            return Mapper.Map<List<DoctorViewModel>>(await HttpClient.GetFromJsonAsync<DoctorDTO[]>(uri));
        }

        public async Task<DoctorViewModel> GetDoctor(long Id)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/doctors/{Id}");
            return Mapper.Map<DoctorViewModel>(await HttpClient.GetFromJsonAsync<DoctorDTO>(uri));
        }

        public async Task CreateOrUpdateDoctor(DoctorViewModel doctor)
        {
            var dto = Mapper.Map<DoctorDTO>(doctor);
            Uri uri = new Uri($"{Settings.WebApiUrl}/doctors");
            if (doctor.Id == 0)
            {
                await HttpClient.PostAsJsonAsync(uri, dto);
            }
            else
            {
                await HttpClient.PutAsJsonAsync(uri, dto);
            }
        }

        public async Task DeleteDoctor(long doctorId)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/doctors/{doctorId}");
            await HttpClient.DeleteAsync(uri);
        }

        public async Task<List<MedicalProcedureViewModel>> GetMedicalProcedures()
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/medicalprocedures");
            return Mapper.Map<List<MedicalProcedureViewModel>>(
                await HttpClient.GetFromJsonAsync<MedicalProcedureDTO[]>(uri));
        }

        public async Task<MedicalProcedureViewModel> GetMedicalProcedure(long Id)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/medicalprocedures/{Id}");
            return Mapper.Map<MedicalProcedureViewModel>(await HttpClient.GetFromJsonAsync<MedicalProcedureDTO>(uri));
        }

        public async Task CreateOrUpdateMedicalProcedure(MedicalProcedureViewModel medicalProcedureViewModel)
        {
            var dto = Mapper.Map<MedicalProcedureDTO>(medicalProcedureViewModel);
            Uri uri = new Uri($"{Settings.WebApiUrl}/medicalprocedures");
            if (medicalProcedureViewModel.Id == 0)
            {
                await HttpClient.PostAsJsonAsync(uri, dto);
            }
            else
            {
                await HttpClient.PutAsJsonAsync(uri, dto);
            }
        }

        public async Task DeleteMedicalProcedure(long medicalProcedureId)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/medicalprocedures/{medicalProcedureId}");
            await HttpClient.DeleteAsync(uri);
        }

        public async Task<List<AppointmentViewModel>> GetAppointments()
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/appointments");
            return Mapper.Map<List<AppointmentViewModel>>(await HttpClient.GetFromJsonAsync<AppointmentDTO[]>(uri));
        }

        public async Task<List<AppointmentViewModel>> GetUserAppointments(string userId)
        {
            Uri uri = new Uri($"{Settings.WebApiUrl}/appointments/userappointments/" + userId);
            return Mapper.Map<List<AppointmentViewModel>>(await HttpClient.GetFromJsonAsync<AppointmentDTO[]>(uri));
        }
    }
}